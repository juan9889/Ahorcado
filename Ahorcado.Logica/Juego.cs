using Ahorcado.Data;
using Ahorcado.Entidades;
using System.Text.Json;

namespace Ahorcado.Logica;
public class Juego
{
    public enum Status
    {
        Victoria = 1,
        Derrota,
        En_Progreso
    }
    public enum Difficulty
    {
        Facil = 1,
        Medio,
        Dificil
    }
    public string response;
    public int idPartida = 0;
    public string palabra = "";
    public char[] palabra_adivinada;
    public char[] letras;
    public int intentos_disponibles = 6;
    public DateTime startTime;
    public DateTime endTime;
    public Juego(string _palabra)
    {
        startTime = DateTime.Now;
        palabra = _palabra;
        palabra_adivinada = new char[_palabra.Length];
        letras = new char[_palabra.Length];
        for (int a = 0; a < palabra.Length; a++)
        {
            palabra_adivinada[a] = '_';
        }
        letras = palabra.ToCharArray();
    }

    public Juego(Difficulty diff)
    {
        int length = generarLongitudPalabra(diff);
        palabra = getPalabraRandomPorDificultad(length);
        if (palabra != null)
        {
            startTime = DateTime.Now;
            palabra_adivinada = new char[length];
            letras = new char[length];
            for (int a = 0; a < length; a++)
            {
                palabra_adivinada[a] = '_';
            }
            letras = palabra.ToCharArray();
        }
    }

    private string? getPalabraRandomPorDificultad(int length)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://palabras-aleatorias-public-api.herokuapp.com/");
            var responseTask = client.GetAsync($"random-by-length?length={length}");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                GetRandomWordResponse? res = JsonSerializer.Deserialize<GetRandomWordResponse>(readTask.Result);
                return res?.body?.Word;
            }
            return null;
        }
    }

    private int generarLongitudPalabra(Difficulty diff)
    {
        int length = 0;
        switch (diff)
        {
            case Difficulty.Facil:
                length = (int)new Random().NextInt64(4, 8);
                break;
            case Difficulty.Medio:
                length = (int)new Random().NextInt64(9, 13);
                break;
            case Difficulty.Dificil:
                length = (int)new Random().NextInt64(14, 19);
                break;
        }
        return length;
    }


    public bool probarLetra(char letra_in)
    {
        bool result = false;
        foreach (char letra in letras)
        {
            if (letra == letra_in)
            {
                result = true;
            }

        }
        if (result)
        {
            int i = 0;
            foreach (char l in letras)
            {
                if (l == letra_in)
                {
                    palabra_adivinada[i] = letra_in;
                }
                i++;
            }
        }
        else
        {
            intentos_disponibles--;
        }
        if (checkResultado() != Status.En_Progreso)
        {
            Partida partida = new();
            partida.gano = (int)checkResultado();
            partida.intentos_disponibles = intentos_disponibles;
            partida.tiempo_transcurrido = (endTime - startTime).TotalMilliseconds;
            int cantidad_letras_adivinadas = 0;
            for (int i = 0; i < palabra.Length; i++)
            {
                if (palabra_adivinada[i] != '_')
                {
                    cantidad_letras_adivinadas++;
                }
            }
            partida.cantidad_letras_adivinadas = cantidad_letras_adivinadas;
            idPartida = JuegoData.PostResultado(partida);
        }
        return result;
    }

    public Status checkResultado()
    {
        if (intentos_disponibles == 0)
        {
            endTime = DateTime.Now;
            return Status.Derrota;
        }
        if (!palabra_adivinada.Contains('_'))
        {
            endTime = DateTime.Now;
            return Status.Victoria;
        }
        return Status.En_Progreso;
    }

}

class GetRandomWordResponse
{
    public ResponseBody? body { get; set; }
}

class ResponseBody
{
    public string? Word { get; set; }
}

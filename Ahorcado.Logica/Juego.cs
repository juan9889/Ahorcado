using Ahorcado.Data;
using Ahorcado.Entidades;
using System.Text.Json;

namespace Ahorcado.Logica;
public class Juego
{
    private const string SCHEMA = "https://";
    private const string BASE_URL = "palabras-aleatorias-public-api.herokuapp.com";

    public enum Status
    {
        Victoria = 1,
        Derrota,
        EnProgreso
    }
    public enum Difficulty
    {
        Facil = 1,
        Medio,
        Dificil
    }

    public string Palabra { get; set; }
    public long UserId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public int IdPartida { get; set; }
    public char[] PalabraAdivinada { get; set; }
    public char[] Letras { get; set; }
    public int IntentosDisponibles { get; set; } = 6;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Juego(string _palabra)
    {
        StartTime = DateTime.Now;
        Palabra = _palabra;
        PalabraAdivinada = new char[_palabra.Length];
        Letras = new char[_palabra.Length];
        for (int a = 0; a < Palabra.Length; a++)
        {
            PalabraAdivinada[a] = '_';
        }
        Letras = Palabra.ToCharArray();
    }

    public Juego(Difficulty diff, long userId)
    {
        int length = GenerarLongitudPalabra(diff);
        PalabraAdivinada = new char[length];
        Letras = new char[length];
        Palabra = GetPalabraRandomPorDificultad(length);
        if (Palabra != "")
        {
            Palabra = Palabra.ToUpper();
            StartTime = DateTime.Now;
            StartDate = StartTime;
            UserId = userId;
            for (int a = 0; a < length; a++)
            {
                PalabraAdivinada[a] = '_';
            }
            Letras = Palabra.ToCharArray();
        }
    }

    private static string GetPalabraRandomPorDificultad(int length)
    {
        using HttpClient client = new();
        client.BaseAddress = new Uri(SCHEMA + BASE_URL);
        var responseTask = client.GetAsync($"/random-by-length?length={length}");
        responseTask.Wait();
        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {
            var readTask = result.Content.ReadAsStringAsync();
            readTask.Wait();
            GetRandomWordResponse? res = JsonSerializer.Deserialize<GetRandomWordResponse>(readTask.Result);
            if (res is not null && res.body is not null && res.body.Word is not null)
            {
                return res.body.Word;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }

    private static int GenerarLongitudPalabra(Difficulty diff)
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


    public bool ProbarLetra(char letra_in)
    {
        bool result = false;
        foreach (char letra in Letras)
        {
            if (letra == letra_in)
            {
                result = true;
            }

        }
        if (result)
        {
            int i = 0;
            foreach (char l in Letras)
            {
                if (l == letra_in)
                {
                    PalabraAdivinada[i] = letra_in;
                }
                i++;
            }
        }
        else
        {
            IntentosDisponibles--;
        }
        if (CheckResultado() != Status.EnProgreso)
        {
            Partida partida = new()
            {
                Gano = (int)CheckResultado(),
                Intentos_disponibles = IntentosDisponibles,
                Tiempo_transcurrido = (EndTime - StartTime).TotalMilliseconds
            };
            int cantidad_letras_adivinadas = 0;
            for (int i = 0; i < Palabra.Length; i++)
            {
                if (PalabraAdivinada[i] != '_')
                {
                    cantidad_letras_adivinadas++;
                }
            }
            partida.Cantidad_letras_adivinadas = cantidad_letras_adivinadas;
            partida.UserId = UserId;
            partida.StartDate = StartDate;
            IdPartida = JuegoData.PostResultado(partida);
        }
        return result;
    }

    public Status CheckResultado()
    {
        if (IntentosDisponibles == 0)
        {
            EndTime = DateTime.Now;
            return Status.Derrota;
        }
        if (!PalabraAdivinada.Contains('_'))
        {
            EndTime = DateTime.Now;
            return Status.Victoria;
        }
        return Status.EnProgreso;
    }

}

class GetRandomWordResponse
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "JSON Mapping")]
    public ResponseBody? body { get; set; }
}

class ResponseBody
{
    public string? Word { get; set; }
}

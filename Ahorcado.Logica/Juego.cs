namespace Ahorcado.Logica;
public class Juego
{
    public string palabra = "";
    public char[] palabra_adivinada;
    public char[] letras;
    public int intentos_disponibles = 6;
    public Juego(string _palabra)
    {
        palabra = _palabra;
        palabra_adivinada = new char[_palabra.Length];
        letras = new char[_palabra.Length];
        for(int a=0; a<palabra.Length; a++)
        {
            palabra_adivinada[a] = '_';
        }
        letras = palabra.ToCharArray();
    }

    public bool probarLetra(char letra_in)
    {
        bool result = false;
        foreach(char letra in letras)
        {
            if (letra == letra_in)
            {
                result=true;
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
        return result;
    }

  

}


using Xunit;
using Ahorcado.Logica;
using System.Threading.Tasks;

namespace UnitTests;

public class JuegoTest
{
    [Fact]
    public void ProbarEncontrarLetra()
    {
        Juego juego = new("ARBOL");
        Assert.True(juego.ProbarLetra('A'));
    }

    [Fact]
    public void ProbarNoEncontrarLetra()
    {
        Juego juego = new("ARBOL");
        Assert.False(juego.ProbarLetra('Q'));
    }

    [Fact]
    public void ProbarDescuentoIntentos()
    {
        Juego juego = new("ARBOL");
        juego.ProbarLetra('Q');
        Assert.Equal(5, juego.IntentosDisponibles);
    }

    [Fact]
    public void ProbarJuegoSinErrores()
    {
        Juego juego = new("ARBOL");
        juego.ProbarLetra('A');
        juego.ProbarLetra('R');
        juego.ProbarLetra('B');
        juego.ProbarLetra('O');
        juego.ProbarLetra('L');
        Assert.InRange(juego.IntentosDisponibles, 0, 6);
        Assert.Equal(juego.Letras, juego.PalabraAdivinada);
        Assert.Equal(Juego.Status.Victoria, juego.CheckResultado());

    }

    [Fact]
    public void ProbarPartidaPerdida()
    {
        Juego juego = new("ARBOL");
        juego.ProbarLetra('A');
        juego.ProbarLetra('R');
        juego.ProbarLetra('B');
        juego.ProbarLetra('O');
        juego.ProbarLetra('C');
        juego.ProbarLetra('D');
        juego.ProbarLetra('E');
        juego.ProbarLetra('F');
        juego.ProbarLetra('G');
        juego.ProbarLetra('H');
        Assert.Equal(0, juego.IntentosDisponibles);
        char[] a = { 'A', 'R', 'B', 'O', '_' };
        Assert.Equal(a, juego.PalabraAdivinada);
        Assert.Equal(Juego.Status.Derrota, juego.CheckResultado());
    }

    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S3168:\"async\" methods should not return \"void\"", Justification = "No point in returning a value on this test")]
    public async void ProbarTiempoTranscurridoPartida()
    {
        Juego juego = new("ARBOL");
        await Task.Delay(1000);
        juego.ProbarLetra('A');
        juego.ProbarLetra('R');
        juego.ProbarLetra('B');
        await Task.Delay(1000);
        juego.ProbarLetra('O');
        juego.ProbarLetra('L');

        Assert.Equal(Juego.Status.Victoria, juego.CheckResultado());
        Assert.InRange((juego.EndTime - juego.StartTime).TotalSeconds, 2, 3);
    }

    [Fact]
    public void ProbarGenerarPalabraRandom()
    {
        Juego juego = new(Juego.Difficulty.Medio, 0);
        Assert.InRange(juego.Palabra.Length, 9, 13);
    }

}

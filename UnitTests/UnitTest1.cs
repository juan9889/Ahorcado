using Xunit;
using Ahorcado.Logica;
using System.Threading.Tasks;
using System;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public void ProbarEncontrarLetra()
    {
        Juego juego = new("ARBOL");
        Assert.True(juego.probarLetra('A'));
    }

    [Fact]
    public void ProbarNoEncontrarLetra()
    {
        Juego juego = new("ARBOL");
        Assert.False(juego.probarLetra('Q'));
    }

    [Fact]
    public void ProbarDescuentoIntentos()
    {
        Juego juego = new("ARBOL");
        juego.probarLetra('Q');
        Assert.Equal(5, juego.intentos_disponibles);
    }

    [Fact]
    public void ProbarJuegoSinErrores()
    {
        Juego juego = new("ARBOL");
        juego.probarLetra('A');
        juego.probarLetra('R');
        juego.probarLetra('B');
        juego.probarLetra('O');
        juego.probarLetra('L');
        Assert.InRange(juego.intentos_disponibles, 0, 6);
        Assert.Equal(juego.letras, juego.palabra_adivinada);
        Assert.Equal(Juego.Status.Victoria, juego.checkResultado());
        
    }

    [Fact]
    public void ProbarPartidaPerdida()
    {
        Juego juego = new("ARBOL");
        juego.probarLetra('A');
        juego.probarLetra('R');
        juego.probarLetra('B');
        juego.probarLetra('O');
        juego.probarLetra('C');
        juego.probarLetra('D');
        juego.probarLetra('E');
        juego.probarLetra('F');
        juego.probarLetra('G');
        juego.probarLetra('H');
        Assert.Equal(0, juego.intentos_disponibles);
        char[] a = { 'A', 'R', 'B', 'O', '_' };
        Assert.Equal(a, juego.palabra_adivinada);
        Assert.Equal(Juego.Status.Derrota, juego.checkResultado());
    }

    [Fact]
    public async void ProbarTiempoTranscurridoPartida()
    {
        Juego juego = new("ARBOL");
        await Task.Delay(1000);
        juego.probarLetra('A');
        juego.probarLetra('R');
        juego.probarLetra('B');
        await Task.Delay(1000);
        juego.probarLetra('O');
        juego.probarLetra('L');

        Assert.Equal(Juego.Status.Victoria, juego.checkResultado());
        Assert.InRange((juego.endTime - juego.startTime).TotalSeconds, 2, 3);
    }

    [Fact]
    public void ProbarEncontrarUsuario()
    {
        Assert.Equal("Juan", Ahorcado.Data.UsuarioData.GetUsuario(1));
    }

    [Fact]
    public async void ProbarGuardarResultado()
    {
        Juego juego = new("ARBOL");
        await Task.Delay(1000);
        juego.probarLetra('A');
        juego.probarLetra('R');
        juego.probarLetra('B');
        await Task.Delay(1000);
        juego.probarLetra('O');
        juego.probarLetra('L');
    }

    
}

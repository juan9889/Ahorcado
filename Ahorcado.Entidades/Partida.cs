using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahorcado.Entidades
{
    public class Partida
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int Gano { get; set; }
        public int Intentos_disponibles { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public double Tiempo_transcurrido { get; set; }
        public int Cantidad_letras_adivinadas { get; set; }
    }
}

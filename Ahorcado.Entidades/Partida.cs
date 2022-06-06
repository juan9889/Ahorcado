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
        public int gano { get; set; }
        public int intentos_disponibles { get; set; }
        public float tiempo_transcurrido { get; set; }
        public int cantidad_letras_adivinadas { get; set; }
    }
}

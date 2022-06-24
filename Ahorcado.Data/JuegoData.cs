using Ahorcado.Entidades;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahorcado.Data
{
    public class JuegoData : Context
    {
        public static int PostResultado(Partida partida)
        {
            try
            {
                int id = 0;
                using (SqliteConnection connection = new SqliteConnection(getConnectionString()))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO partidas (gano,intentos_disponibles,tiempo_transcurrido,cantidad_letras_adivinadas) VALUES ($gano, $intentos_disponibles, $tiempo_transcurrido, $cantidad_letras_adivinadas)";
                    command.Parameters.AddWithValue("$gano", partida.gano);
                    command.Parameters.AddWithValue("$intentos_disponibles", partida.intentos_disponibles);
                    command.Parameters.AddWithValue("$tiempo_transcurrido", partida.tiempo_transcurrido);
                    command.Parameters.AddWithValue("$cantidad_letras_adivinadas", partida.cantidad_letras_adivinadas);
                    id = command.ExecuteNonQuery();
                }
                Console.WriteLine(id);
                return id;
            }
            catch
            {
                return 0;
            }
        }

        public static Partida GetPartida(int id)
        {
            Partida partida = new Partida();
            using (SqliteConnection connection = new SqliteConnection(getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM partidas WHERE id = $id";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        partida.gano = reader.GetInt32(1);
                        partida.intentos_disponibles = reader.GetInt32(2);
                        partida.tiempo_transcurrido = reader.GetFloat(3);
                        partida.cantidad_letras_adivinadas = reader.GetInt32(4);
                    }
                }
            }
            return partida;
        }
        public static List<Partida> GetPartidasUsuario(int id_usuario)
        {
            List<Partida> partidas = new();
            using (SqliteConnection connection = new SqliteConnection(getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM partidas WHERE id_usuario = $id";
                command.Parameters.AddWithValue("$id", id_usuario);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Partida partida = new();
                        partida.gano = reader.GetInt32(1);
                        partida.intentos_disponibles = reader.GetInt32(2);
                        partida.tiempo_transcurrido = reader.GetFloat(3);
                        partida.cantidad_letras_adivinadas = reader.GetInt32(4);
                        partidas.Add(partida);
                    }
                }
            }
            return partidas;
        }
    }
}

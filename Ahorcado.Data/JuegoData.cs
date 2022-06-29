using Ahorcado.Entidades;
using Microsoft.Data.Sqlite;

namespace Ahorcado.Data
{
    public class JuegoData : Context
    {
        public static int PostResultado(Partida partida)
        {
            try
            {
                int id = 0;
                using (SqliteConnection connection = new(getConnectionString()))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO partidas (id_usuario,gano,intentos_disponibles,fecha,tiempo_transcurrido,cantidad_letras_adivinadas) VALUES ($id_usuario, $gano, $intentos_disponibles,$fecha, $tiempo_transcurrido, $cantidad_letras_adivinadas)";
                    command.Parameters.AddWithValue("$id_usuario", partida.UserId);
                    command.Parameters.AddWithValue("$gano", partida.Gano);
                    command.Parameters.AddWithValue("$intentos_disponibles", partida.Intentos_disponibles);
                    command.Parameters.AddWithValue("$fecha", partida.StartDate.ToString("o"));
                    command.Parameters.AddWithValue("$tiempo_transcurrido", partida.Tiempo_transcurrido);
                    command.Parameters.AddWithValue("$cantidad_letras_adivinadas", partida.Cantidad_letras_adivinadas);
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
            Partida partida = new();
            using (SqliteConnection connection = new(getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM partidas WHERE id = $id";
                command.Parameters.AddWithValue("$id", id);

                using SqliteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    partida.UserId = reader.GetInt32(1);
                    partida.Gano = reader.GetInt32(2);
                    partida.Intentos_disponibles = reader.GetInt32(3);
                    partida.StartDate = reader.GetDateTime(4);
                    partida.Tiempo_transcurrido = reader.GetFloat(5);
                    partida.Cantidad_letras_adivinadas = reader.GetInt32(6);
                }
            }
            return partida;
        }
        public static List<Partida> GetPartidasUsuario(int id_usuario)
        {
            List<Partida> partidas = new();
            using (SqliteConnection connection = new(getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM partidas WHERE id_usuario = $id";
                command.Parameters.AddWithValue("$id", id_usuario);

                using SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Partida partida = new()
                    {
                        UserId = reader.GetInt32(1),
                        Gano = reader.GetInt32(2),
                        Intentos_disponibles = reader.GetInt32(3),
                        StartDate = reader.GetDateTime(4),
                        Tiempo_transcurrido = reader.GetFloat(5),
                        Cantidad_letras_adivinadas = reader.GetInt32(6)
                    };
                    partidas.Add(partida);
                }
            }
            return partidas;
        }
    }
}

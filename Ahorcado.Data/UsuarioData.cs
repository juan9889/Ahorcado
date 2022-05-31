using System.Data;
using Microsoft.Data.Sqlite;

namespace Ahorcado.Data;
public class UsuarioData
{
    static string cs = "AhorcadoDB.db";
    public static string GetUsuario(long id)
    {
        string name = "";
        using (var connection = new SqliteConnection("Data Source=hello.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
        SELECT name
        FROM user
        WHERE id = $id
    ";
            command.Parameters.AddWithValue("$id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    name = reader.GetString(1);
                    Console.WriteLine(name);
                    
                }
            }
        }
        return name;
    }


}


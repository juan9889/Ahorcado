using System.Data;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ahorcado.Data;
public class UsuarioData : Context
{
    public static string GetUsuario(long id)
    {
        string name = "";
        
        using (SqliteConnection connection = new SqliteConnection(getConnectionString()))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM usuarios WHERE id = $id";
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

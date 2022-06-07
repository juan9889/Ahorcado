using System.Data;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Ahorcado.Entidades;

namespace Ahorcado.Data;
public class UsuarioData : Context
{
    public static Usuario GetUsuario(long id)
    {
        string name = "";
        Usuario usuario = new();
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
                    usuario.ID= reader.GetInt32(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Clave = reader.GetString(2);
                    

                }
            }
        }
        return usuario;
    }
    

    public static Usuario Login(string nombre, string clave)
    {
        Usuario usuario = new();
        
        using (SqliteConnection connection = new SqliteConnection(getConnectionString()))
        {
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM usuarios WHERE nombre = $nombre AND clave = $clave" ;
            command.Parameters.AddWithValue("$nombre", nombre);
            command.Parameters.AddWithValue("$clave", clave);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    usuario = reader.GetString(1);
                    Console.WriteLine(usuario);

                }
            }
        }
        return usuario;
    }


}

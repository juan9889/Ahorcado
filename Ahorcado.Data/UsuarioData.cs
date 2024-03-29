﻿using System.Data;
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
        Usuario usuario = new();
        using (SqliteConnection connection = new(getConnectionString()))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM usuarios WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                usuario.ID = reader.GetInt32(0);
                usuario.Nombre = reader.GetString(1);
                usuario.Clave = reader.GetString(2);
            }
        }

        return usuario;
    }


    public static Usuario Login(string nombre, string clave)
    {
        Usuario usuario = new();

        using (SqliteConnection connection = new(getConnectionString()))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM usuarios WHERE nombre = $nombre AND pass = $clave";
            command.Parameters.AddWithValue("$nombre", nombre);
            command.Parameters.AddWithValue("$clave", clave);

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                usuario.ID = reader.GetInt32(0);
                usuario.Nombre = reader.GetString(1);
                usuario.Clave = reader.GetString(2);
            }
        }
        return usuario;
    }

    public static bool RegistrarUsuario(string nombre, string clave)
    {
        var resultado = false;
        using (SqliteConnection connection = new(getConnectionString()))
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = "SELECT count(*) FROM usuarios WHERE nombre = $nombre";
            command.Parameters.AddWithValue("$nombre", nombre);

            var count = Convert.ToInt32(command.ExecuteScalar());

            if (count == 0)
            {
                command.Parameters.Clear();

                command.CommandText = "INSERT INTO usuarios (nombre, pass) VALUES ($nombre, $pass)";
                command.Parameters.AddWithValue("$nombre", nombre);
                command.Parameters.AddWithValue("$pass", clave);

                resultado = command.ExecuteNonQuery() > 0;
            }
        }

        return resultado;
    }
}

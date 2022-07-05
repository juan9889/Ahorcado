using System;
using System.Runtime.InteropServices;

namespace Ahorcado.Data
{
	public class Context
	{
		public Context()
		{
		}
        public static string getConnectionString()
        {
            string connstring = "";
            string bar = "/";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                bar = "\\";
            }
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string projectDirectory = currentDirectory.Parent.Parent.Parent.Parent.FullName;
            connstring = "Data Source=" + projectDirectory + bar + "AhorcadoDB.db";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //Esta en el servidor
                connstring = "Data Source=AhorcadoDB.db";
            }
            return connstring;

        }
    }
}


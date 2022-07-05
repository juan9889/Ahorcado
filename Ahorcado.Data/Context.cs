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
            string bar = "/";
            string connectionstring = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                bar = "\\";
            }
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string projectDirectory = currentDirectory.Parent.Parent.Parent.Parent.FullName;
            connectionstring = "Data Source=" + projectDirectory + bar + "AhorcadoDB.db";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                connectionstring = "Data Source=AhorcadoDB.db";
            }
            return connectionstring;

        }
    }
}


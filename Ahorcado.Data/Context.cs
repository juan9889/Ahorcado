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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                bar = "\\";
            }
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string projectDirectory = currentDirectory.Parent.Parent.Parent.Parent.FullName;
            return "Data Source=" + projectDirectory + bar + "AhorcadoDB.db";

        }
    }
}


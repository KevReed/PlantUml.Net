using System;
using System.IO;

namespace PlantUml.Net.Java
{
    internal class EnvironmentJavaLocator : IJavaLocator
    {
        public string GetJavaInstallationPath()
        {
            string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME").Trim('"');

            return Directory.Exists(javaHome)
                ? javaHome
                : null;
        }
    }
}

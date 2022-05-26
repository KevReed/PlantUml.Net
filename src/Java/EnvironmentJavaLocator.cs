using System;
using System.IO;

namespace PlantUml.Net.Java
{
    internal class EnvironmentJavaLocator : IJavaLocator
    {
        public string GetJavaInstallationPath()
        {
            string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (string.IsNullOrWhiteSpace(javaHome))
            {
                throw new JavaNotFoundException("Environment variable JAVA_HOME is not set");
            }

            javaHome = javaHome.Trim('"');

            string javaExecutable = Path.Combine(javaHome, "bin", "java.exe");
            if (!File.Exists(javaExecutable))
            {
                throw new JavaNotFoundException($"Java executable '{javaExecutable}' does not exist");
            }

            return javaExecutable;
        }
    }
}

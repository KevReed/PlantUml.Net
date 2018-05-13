using System;
using System.IO;
using PlantUml.Net.Tools;

namespace PlantUml.Net.Java
{
    internal class JarRunner
    {
        private readonly string javaPath;
        private readonly string jarPath;

        public JarRunner(string javaPath, string jarPath)
        {
            this.javaPath = javaPath;
            this.jarPath = jarPath;
        }

        public IProcessResult RunJarWithInput(string input, params string[] arguments)
        {
            ValidateJavaPath();
            var argumentString = $"-jar {jarPath} {string.Join(" ", arguments)}";
            return new ProcessHelper().RunProcessWithInput(javaPath, argumentString, input);
        }

        private void ValidateJavaPath()
        {
            if (File.Exists(javaPath))
            {
                return;
            }

            throw new FileNotFoundException("Unable to locate java.exe, check your JAVA_HOME environment variable or specify JavaPath in PlantUmlSettings.");
        }
    }
}

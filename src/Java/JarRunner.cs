using System.IO;
using System.Threading.Tasks;
using PlantUml.Net.Tools;

namespace PlantUml.Net.Java
{
    internal class JarRunner
    {
        private readonly string javaPath;
        private readonly string jarPath;

        public JarRunner(string javaHome, string jarPath)
        {
            javaPath = Path.Combine(javaHome, "bin", "java.exe");
            this.jarPath = jarPath;
        }

        public IProcessResult RunJarWithInput(string input, params string[] arguments)
        {
            var argumentString = $"-jar {jarPath} {string.Join(" ", arguments)}";
            return new ProcessHelper().RunProcessWithInput(javaPath, argumentString, input);
        }
    }
}

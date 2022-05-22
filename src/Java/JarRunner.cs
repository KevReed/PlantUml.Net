using System.IO;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<IProcessResult> RunJarWithInputAsync(string input, CancellationToken cancellationToken,
            params string[] arguments)
        {
            ValidateJavaPath();
            var argumentString = $"-jar \"{jarPath}\" {string.Join(" ", arguments)}";
            System.Diagnostics.Debug.WriteLine(argumentString);
            return await new ProcessHelper().RunProcessWithInputAsync(javaPath, argumentString, input, cancellationToken)
                .ConfigureAwait(false);
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

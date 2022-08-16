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
            if (!File.Exists(javaPath))
            {
                throw new JavaNotFoundException($"Java executable '{javaPath}' does not exist");
            }

            this.javaPath = javaPath;
            this.jarPath = jarPath;
        }

        public async Task<IProcessResult> RunJarWithInputAsync(string input, CancellationToken cancellationToken,
            params string[] arguments)
        {
            var argumentString = $"-Dfile.encoding=UTF-8 -jar \"{jarPath}\" {string.Join(" ", arguments)}";
            return await new ProcessHelper().RunProcessWithInputAsync(javaPath, argumentString, input, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

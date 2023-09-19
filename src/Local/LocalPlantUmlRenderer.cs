using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PlantUml.Net.Java;
using PlantUml.Net.Remote;
using PlantUml.Net.Tools;

namespace PlantUml.Net.Local
{
    internal class LocalPlantUmlRenderer : IPlantUmlRenderer
    {
        private readonly JarRunner jarRunner;
        private readonly string workingDirectory;
        private readonly LocalCommandProvider commandProvider;
        private readonly RenderUrlCalculator renderUrlCalculator;

        public LocalPlantUmlRenderer(JarRunner jarRunner, string workingDirectory,
            LocalCommandProvider commandProvider, RenderUrlCalculator renderUrlCalculator)
        {
            this.jarRunner = jarRunner;
            this.workingDirectory = workingDirectory;
            this.commandProvider = commandProvider;
            this.renderUrlCalculator = renderUrlCalculator;
        }

        public async Task<byte[]> RenderAsync(string code, OutputFormat outputFormat,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string command = commandProvider.GetCommand(outputFormat);
            IProcessResult processResult = await jarRunner.RunJarWithInputAsync(workingDirectory, code,
                cancellationToken, command, "-pipe", "-charset UTF-8").ConfigureAwait(false);
            if (processResult.ExitCode != 0)
            {
                string message = Encoding.UTF8.GetString(processResult.Error);
                throw new RenderingException(code, message);
            }

            return processResult.Output;
        }

        public byte[] Render(string code, OutputFormat outputFormat)
        {
            return RenderAsync(code, outputFormat).GetAwaiter().GetResult();
        }

        public Uri RenderAsUri(string code, OutputFormat outputFormat)
        {
            return renderUrlCalculator.GetRenderUrl(code, outputFormat);
        }
    }
}

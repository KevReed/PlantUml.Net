using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PlantUml.Net.Java;
using PlantUml.Net.Remote;

namespace PlantUml.Net.Local
{
    internal class LocalPlantUmlRenderer : IPlantUmlRenderer
    {
        private readonly JarRunner jarRunner;
        private readonly LocalCommandProvider commandProvider;
        private readonly RenderUrlCalculator renderUrlCalculator;

        public LocalPlantUmlRenderer(JarRunner jarRunner, LocalCommandProvider commandProvider, RenderUrlCalculator renderUrlCalculator)
        {
            this.jarRunner = jarRunner;
            this.commandProvider = commandProvider;
            this.renderUrlCalculator = renderUrlCalculator;
        }

        public async Task<byte[]> RenderAsync(string code, OutputFormat outputFormat, CancellationToken cancellationToken = default)
        {
            string command = commandProvider.GetCommand(outputFormat);
            var processResult = await jarRunner.RunJarWithInputAsync(code, cancellationToken, command, "-pipe", "-charset UTF-8")
                .ConfigureAwait(false);
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

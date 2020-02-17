using System;
using System.Threading.Tasks;
using PlantUml.Net.Java;
using PlantUml.Net.Remote;
using static System.Text.Encoding;

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

        public Task<byte[]> RenderAsync(string code, OutputFormat outputFormat)
        {
            return Task.FromResult(Render(code, outputFormat));
        }

        public byte[] Render(string code, OutputFormat outputFormat)
        {
            string command = commandProvider.GetCommand(outputFormat);
            var processResult = jarRunner.RunJarWithInput(code, command, "-pipe");

            if(processResult.ExitCode != 0)
            {
                string message = UTF8.GetString(processResult.Error);
                throw new RenderingException(code, message);
            }

            return processResult.Output;
        }

        public Uri RenderAsUri(string code, OutputFormat outputFormat)
        {
            string renderUri = renderUrlCalculator.GetRenderUrl(code, outputFormat);
            return new Uri(renderUri);
        }
    }
}

using System;
using PlantUml.Net.Java;

namespace PlantUml.Net.Local
{
    internal class LocalPlantUmlRenderer : IPlantUmlRenderer
    {
        private readonly JarRunner jarRunner;
        private readonly LocalCommandProvider commandProvider;

        public LocalPlantUmlRenderer(JarRunner jarRunner, LocalCommandProvider commandProvider)
        {
            this.jarRunner = jarRunner;
            this.commandProvider = commandProvider;
        }

        public string Render(string code, OutputFormat outputFormat)
        {
            string command = commandProvider.GetCommand(outputFormat);
            var processResult = jarRunner.RunJarWithInput(code, command, "-pipe");

            if(processResult.ExitCode != 0)
            {
                throw new RenderingException(code, processResult.Error);
            }

            return processResult.Output;
        }
    }
}

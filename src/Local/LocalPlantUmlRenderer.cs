using PlantUml.Net.Java;

using static System.Text.Encoding;

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
    }
}

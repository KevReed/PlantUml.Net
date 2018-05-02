using System.Diagnostics;

namespace PlantUml.Net.Tools
{
    internal class ProcessHelper
    {
        public IProcessResult RunProcessWithInput(string fileName, string arguments, string input)
        {
            ProcessStartInfo processStartInfo = GetProcessStartInfo(fileName, arguments);

            using (Process process = Process.Start(processStartInfo))
            {
                process.WriteInput(input);
                return new ProcessResult
                {
                    Output = process.GetOutput(),
                    Error = process.GetError(),
                    ExitCode = process.ExitCode
                };
            }
        }

        private static ProcessStartInfo GetProcessStartInfo(string command, string arguments)
        {
            return new ProcessStartInfo(command)
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = arguments
            };
        }
    }
}

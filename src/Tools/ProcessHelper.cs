using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PlantUml.Net.Tools
{
    internal class ProcessHelper
    {
        public async Task<IProcessResult> RunProcessWithInputAsync(string fileName, string arguments, string input, CancellationToken cancellationToken)
        {
            using (Process process = new Process()
            {
                StartInfo = GetProcessStartInfo(fileName, arguments),
                EnableRaisingEvents = true
            })
            {
                var tcs = new TaskCompletionSource<ProcessResult>();
                if (cancellationToken != CancellationToken.None)
                {
                    cancellationToken.Register(() =>
                    {
                        if (tcs.TrySetCanceled())
                        {
                            process.Kill();
                        }
                    });
                }

                process.Start();
                process.WriteInput(input);

                Task.Run(() =>
                {
                    ProcessResult result = new ProcessResult
                    {
                        Output = process.GetOutput(),
                        Error = process.GetError(),
                        ExitCode = process.ExitCode
                    };
                    tcs.SetResult(result);
                });

                return await tcs.Task.ConfigureAwait(false);
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

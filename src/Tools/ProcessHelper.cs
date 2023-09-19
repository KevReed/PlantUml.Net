using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PlantUml.Net.Tools
{
    internal class ProcessHelper
    {
        public async Task<IProcessResult> RunProcessWithInputAsync(string fileName, string arguments, string workingDirectory,
            string input, CancellationToken cancellationToken)
        {
            using (Process process = new Process()
            {
                StartInfo = GetProcessStartInfo(fileName, arguments, workingDirectory),
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
                            try
                            {
                                process.Kill();
                            }
                            catch (ArgumentNullException)
                            {
                                //Catch potential ArgumentNullException: SafeHandle cannot be null
                            }
                        }
                    });
                }

                process.Start();
                process.WriteInput(input);

                _ = Task.Run(() =>
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

        private static ProcessStartInfo GetProcessStartInfo(string command, string arguments, string workingDirectory)
        {
            return new ProcessStartInfo(command)
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = arguments,
                StandardErrorEncoding = System.Text.Encoding.UTF8,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                WorkingDirectory = workingDirectory
            };
        }
    }
}

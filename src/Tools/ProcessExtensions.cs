using System.Diagnostics;
using System.IO;

namespace PlantUml.Net.Tools
{
    public static class ProcessExtensions
    {
        public static void WriteInput(this Process process, string input)
        {
            using (StreamWriter stdIn = process.StandardInput)
            {
                stdIn.AutoFlush = true;
                stdIn.Write(input);
                stdIn.Close();
            }
        }

        public static string GetOutput(this Process process)
        {
            using (StreamReader stdOut = process.StandardOutput)
            {
                return stdOut.ReadToEnd();
            }
        }

        public static string GetErrors(this Process process)
        {
            using (StreamReader stdErr = process.StandardError)
            {
                return stdErr.ReadToEnd();
            }
        }
    }
}

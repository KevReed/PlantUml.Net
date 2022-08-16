using System.Diagnostics;
using System.IO;
using System.Text;

namespace PlantUml.Net.Tools
{
    public static class ProcessExtensions
    {
        public static void WriteInput(this Process process, string input)
        {
            using (StreamWriter stdIn = new StreamWriter(process.StandardInput.BaseStream, new UTF8Encoding(false)))
            {
                stdIn.AutoFlush = true;
                stdIn.Write(input);
            }
        }

        public static byte[] GetOutput(this Process process)
        {
            return ExtractBytes(process.StandardOutput.BaseStream);
        }

        public static byte[] GetError(this Process process)
        {
            return ExtractBytes(process.StandardError.BaseStream);
        }

        private static byte[] ExtractBytes(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

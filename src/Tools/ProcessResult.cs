using System;
using System.IO;

namespace PlantUml.Net.Tools
{
    internal class ProcessResult : IProcessResult
    {
        public byte[] Output { get; set; }

        public byte[] Error { get; set; }

        public int ExitCode { get; set; }
    }
}
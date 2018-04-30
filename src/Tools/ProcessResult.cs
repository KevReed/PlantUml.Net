using System;

namespace PlantUml.Net.Tools
{
    internal class ProcessResult : IProcessResult
    {
        public string Output { get; set; }

        public string Error { get; set; }

        public int ExitCode { get; set; }
    }
}
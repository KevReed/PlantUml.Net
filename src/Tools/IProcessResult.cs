namespace PlantUml.Net.Tools
{
    public interface IProcessResult
    {
        string Output { get; }

        string Error { get; }

        int ExitCode { get; }
    }
}
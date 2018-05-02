namespace PlantUml.Net.Tools
{
    public interface IProcessResult
    {
        byte[] Output { get; }

        byte[] Error { get; }

        int ExitCode { get; }
    }
}
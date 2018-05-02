namespace PlantUml.Net
{
    public interface IPlantUmlRenderer
    {
        byte[] Render(string code, OutputFormat outputFormat);
    }
}
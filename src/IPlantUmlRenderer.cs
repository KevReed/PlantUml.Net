using System.Threading.Tasks;

namespace PlantUml.Net
{
    public interface IPlantUmlRenderer
    {
        string Render(string code, OutputFormat outputFormat);
    }
}
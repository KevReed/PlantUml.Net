using System;

namespace PlantUml.Net
{
    public interface IPlantUmlRenderer
    {
        byte[] Render(string code, OutputFormat outputFormat);

        Uri RenderAsUri(string code, OutputFormat outputFormat);
    }
}
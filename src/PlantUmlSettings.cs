namespace PlantUml.Net
{
    public class PlantUmlSettings
    {
        public string JavaPath { get; set; }
        public string RemoteUrl { get; set; }
        public string LocalPlantUmlPath { get; set; }
        public string LocalGraphvizDotPath { get; set; }
        public RenderingMode RenderingMode { get; set; }
        public OutputFormat OutputFormat { get; set; }

        public PlantUmlSettings()
        {
            OutputFormat = OutputFormat.Png;
            RenderingMode = RenderingMode.Remote;
            RemoteUrl = "http://www.plantuml.com/plantuml/";
            LocalPlantUmlPath = "plantuml.jar";
        }
    }
}

namespace PlantUml.Net
{
    public class PlantUmlSettings
    {
        /// <summary>
        /// Path to java.exe.
        /// By default this will be obtained from the JAVA_HOME environment variable.
        /// </summary>
        public string JavaPath { get; set; }

        /// <summary>
        /// Url pointing to remote PlantUml server.
        /// Defaults to http://www.plantuml.com/plantuml/
        /// </summary>
        public string RemoteUrl { get; set; }

        /// <summary>
        /// Path to plantuml.jar.
        /// Defaults to working directory.
        /// </summary>
        public string LocalPlantUmlPath { get; set; }

        /// <summary>
        /// Format of the error output.
        /// </summary>
        public ErrorReportMode ErrorReportMode { get; set; }

        /// <summary>
        /// Path to dot.exe.
        /// Required for Local rendering mode.
        /// </summary>
        public string LocalGraphvizDotPath { get; set; }

        /// <summary>
        /// Local or Remote rendering mode.
        /// Defaults to Remote.
        /// </summary>
        public RenderingMode RenderingMode { get; set; }

        /// <summary>
        /// Include a file as if '!include file' were used, also allowing pattern like '*.puml'
        /// </summary>
        public string Include { get; set; }

        /// <summary>
        /// Separators between diagrams if multiple diagrams are generated.
        /// This way it can be determined can determine where one image ends and another starts.
        /// </summary>
        public string Delimitor { get; set; }

        /// <summary>
        /// To generate the Nth image
        /// </summary>
        public int ImageIndex { get; set; }

        public PlantUmlSettings()
        {
            RenderingMode = RenderingMode.Remote;
            RemoteUrl = "http://www.plantuml.com/plantuml/";
            LocalPlantUmlPath = "plantuml.jar";
        }
    }
}

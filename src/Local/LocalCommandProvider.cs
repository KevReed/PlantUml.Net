using System;

namespace PlantUml.Net.Local
{
    internal class LocalCommandProvider
    {
        private string localGraphvizDotPath;

        private string GraphvizDotCommand => string.IsNullOrEmpty(localGraphvizDotPath)
            ? string.Empty
            : $" -graphvizdot {localGraphvizDotPath}";

        public LocalCommandProvider(string localGraphvizDotPath)
        {
            this.localGraphvizDotPath = $"\"{localGraphvizDotPath}\"";
        }

        public string GetCommand(OutputFormat outputFormat)
        {
            string outputFormatCommand = GetOuputFormatCommand(outputFormat);
            return outputFormatCommand + GraphvizDotCommand;
        }

        private string GetOuputFormatCommand(OutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case OutputFormat.Svg:
                    return "-tsvg";

                case OutputFormat.Png:
                    return "-tpng";

                case OutputFormat.Eps:
                    return "-teps";

                case OutputFormat.Pdf:
                    return "-tpdf";

                case OutputFormat.Vdx:
                    return "-tvdx";

                case OutputFormat.Xmi:
                    return "-txmi";

                case OutputFormat.Scxml:
                    return "-tscxml";

                case OutputFormat.Html:
                    return "-thtml";

                case OutputFormat.Ascii:
                    return "-ttxt";

                case OutputFormat.Ascii_Unicode:
                    return "-tutxt";

                case OutputFormat.LaTeX:
                    return "-tlatex";

                default:
                    throw new ArgumentException($"unknown {nameof(OutputFormat)} value", nameof(outputFormat));
            }
        }
    }
}
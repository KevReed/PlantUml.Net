using System;

namespace PlantUml.Net.Local
{
    internal class LocalCommandProvider
    {
        private readonly ErrorReportMode errorReportMode;
        private readonly string localGraphvizDotPath;
        private readonly string delimitor;

        private string ErrorReportModeCommand
        {
            get
            {
                switch (errorReportMode)
                {
                    case ErrorReportMode.TwoLines:
                        return "";
                    case ErrorReportMode.SingleLine:
                        return " -stdrpt:2";
                    case ErrorReportMode.Verbose:
                        return " -stdrpt:1";
                    default:
                        throw new ArgumentException($"Unknown {nameof(ErrorReportMode)} value", nameof(errorReportMode));
                }
            }
        }

    private string GraphvizDotCommand => string.IsNullOrEmpty(localGraphvizDotPath)
            ? string.Empty
            : $" -graphvizdot \"{localGraphvizDotPath}\"";

        private string DelimitorCommand => string.IsNullOrEmpty(delimitor)
            ? string.Empty
            : $" -pipedelimitor \"{delimitor}\"";

        public LocalCommandProvider(PlantUmlSettings settings)
        {
            errorReportMode = settings.ErrorReportMode;
            localGraphvizDotPath = settings.LocalGraphvizDotPath;
            delimitor = settings.Delimitor;
        }

        public string GetCommand(OutputFormat outputFormat)
        {
            string outputFormatCommand = GetOuputFormatCommand(outputFormat);
            outputFormatCommand += ErrorReportModeCommand;
            outputFormatCommand += GraphvizDotCommand;
            outputFormatCommand += DelimitorCommand;
            return outputFormatCommand;
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
                    throw new ArgumentException($"Unknown {nameof(OutputFormat)} value", nameof(outputFormat));
            }
        }
    }
}
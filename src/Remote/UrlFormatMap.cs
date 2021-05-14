using System;
using System.Linq;

namespace PlantUml.Net.Remote
{
    internal class UrlFormatMap
    {
        private readonly string remoteUrl;

        public UrlFormatMap(string remoteUrl)
        {
            this.remoteUrl = remoteUrl;
        }

        public Uri GetRenderUrl(string urlComponent, OutputFormat outputFormat)
        {
            var baseUri = new Uri(remoteUrl);
            
            switch (outputFormat)
            {
                case OutputFormat.Png:

                    return AppendPath(baseUri, "png", urlComponent);

                case OutputFormat.Svg:

                    return AppendPath(baseUri, "svg", urlComponent);

                case OutputFormat.Ascii:

                    return AppendPath(baseUri, "txt", urlComponent);

                case OutputFormat.Eps:

                    return AppendPath(baseUri, "eps", urlComponent);

                case OutputFormat.LaTeX:

                    return AppendPath(baseUri, "latex", urlComponent);

                default:

                    throw new NotSupportedException($"OutputFormat '{outputFormat}' is not supported for remote rendering");
            }
        }

        private static Uri AppendPath(Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/','\\')}/{path.TrimStart('/')}"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantUml.Net.Remote
{
    internal class UrlFormatMap
    {
        private readonly string remoteUrl;

        public UrlFormatMap(string remoteUrl)
        {
            this.remoteUrl = remoteUrl;
        }

        public string GetRenderUrl(string urlComponent, OutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case OutputFormat.Png:

                    return $"{remoteUrl}png/{urlComponent}";

                case OutputFormat.Svg:

                    return $"{remoteUrl}svg/{urlComponent}";

                default:

                    throw new NotSupportedException($"OutputFormat {outputFormat} is not supported for remote rendering");
            }
        }
    }
}

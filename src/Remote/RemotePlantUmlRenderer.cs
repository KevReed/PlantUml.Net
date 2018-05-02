using System;
using System.Net;
using System.Net.Http;
using PlantUml.Net.Java;
using PlantUml.Net.Tools;

using static System.Text.Encoding;

namespace PlantUml.Net.Remote
{
    internal class RemotePlantUmlRenderer : IPlantUmlRenderer
    {
        private readonly JarRunner jarRunner;
        private readonly UrlFormatMap urlFormatMap;

        public RemotePlantUmlRenderer(JarRunner jarRunner, UrlFormatMap urlFormatMap)
        {
            this.jarRunner = jarRunner;
            this.urlFormatMap = urlFormatMap;
        }

        public byte[] Render(string code, OutputFormat outputFormat)
        {
            string urlComponent = GetUrlComponent(code);
            string renderUrl = urlFormatMap.GetRenderUrl(urlComponent, outputFormat);

            using (HttpClient httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(renderUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    return result.Content.ReadAsByteArrayAsync().Result;
                }

                if(result.StatusCode == HttpStatusCode.BadRequest)
                {
                    var messages = result.Headers.GetValues("X-PlantUML-Diagram-Error");
                    throw new RenderingException(code, string.Join(Environment.NewLine, messages));
                }

                throw new HttpRequestException(result.ReasonPhrase);
            }
        }

        private string GetUrlComponent(string code)
        {
            IProcessResult processResult = jarRunner.RunJarWithInput(code, "-encodeurl", "-pipe");

            if (processResult.ExitCode != 0)
            {
                string message = UTF8.GetString(processResult.Error);
                throw new RenderingException(code, message);
            }

            return UTF8.GetString(processResult.Output);
        }
    }
}

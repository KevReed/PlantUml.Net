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
        private readonly UrlFormatMap urlFormatMap;

        public RemotePlantUmlRenderer(UrlFormatMap urlFormatMap)
        {
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
            return PlantUmlTextEncoding.EncodeUrl(code);
        }
    }
}

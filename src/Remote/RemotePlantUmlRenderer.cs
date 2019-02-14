using System;
using System.Net;
using System.Net.Http;

namespace PlantUml.Net.Remote
{
    internal class RemotePlantUmlRenderer : IPlantUmlRenderer
    {
        private readonly RenderUrlCalculator renderUrlCalculator;

        public RemotePlantUmlRenderer(RenderUrlCalculator renderUrlCalculator)
        {
            this.renderUrlCalculator = renderUrlCalculator;
        }

        public byte[] Render(string code, OutputFormat outputFormat)
        {
            string renderUrl = renderUrlCalculator.GetRenderUrl(code, outputFormat);

            using (HttpClient httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(renderUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    return result.Content.ReadAsByteArrayAsync().Result;
                }

                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    var messages = result.Headers.GetValues("X-PlantUML-Diagram-Error");
                    throw new RenderingException(code, string.Join(Environment.NewLine, messages));
                }

                throw new HttpRequestException(result.ReasonPhrase);
            }
        }

        public Uri RenderAsUri(string code, OutputFormat outputFormat)
        {
            string renderUri = renderUrlCalculator.GetRenderUrl(code, outputFormat);
            return new Uri(renderUri);
        }
    }
}

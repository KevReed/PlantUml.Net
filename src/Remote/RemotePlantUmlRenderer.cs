using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlantUml.Net.Remote
{
    internal class RemotePlantUmlRenderer : IPlantUmlRenderer
    {
        private readonly RenderUrlCalculator renderUrlCalculator;

        public RemotePlantUmlRenderer(RenderUrlCalculator renderUrlCalculator)
        {
            this.renderUrlCalculator = renderUrlCalculator;
        }

        public async Task<byte[]> RenderAsync(string code, OutputFormat outputFormat)
        {
            string renderUrl = renderUrlCalculator.GetRenderUrl(code, outputFormat);

            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(renderUrl).ConfigureAwait(false);

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }

                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    var messages = result.Headers.GetValues("X-PlantUML-Diagram-Error");
                    throw new RenderingException(code, string.Join(Environment.NewLine, messages));
                }

                throw new HttpRequestException(result.ReasonPhrase);
            }
        }

        public byte[] Render(string code, OutputFormat outputFormat)
        {
            return RenderAsync(code, outputFormat).GetAwaiter().GetResult();
        }

        public Uri RenderAsUri(string code, OutputFormat outputFormat)
        {
            string renderUri = renderUrlCalculator.GetRenderUrl(code, outputFormat);
            return new Uri(renderUri);
        }
    }
}

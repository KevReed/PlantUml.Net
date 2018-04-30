using System;
using System.Net.Http;
using PlantUml.Net.Java;
using PlantUml.Net.Tools;

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

        public string Render(string code, OutputFormat outputFormat)
        {
            string urlComponent = GetUrlComponent(code);
            string renderUrl = urlFormatMap.GetRenderUrl(urlComponent, outputFormat);

            using (HttpClient httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(renderUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    return result.Content.ReadAsStringAsync().Result;
                }

                throw new HttpRequestException(result.ReasonPhrase);
            }
        }

        private string GetUrlComponent(string code)
        {
            IProcessResult processResult = jarRunner.RunJarWithInput(code, "-encodeurl", "-pipe");

            if (processResult.ExitCode != 0)
            {
                throw new RenderingException(code, processResult.Error);
            }

            return processResult.Output;
        }
    }
}

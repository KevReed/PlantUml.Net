﻿using System;

namespace PlantUml.Net.Remote
{
    internal class RenderUrlCalculator
    {
        private readonly UrlFormatMap urlFormatMap;

        public RenderUrlCalculator(UrlFormatMap urlFormatMap)
        {
            this.urlFormatMap = urlFormatMap;
        }

        public Uri GetRenderUrl(string code, OutputFormat outputFormat)
        {
            string urlComponent = PlantUmlTextEncoding.EncodeUrl(code);
            return urlFormatMap.GetRenderUrl(urlComponent, outputFormat);
        }
    }
}

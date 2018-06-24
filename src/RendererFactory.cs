using System;
using PlantUml.Net.Java;
using PlantUml.Net.Local;
using PlantUml.Net.Remote;

namespace PlantUml.Net
{
    public class RendererFactory
    {
        public IPlantUmlRenderer CreateRenderer()
        {
            return CreateRenderer(new PlantUmlSettings());
        }

        public IPlantUmlRenderer CreateRenderer(PlantUmlSettings settings)
        {
            switch (settings.RenderingMode)
            {
                case RenderingMode.Remote:

                    UrlFormatMap urlFormatMap = new UrlFormatMap(settings.RemoteUrl);
                    return new RemotePlantUmlRenderer(urlFormatMap);

                case RenderingMode.Local:

                    JarRunner jarRunner = CreateJarRunner(settings);
                    LocalCommandProvider commandProvider = new LocalCommandProvider(settings.LocalGraphvizDotPath);
                    return new LocalPlantUmlRenderer(jarRunner, commandProvider);

                default:
                    throw new ArgumentException("invalid rendering mode", nameof(settings.RenderingMode));
            }
        }

        private static JarRunner CreateJarRunner(PlantUmlSettings settings)
        {
            IJavaLocator javaLocator = CreateJavaLocator(settings);
            string installationPath = javaLocator.GetJavaInstallationPath();
            return new JarRunner(installationPath, settings.LocalPlantUmlPath);
        }

        private static IJavaLocator CreateJavaLocator(PlantUmlSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.JavaPath))
            {
                return new EnvironmentJavaLocator();
            }
            else
            {
                return new ExplicitJavaLocator(settings.JavaPath);
            }
        }
    }
}

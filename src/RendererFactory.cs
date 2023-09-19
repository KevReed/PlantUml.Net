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
            UrlFormatMap urlFormatMap = new UrlFormatMap(settings.RemoteUrl);
            RenderUrlCalculator renderUrlCalculator = new RenderUrlCalculator(urlFormatMap);

            switch (settings.RenderingMode)
            {
                case RenderingMode.Remote:

                    return new RemotePlantUmlRenderer(renderUrlCalculator);

                case RenderingMode.Local:

                    JarRunner jarRunner = CreateJarRunner(settings);
                    LocalCommandProvider commandProvider = new LocalCommandProvider(settings);
                    return new LocalPlantUmlRenderer(jarRunner, settings.WorkingDirectory, commandProvider, renderUrlCalculator);

                default:
                    throw new ArgumentException("Invalid rendering mode", nameof(settings.RenderingMode));
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

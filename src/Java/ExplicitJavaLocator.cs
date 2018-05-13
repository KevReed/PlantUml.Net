namespace PlantUml.Net.Java
{
    internal class ExplicitJavaLocator: IJavaLocator
    {
        private readonly string javaPath;

        public ExplicitJavaLocator(string javaPath)
        {
            this.javaPath = javaPath;
        }

        public string GetJavaInstallationPath() => javaPath;        
    }
}

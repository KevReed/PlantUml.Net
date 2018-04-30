namespace PlantUml.Net.Java
{
    internal class ExplicitJavaLocator: IJavaLocator
    {
        private readonly string javaHome;

        public ExplicitJavaLocator(string javaHome)
        {
            this.javaHome = javaHome;
        }

        public string GetJavaInstallationPath() => javaHome;        
    }
}

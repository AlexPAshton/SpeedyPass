namespace SpeedyPass.Models
{
    public class ApplicationLoadedConfig
    {
        private string passwordDataPath;

        public string PasswordDataPath { get => this.passwordDataPath; set => this.passwordDataPath = value; }
    }
}

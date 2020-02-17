namespace SpeedyPass.Models
{
    public class ApplicationConfigModel
    {
        private string passwordDataPath;

        public string PasswordDataPath { get => this.passwordDataPath; set => this.passwordDataPath = value; }
    }
}

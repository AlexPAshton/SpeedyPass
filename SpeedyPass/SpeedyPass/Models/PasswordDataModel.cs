namespace SpeedyPass
{
    public class PasswordDataModel
    {
        private string domain;
        private string username;
        private string password;

        public string Domain { get => domain; set => domain = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}

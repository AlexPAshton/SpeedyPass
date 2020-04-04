namespace SpeedyPass.Models
{
    public class AppConfigModel
    {
        public delegate void UpdatedArgs();
        public event UpdatedArgs OnUpdated;

        public bool Setup
        {
            get => this.setup;
            set
            {
                this.setup = value;
                this.OnUpdated?.Invoke();
            }
        }
        public string DataPath
        {
            get => this.dataPath;
            set
            {
                this.dataPath = value;
                this.OnUpdated?.Invoke();
            }
        }
        public bool UseDirectInput
        {
            get => this.useDirectInput;
            set
            {
                this.useDirectInput = value;
                this.OnUpdated?.Invoke();
            }
        }
        public bool UseClipboardInput
        {
            get => this.useClipboardInput;
            set
            {
                this.useClipboardInput = value;
                this.OnUpdated?.Invoke();
            }
        }
        public bool UsePin
        {
            get => this.usePin;
            set
            {
                this.usePin = value;
                this.OnUpdated?.Invoke();
            }
        }
        public string Pin
        {
            get => this.pin;
            set
            {
                this.pin = value;
                this.OnUpdated?.Invoke();
            }
        }

        private bool setup;
        private string dataPath;
        private bool useDirectInput;
        private bool useClipboardInput;
        private bool usePin;
        private string pin;
    }
}

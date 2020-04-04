namespace SpeedyPass.ViewModels
{
    public class SetPinViewModel : ViewModel, ISetPinViewModel
    {
        public delegate void VerifyArgs(int index);
        public event VerifyArgs OnVerifyInput;

        public string InputChar0
        {
            get => this.inputChar0;
            set
            {
                this.SetField(ref this.inputChar0, value);
                this.OnVerifyInput?.Invoke(0);
            }
        }
        public string InputChar1
        {
            get => this.inputChar1;
            set
            {
                this.SetField(ref this.inputChar1, value);
                this.OnVerifyInput?.Invoke(1);
            }
            }
        public string InputChar2
        {
            get => this.inputChar2;
            set
            {
                this.SetField(ref this.inputChar2, value);
                this.OnVerifyInput?.Invoke(2);
            }
        }
        public string InputChar3
        {
            get => this.inputChar3;
            set
            {
                this.SetField(ref this.inputChar3, value);
                this.OnVerifyInput?.Invoke(3);
            }
        }
        public string InputChar0Validity
        {
            get => this.inputChar0Valid;
            set => this.SetField(ref this.inputChar0Valid, value);
        }
        public string InputChar1Validity
        {
            get => this.inputChar1Valid;
            set => this.SetField(ref this.inputChar1Valid, value);
        }
        public string InputChar2Validity
        {
            get => this.inputChar2Valid;
            set => this.SetField(ref this.inputChar2Valid, value);
        }
        public string InputChar3Validity
        {
            get => this.inputChar3Valid;
            set => this.SetField(ref this.inputChar3Valid, value);
        }
        public bool ContinueButtonEnabled
        {
            get { return this.continueButtonEnabled; }
            set { base.SetField(ref this.continueButtonEnabled, value); }
        }

        private string inputChar0;
        private string inputChar1;
        private string inputChar2;
        private string inputChar3;

        private string inputChar0Valid;
        private string inputChar1Valid;
        private string inputChar2Valid;
        private string inputChar3Valid;

        private bool continueButtonEnabled = false;
    }
}

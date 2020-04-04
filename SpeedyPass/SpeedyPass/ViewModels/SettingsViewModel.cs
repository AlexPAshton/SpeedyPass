using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyPass.ViewModels
{
    public class SettingsViewModel : ViewModel, ISettingsViewModel
    {
        public bool UseDirectPasswordInput 
        { 
            get => this.useDirectPasswordInput;
            set
            {
                this.SetField(ref this.useDirectPasswordInput, value);

                if (value == true)
                {
                    this.UseClipboardPasswordInput = false;
                }
                else if (value == false && this.UseClipboardPasswordInput == false)
                {
                    this.UseClipboardPasswordInput = true;
                }
            }
        } 
        public bool UseClipboardPasswordInput 
        { 
            get => this.useClipboardPasswordInput;
            set
            {
                this.SetField(ref this.useClipboardPasswordInput, value);

                if (value == true)
                {
                    this.UseDirectPasswordInput = false;
                }
                else if (value == false && this.UseDirectPasswordInput == false)
                {
                    this.UseDirectPasswordInput = true;
                }
            }
        }
        public bool ProtectPasswordsWithPIN 
        { 
            get => this.protectPasswordsWithPIN; 
            set => this.SetField(ref this.protectPasswordsWithPIN, value); 
        }
 
        private bool protectPasswordsWithPIN;
        private bool useDirectPasswordInput = true;
        private bool useClipboardPasswordInput;
    }
}

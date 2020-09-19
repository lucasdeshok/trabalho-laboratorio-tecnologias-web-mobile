using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action ShowWarningInvalidLogin;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;

        public int MyProperty { get; set; }
        //public string Email { get;  { return email; }
        //    set { PropertyChanged(this, new PropertyChangedEventArgs("Email")); }
        //}

    }
}

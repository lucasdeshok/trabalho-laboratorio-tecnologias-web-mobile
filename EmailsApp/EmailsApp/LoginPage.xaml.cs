using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmailsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public ICommand ForgotPasswordClick => new Command(OnForgotPassword);

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnForgotPassword()
        {
            Debug.WriteLine("Clicked");
            await DisplayAlert("Alert", "Label clicked", "OK");

        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryEmail.Text) || string.IsNullOrWhiteSpace(entryPassword.Text))
                DisplayAlert("Alert", "O email e senha precisam ser preenchidos.", "OK");

            Navigation.PushAsync(new MainPage());


        }
        private void LabelForgotPassword_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Forgot Password", "OK");
        }

        private void LabelCreateAccount_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Create Account", "OK");
        }
    }
}
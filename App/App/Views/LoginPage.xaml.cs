using App.Models;
using App.Repository;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(string email)
        {            
            InitializeComponent();
            entryEmail.Text = email;
        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryEmail.Text) | string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                labelInvalidEmail.IsVisible = true;
                labelInvalidPassword.IsVisible = true;
                return;
            }            

            labelInvalidEmail.IsVisible = false;
            labelInvalidPassword.IsVisible = false;

            var user = new User();
            var repository = new UserRepository();

            user.Email = entryEmail.Text;
            user.Password = entryPassword.Text;

            if(repository.Auth(user))
            {
                var name = repository.GetByEmail(user.Email).FirstName;

                entryEmail.Text = string.Empty;
                entryPassword.Text = string.Empty;

                Navigation.PushAsync(new MainPage(name));                
            }                
            else
            {
                DisplayAlert("Info", "Login invalid, email or password incorrect :(", "OK");
            }
        }

        private void LabelForgotPassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}
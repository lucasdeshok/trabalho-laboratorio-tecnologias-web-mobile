using App.Models;
using App.Repository;
using App.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        User user = new User();
        UserRepository repository = new UserRepository();

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
            labelInvalidEmail.IsVisible = false;
            labelInvalidPassword.IsVisible = false;

            if (string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                labelInvalidPassword.IsVisible = true;
            }

            if (string.IsNullOrWhiteSpace(entryEmail.Text) | !Email.SenacEmailIsValid(entryEmail.Text))
            {
                labelInvalidEmail.IsVisible = true;
                return;
            }

            var user = new User();
            var repository = new UserRepository();

            user.Email = entryEmail.Text;
            user.Password = entryPassword.Text;

            if(repository.Auth(user))
            {
                var userAuth = repository.Get(user.Email);

                entryEmail.Text = string.Empty;
                entryPassword.Text = string.Empty;

                Navigation.PushAsync(new MainPage(userAuth.FirstName, user.Email));                
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

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            var user = repository.Get(true);
            if (user == null)
                return;

            if (user.RememberMe)
            {
                Navigation.PushAsync(new MainPage(user.FirstName, user.Email));
            }
            
        }
    }
}
using App.Models;
using App.Repository;
using App.Service;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private bool ValidateEntrys()
        {
            if (string.IsNullOrWhiteSpace(entryFirstName.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(entryEmail.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                return false;
            }

            return true;
        }

        private void ButtonCreate_Clicked(object sender, EventArgs e)
        {
            var user = new User();
            var userRepository = new UserRepository();

            if (!string.IsNullOrWhiteSpace(entryEmail.Text) & !Email.SenacEmailIsValid(entryEmail.Text))
                return;

            user.FirstName = entryFirstName.Text;
            user.LastName = entryLastName.Text;
            user.Email = entryEmail.Text;
            user.Password = entryPassword.Text;

            var result = userRepository.CheckExists(user.Email);
            if (result)
            {
                DisplayAlert("Info", "User already registered :(", "OK");
                return;
            }

            if (userRepository.Create(user))
            {                
                DisplayAlert("Info", "User inserted successfully :)", "OK");
                
                user.FirstName = string.Empty; ;
                user.LastName = string.Empty; ;
                user.Email = string.Empty; ;
                user.Password = string.Empty;
                
                Navigation.PushAsync(new LoginPage(user.Email));

            }                
            else
            {
                DisplayAlert("Info", "Fail to insert user :(", "OK");
            }                        
        }
    }
}
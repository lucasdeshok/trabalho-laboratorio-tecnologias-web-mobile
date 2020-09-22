using App.Models;
using App.Repository;
using App.Service;
using System;
using System.Data.SqlTypes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        User user = new User();
        UserRepository repository = new UserRepository();

        public CreateAccountPage()
        {
            InitializeComponent();            
        }

        private void EntryFirstName_Unfocused(object sender, FocusEventArgs e)
        {
            labelInvalidFirstName.IsVisible = false;

            if (string.IsNullOrWhiteSpace(entryFirstName.Text))
            {
                labelInvalidFirstName.IsVisible = true;
            }
        }

        private void ButtonSave_Clicked(object sender, EventArgs e)
        {           
            if (string.IsNullOrWhiteSpace(entryEmail.Text) |
                string.IsNullOrWhiteSpace(entryPassword.Text) |
                string.IsNullOrWhiteSpace(entryFirstName.Text) |
                !Email.SenacEmailIsValid(entryEmail.Text))
                return;

            var result = repository.Get(user.Email);
            if (result != null)
            {
                DisplayAlert("Info", "User already registered :(", "OK");
                return;
            }

            user.FirstName = entryFirstName.Text;
            user.LastName = entryLastName.Text;
            user.Email = entryEmail.Text;
            user.Password = entryPassword.Text;

            if (repository.Save(user))
            {
                DisplayAlert("Info", "User save successfully :)", "OK");

                entryEmail.Text = string.Empty;
                entryLastName.Text = string.Empty;
                entryPassword.Text = string.Empty;
                entryFirstName.Text = string.Empty;

                Navigation.PushAsync(new LoginPage(user.Email));
            }
            else
            {
                DisplayAlert("Info", "Fail to save user :(", "OK");
            }
        }

        private void EntryEmail_Unfocused(object sender, FocusEventArgs e)
        {
            labelInvalidEmail.IsVisible = false;

            if (string.IsNullOrWhiteSpace(entryEmail.Text))
            {
                labelInvalidEmail.IsVisible = true;
            }
        }

        private void EntryPassword_Unfocused(object sender, FocusEventArgs e)
        {
            labelInvalidPassword.IsVisible = false;

            if (string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                labelInvalidPassword.IsVisible = true;
            }
        }
    }
}
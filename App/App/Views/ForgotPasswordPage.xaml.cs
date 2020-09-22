using App.Models;
using App.Repository;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        User user = new User();
        UserRepository repository = new UserRepository();

        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private void ButtonRecover_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryEmail.Text))
                return;

            user = repository.Get(entryEmail.Text);
            
            user.Password = "1111";
            user.ForgotPassword = true;
            user.UpdatedAt = DateTime.Now;

            var result = repository.Save(user);

            if (result)
            {
                DisplayAlert("Info", "password reseted, your new password is 1111 :)", "OK");
                Navigation.PushAsync(new LoginPage(user.Email));                
            }
        }
    }
}
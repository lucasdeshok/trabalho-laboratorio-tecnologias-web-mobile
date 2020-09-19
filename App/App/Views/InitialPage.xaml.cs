using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitialPage : ContentPage
    {
        public InitialPage()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void LabelCreateAccount_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountPage());
        }
    }
}
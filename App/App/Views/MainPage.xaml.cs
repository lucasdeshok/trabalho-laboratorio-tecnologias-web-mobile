using Android.App.Admin;
using App.Models;
using App.Repository;
using App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace App
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {        
        User userAuth = new User();
        Recipe recipe = new Recipe();
        List<Recipe> recipes = new List<Recipe>();
        UserRepository userRepository = new UserRepository();
        RecipeRepository recipeRepository = new RecipeRepository();

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(string firstName, string email)
        {
            InitializeComponent();
            
            userAuth.FirstName = firstName;
            userAuth.Email = email;

            if (!string.IsNullOrWhiteSpace(userAuth.FirstName))
                labelTitle.Text = string.Format("Hello, {0}.\nHere, your saved recipes.", userAuth.FirstName);
        }

        async void ContentPage_Appearing(object sender, EventArgs e)
        {           
            stackLayoutOfLabelMessage.IsVisible = false;
            stackLayoutActivityIndicator.IsVisible = false;

            recipes = recipeRepository.GetAll().ToList();

            stackLayoutOfLabelMessage.IsVisible = false;

            if (recipes.Count > 0)
            {
                listViewRecipes.ItemsSource = recipes;
            }
            else
            {
                stackLayoutOfLabelMessage.IsVisible = true;
            }

            userAuth = userRepository.Get(userAuth.Email);
            if (userAuth.ForgotPassword)
            {
                string newPassword = await DisplayPromptAsync("Forgot password", "What's your new password?");

                if (string.IsNullOrEmpty(newPassword))
                {
                    await DisplayAlert("Info", "You not input a new password, so will be set default 1111", "OK");
                    userAuth.Password = "1111";
                    userAuth.ForgotPassword = false;
                    userAuth.UpdatedAt = DateTime.Now;
                    userRepository.Save(userAuth);
                }
                else
                {
                    userAuth.ForgotPassword = false;
                    userAuth.Password = newPassword;                                        
                    userAuth.UpdatedAt = DateTime.Now;
                    userRepository.Save(userAuth);
                    await DisplayAlert("Info", "Great, your new password is saved", "OK");
                }                
            }       
        }

        private void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipeRegisterPage());
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listViewRecipes.ItemsSource = null;
            listViewRecipes.ItemsSource = recipes.Where(x => x.Name.ToLower().Contains(e.NewTextValue));
        }

        private void ListViewRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                DisplayAlert("Info", "Hey, please select an item :)", "OK");
                return;
            }

            recipe = (e.SelectedItem as Recipe);
            Navigation.PushAsync(new RecipeDetailPage(recipe.Id));
        }
    }
}

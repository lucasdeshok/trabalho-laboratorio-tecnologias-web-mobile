using App.Models;
using App.Repository;
using App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeRegisterPage : ContentPage
    {
        Recipe recipe = new Recipe();
        List<Recipe> recipes = new List<Recipe>();
        RecipeRepository repository = new RecipeRepository();


        public RecipeRegisterPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            var stateConnection = Connectivity.NetworkAccess;
            stackLayoutOfLabelMessage.IsVisible = false;

            if (stateConnection != NetworkAccess.Internet)
            {
                stackLayoutOfLabelMessage.IsVisible = true;
                labelMessage.Text = "Internet unavailable :(";
                return;
            }

            activityIndicator.IsRunning = true;
            recipes = Request.RecipesGetFromApi();

            listViewRecipes.ItemsSource = recipes;
            activityIndicator.IsRunning = false;
        }

        private void ListViewRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            recipe = (e.SelectedItem as Recipe);
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listViewRecipes.ItemsSource = null;
            listViewRecipes.ItemsSource = recipes.Where(i => i.Name.Contains(e.NewTextValue));
        }

        private void TapGestureRecognizer_Tapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                DisplayAlert("Info", "Hey, please select an item :)", "OK");
                return;
            }

            recipe = (e.SelectedItem as Recipe);
            Navigation.PushAsync(new RecipeDetailPage(recipe));
        }

        private void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            bool result = repository.CheckExists(recipe);
            
            if (result)
            {
                DisplayAlert("Info", "Recipes already registered :(", "OK");
                return;
            }

            if (recipe.Id.Length > 0)
            {
                result = repository.Save(recipe);
            }

            if (result)
            {
                DisplayAlert("Info", "Recipe save success :)", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Info", "Something wrong :(", "OK");
                return;
            }

        }
    }
}
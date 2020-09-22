using App.Models;
using App.Repository;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        Recipe recipe = new Recipe();
        RecipeRepository repository = new RecipeRepository();


        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        public RecipeDetailPage(Recipe recipe)
        {
            InitializeComponent();

            listViewIngredients.ItemsSource = null;
            listViewPreparationMode.ItemsSource = null;

            this.recipe = recipe;
            labelNameOfRecipe.Text = recipe.Name;

            listViewIngredients.ItemsSource = recipe.Ingredient;
            listViewPreparationMode.ItemsSource = recipe.PreparationMode;

            if (recipe.IsFavorite)
            {
                buttonUnfavorite.IsVisible = true;
                buttonFavorite.IsVisible = false;
            }
            else
            {
                buttonUnfavorite.IsVisible = false;
                buttonFavorite.IsVisible = true;
            }
        }

        public RecipeDetailPage(string recipeId)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(recipeId))
                return;

            listViewIngredients.ItemsSource = null;
            listViewPreparationMode.ItemsSource = null;

            recipe = repository.GetDetails(recipeId);
            labelNameOfRecipe.Text = recipe.Name;
            listViewIngredients.ItemsSource = recipe.Ingredient;
            listViewPreparationMode.ItemsSource = recipe.PreparationMode;

            if (recipe.IsFavorite)
            {
                buttonUnfavorite.IsVisible = true;
                buttonFavorite.IsVisible = false;
            }
            else
            {
                buttonUnfavorite.IsVisible = false;
                buttonFavorite.IsVisible = true;
            }
        }

        private void ButtonFavorite_Clicked(object sender, System.EventArgs e)
        {            
            recipe.IsFavorite = true;
            recipe.UpdateAt = DateTime.Now;

            var result = repository.Save(recipe);

            if (result)
            {
                DisplayAlert("Info", "Recipe set as with favorite :)", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Info", "Something wrong :(", "OK");
            }
        }

        private void ButtonUnfavorite_Clicked(object sender, System.EventArgs e)
        {
            recipe.IsFavorite = false;
            recipe.UpdateAt = DateTime.Now;

            var result = repository.Save(recipe);

            if (result)
            {
                DisplayAlert("Info", "Recipe set as with unfavorite :)", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Info", "Something wrong :(", "OK");
            }
        }
    }
}
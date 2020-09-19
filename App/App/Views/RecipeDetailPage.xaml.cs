using App.Models;
using App.Repository;
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
        }

        private void ButtonFavorite_Clicked(object sender, System.EventArgs e)
        {
            recipe.Favorite.RecipeId = recipe.Id;
            recipe.Favorite.IsFavorite = true;
            var result = repository.SetFavorite(recipe);

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
    }
}
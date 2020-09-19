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
        Recipe recipe = new Recipe();
        List<Recipe> recipes = new List<Recipe>();

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(string name)
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(name))
                labelMessageUser.Text = string.Format("Hello, {0}.\nHere, your saved recipes.", name);
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            var repository = new RecipeRepository();            
            recipes = repository.GetAll().ToList();

            stackLayoutOfLabelMessage.IsVisible = false;

            if (recipes.Count > 0)
            {
                listViewRecipes.ItemsSource = recipes;
            }
            else
            {
                stackLayoutOfLabelMessage.IsVisible = true;                
            }           
        }

        private void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipeRegisterPage());
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listViewRecipes.ItemsSource = null;
            listViewRecipes.ItemsSource = recipes.Where(i => i.Name.Contains(e.NewTextValue));
        }
    }
}

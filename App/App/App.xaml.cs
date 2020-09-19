using Xamarin.Forms;


namespace App
{
    public partial class App : Application
    {
        public static string DatabasePath = string.Empty;

        public App()
        {
            InitializeComponent();            
            MainPage = new NavigationPage(new LoginPage());
        }

        public App(string databasePath)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new InitialPage());

            DatabasePath = databasePath;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

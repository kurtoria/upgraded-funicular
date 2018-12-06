using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Yodaz.View;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Yodaz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

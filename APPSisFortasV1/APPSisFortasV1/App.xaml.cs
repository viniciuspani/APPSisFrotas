using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPSisFortasV1
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new APPSisFortasV1.Pages.Login());
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

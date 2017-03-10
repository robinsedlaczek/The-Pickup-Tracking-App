using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fairmas.PickupTracking.TheUniversalApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreen : Page
    {
        public SplashScreen()
        {
            InitializeComponent();

            ShowThisCustomSplashScreen();
        }

        private async Task ShowThisCustomSplashScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));

            Frame.Navigate(typeof(LoginPage));
        }
    }
}

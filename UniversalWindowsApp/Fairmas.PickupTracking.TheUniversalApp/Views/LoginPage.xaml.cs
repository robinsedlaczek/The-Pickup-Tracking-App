using Fairmas.PickupTracking.Shared.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fairmas.PickupTracking.TheUniversalApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            DataContext = ViewModelLocator.LoginViewModel;


        }

        private void OnBackRequested(System.Object sender, BackRequestedEventArgs e)
        {
            CoreApplication.Exit();

            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            var loginViewModel = DataContext as LoginViewModel;
            loginViewModel.IsLoggedIn = false;
            loginViewModel.PropertyChanged += OnViewModelPropertyChanged;

            base.OnNavigatedTo(e);
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var loginViewModel = DataContext as LoginViewModel;

            if(e.PropertyName == nameof(LoginViewModel.IsLoggedIn) && loginViewModel.IsLoggedIn)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(HotelListPage));
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;

            var loginViewModel = DataContext as LoginViewModel;
            loginViewModel.PropertyChanged -= OnViewModelPropertyChanged;

            base.OnNavigatingFrom(e);
        }
    }
}

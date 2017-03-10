using Fairmas.PickupTracking.Shared.ViewModels;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        
        public LoginPage()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.LoginViewModel;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;

            BindingContext = viewModel;
        }


        public async void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var loginViewModel = BindingContext as LoginViewModel;

            if (e.PropertyName == nameof(LoginViewModel.IsLoggedIn) && loginViewModel.IsLoggedIn)
            {
                Navigation.InsertPageBefore(new HotelListPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
}

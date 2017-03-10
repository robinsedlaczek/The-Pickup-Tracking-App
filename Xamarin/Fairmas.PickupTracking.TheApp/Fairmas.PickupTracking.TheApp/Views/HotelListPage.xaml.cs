using System;
using Xamarin.Forms;
using Fairmas.PickupTracking.Shared.ViewModels;

namespace Fairmas.PickupTracking.TheApp.Views
{
    public partial class HotelListPage : ContentPage
    {
        //public ObservableCollection<HotelViewModel> HotelViewModels { get; set; }

        public HotelListPage()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.HotelListViewModel;
            BindingContext = viewModel;
            Title = "Hotel List";

            
            PropertyListView.ItemsSource = viewModel.Hotels;

            viewModel.PropertyChanged += OnItemSelected;
        }

        protected override void OnBindingContextChanged()
        {
            var viewModel = BindingContext as HotelListViewModel;
            if (viewModel == null)
                return;

            if(viewModel.LoadHotelListCommand.CanExecute(viewModel))
                viewModel.LoadHotelListCommand.Execute(viewModel);

            base.OnBindingContextChanged();
        }

        public async void OnItemSelected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HotelListViewModel.SelectedHotel))
            {
                var viewModel = BindingContext as HotelListViewModel;
                var selectedProperty = viewModel.SelectedHotel;

                await Navigation.PushAsync(new NavigationPage(new MonthlyPickupSummaryPage(selectedProperty)));
            }

        }
    }
}

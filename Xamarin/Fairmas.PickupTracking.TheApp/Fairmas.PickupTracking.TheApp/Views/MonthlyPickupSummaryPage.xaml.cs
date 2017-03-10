
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Fairmas.PickupTracking.Shared.ViewModels;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Views
{
    public partial class MonthlyPickupSummaryPage : ContentPage
    {
        public PickupFiguresViewModel SelectedPickup { get; set; }



        public MonthlyPickupSummaryPage(HotelViewModel hotel)
        {
            InitializeComponent();
            Title = hotel.Name;
            
            var viewModel = ViewModelLocator.MonthlyPickupSummaryViewModel;
            viewModel.SelectedProperty = hotel;
            
            DataList.ItemsSource = viewModel.Pickup;

            BindingContext = viewModel;

            HeaderGrid.ColumnSpacing = 1;
            HeaderGrid.RowSpacing = 1;
        }


        protected override void OnBindingContextChanged()
        {
            var viewModel = GetViewModel();

            if (viewModel == null)
                return;

            ExecuteDataLoadCommand(viewModel);
            base.OnBindingContextChanged();
        }

        /// <summary>
        /// register events
        /// display navigation bar when navigating from daily page
        /// </summary>
        protected override void OnAppearing()
        {
            var viewModel = GetViewModel();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.SettingsViewModel.PropertyChanged += ViewModel_PropertyChanged;

            NavigationPage.SetHasNavigationBar(Parent, true);
            base.OnAppearing();
        }

        /// <summary>
        /// disable events
        /// hide navigation bar of this page when displaying next page
        /// </summary>
        protected override void OnDisappearing()
        {
            var viewModel = GetViewModel();
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            viewModel.SettingsViewModel.PropertyChanged -= ViewModel_PropertyChanged;

            NavigationPage.SetHasNavigationBar(Parent, false);
            base.OnDisappearing();
        }

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MonthlyPickupSummaryViewModel.SelectedMonth):
                    await NavigateToDailyPage();
                    return;
                case nameof(SettingsViewModel.PickupDate):
                case nameof(SettingsViewModel.ReportDate):
                    ExecuteDataLoadCommand(GetViewModel());
                    return;
            }
        }


        private MonthlyPickupSummaryViewModel GetViewModel()
        {
            return BindingContext as MonthlyPickupSummaryViewModel;
        }

        private async Task NavigateToDailyPage()
        {
            var viewModel = GetViewModel();
            await Navigation.PushAsync(new NavigationPage(new DailyPickupSummaryPage(viewModel)));
        }

        private static void ExecuteDataLoadCommand(MonthlyPickupSummaryViewModel viewModel)
        {
            if (viewModel.LoadMonthlyPickupSummaryCommand.CanExecute(viewModel))
                viewModel.LoadMonthlyPickupSummaryCommand.Execute(viewModel);
        }
    }
}

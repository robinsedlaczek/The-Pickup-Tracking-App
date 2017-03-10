using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fairmas.PickupTracking.Shared.ViewModels;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Views
{
    public partial class DailyPickupSummaryPage : ContentPage
    {
        public DailyPickupSummaryPage(MonthlyPickupSummaryViewModel monthlyPickupSummaryViewModel)
        {
            InitializeComponent();

            if (!monthlyPickupSummaryViewModel.SelectedMonth.BusinessDate.HasValue || monthlyPickupSummaryViewModel.SelectedProperty == null)
            {
                return;
            }

            Title = $"{monthlyPickupSummaryViewModel.SelectedProperty.Name} - {monthlyPickupSummaryViewModel.SelectedMonth.BusinessDate.Value:Y}";

            var viewModel = ViewModelLocator.DailyPickupSummaryViewModel;
            viewModel.SelectedProperty = monthlyPickupSummaryViewModel.SelectedProperty;
            viewModel.SelectedMonth = monthlyPickupSummaryViewModel.SelectedMonth;

            DataList.ItemsSource = viewModel.Pickup;

            BindingContext = viewModel;
        }

        protected override void OnBindingContextChanged()
        {
            var viewModel = GetViewModel();
            if (viewModel == null)
                return;

            ExecuteDataLoadCommand(viewModel);
            base.OnBindingContextChanged();
        }

        protected override void OnAppearing()
        {
            var viewModel = GetViewModel();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.SettingsViewModel.PropertyChanged += ViewModel_PropertyChanged;

            NavigationPage.SetHasNavigationBar(Parent, true);
            base.OnAppearing();
        }

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
                case nameof(DailyPickupSummaryViewModel.SelectedDay):
                    await NavigateToSegmentPage();
                    return;
                case nameof(SettingsViewModel.PickupDate):
                case nameof(SettingsViewModel.ReportDate):
                    ExecuteDataLoadCommand(GetViewModel());
                    return;
            }
        }


        private DailyPickupSummaryViewModel GetViewModel()
        {
            return BindingContext as DailyPickupSummaryViewModel;
        }


        private async Task NavigateToSegmentPage()
        {
            var viewModel = GetViewModel();
            await Navigation.PushAsync(new NavigationPage(new SegmentedPickupPage(viewModel)));
        }

        private void ExecuteDataLoadCommand(DailyPickupSummaryViewModel viewModel)
        {
            if(viewModel.LoadDailyPickupSummaryCommand.CanExecute(viewModel))
                viewModel.LoadDailyPickupSummaryCommand.Execute(viewModel);
        }
    }
}

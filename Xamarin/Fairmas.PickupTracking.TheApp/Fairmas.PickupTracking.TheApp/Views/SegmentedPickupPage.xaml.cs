using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fairmas.PickupTracking.Shared.ViewModels;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Views
{
    public partial class SegmentedPickupPage : ContentPage
    {
        public SegmentedPickupPage(DailyPickupSummaryViewModel dailyPickupSummaryViewModel)
        {
            InitializeComponent();

            if (!dailyPickupSummaryViewModel.SelectedMonth.BusinessDate.HasValue 
                || dailyPickupSummaryViewModel.SelectedProperty == null
                || dailyPickupSummaryViewModel.SelectedDay == null)
                return;

            Title = $"{dailyPickupSummaryViewModel.SelectedProperty.Name} - {dailyPickupSummaryViewModel.SelectedMonth.BusinessDate.Value:D}";

            var viewModel = ViewModelLocator.SegmentedPickupViewModel;
            viewModel.SelectedProperty = dailyPickupSummaryViewModel.SelectedProperty;
            viewModel.SelectedMonth = dailyPickupSummaryViewModel.SelectedMonth;
            viewModel.SelectedDay = dailyPickupSummaryViewModel.SelectedDay;

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
            NavigationPage.SetHasNavigationBar(Parent, true);
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            NavigationPage.SetHasNavigationBar(Parent, false);
            base.OnDisappearing();
        }



        private SegmentedPickupViewModel GetViewModel()
        {
            return BindingContext as SegmentedPickupViewModel;
        }

        private void ExecuteDataLoadCommand(SegmentedPickupViewModel viewModel)
        {
            if (viewModel.LoadSegmentedPickupCommand.CanExecute(viewModel))
                viewModel.LoadSegmentedPickupCommand.Execute(viewModel);
        }
    }
}

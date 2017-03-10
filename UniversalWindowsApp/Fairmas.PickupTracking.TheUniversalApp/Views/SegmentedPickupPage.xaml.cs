using Fairmas.PickupTracking.Shared.ViewModels;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fairmas.PickupTracking.TheUniversalApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SegmentedPickupPage : Page
    {
        public SegmentedPickupPage()
        {
            var viewModel = ViewModelLocator.SegmentedPickupViewModel;

            DataContext = viewModel;

            InitializeComponent();
        }

        private void OnBackRequested(Object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();

            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

                var dailyPickupSummaryViewModel = e.Parameter as DailyPickupSummaryViewModel;
                var viewModel = DataContext as SegmentedPickupViewModel;

                viewModel.SelectedProperty = dailyPickupSummaryViewModel.SelectedProperty;
                viewModel.SelectedMonth = dailyPickupSummaryViewModel.SelectedMonth;
                viewModel.SelectedDay = dailyPickupSummaryViewModel.SelectedDay;

                if (viewModel.LoadSegmentedPickupCommand.CanExecute(viewModel))
                    viewModel.LoadSegmentedPickupCommand.Execute(viewModel);

                base.OnNavigatedTo(e);

                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SelectedDay");

                if (animation != null)
                    animation.TryStart(SegmentedFiguresListView);
            }
            catch (Exception exception)
            {
                // TODO: [RS] Handle exception while page navigation.
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;

            base.OnNavigatedFrom(e);
        }

    }
}

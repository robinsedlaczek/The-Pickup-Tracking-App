using Fairmas.PickupTracking.Shared.ViewModels;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fairmas.PickupTracking.TheUniversalApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DailyPickupSummaryPage : Page
    {
        public DailyPickupSummaryPage()
        {
            var viewModel = ViewModelLocator.DailyPickupSummaryViewModel;

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

                var monthlyPickupSummaryViewModel = e.Parameter as MonthlyPickupSummaryViewModel;
                var viewModel = DataContext as DailyPickupSummaryViewModel;

                viewModel.SelectedProperty = monthlyPickupSummaryViewModel.SelectedProperty;
                viewModel.SelectedMonth = monthlyPickupSummaryViewModel.SelectedMonth;

                if (viewModel.LoadDailyPickupSummaryCommand.CanExecute(viewModel))
                    viewModel.LoadDailyPickupSummaryCommand.Execute(viewModel);

                viewModel.PropertyChanged += OnViewModelPropertyChanged;

                base.OnNavigatedTo(e);

                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SelectedMonth");

                if (animation != null)
                    animation.TryStart(DailyFiguresListView);
            }
            catch (Exception exception)
            {
                // TODO: [RS] Handle exception while page navigation.
            }
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DailyPickupSummaryViewModel.SelectedDay))
            {
                var viewModel = DataContext as DailyPickupSummaryViewModel;

                // [RS] In the case when the selected day is null, we will not navigate to the segmented pickup view.
                //      Simply because we do not know for which day data should be shown. 
                //      The selected day can become null here because it will be set by the data binding if the list
                //      gets cleared for example.
                if (viewModel.SelectedDay == null)
                    return;

                var frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(SegmentedPickupPage), viewModel);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;

            var viewModel = DataContext as DailyPickupSummaryViewModel;
            viewModel.PropertyChanged -= OnViewModelPropertyChanged;

            base.OnNavigatingFrom(e);
        }
    }
}

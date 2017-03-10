using Fairmas.PickupTracking.Shared.ViewModels;
using Fairmas.PickupTracking.TheUniversalApp.Helper;
using System;
using System.Linq;
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
    public sealed partial class MonthlyPickupSummaryPage : Page
    {
        public MonthlyPickupSummaryPage()
        {
            var viewModel = ViewModelLocator.MonthlyPickupSummaryViewModel;

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
            InitializeViewModel(e);
            RegisterEventHandler();

            base.OnNavigatedTo(e);

            StartPageTransitionAnimation();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            UnregisterEventHandler();
            PreparePageTransitionAnimation();

            base.OnNavigatingFrom(e);
        }

        private void InitializeViewModel(NavigationEventArgs e)
        {
            var selectedProperty = e.Parameter as HotelViewModel;
            var viewModel = DataContext as MonthlyPickupSummaryViewModel;

            viewModel.SelectedProperty = selectedProperty;

            if (viewModel.LoadMonthlyPickupSummaryCommand.CanExecute(viewModel))
                viewModel.LoadMonthlyPickupSummaryCommand.Execute(viewModel);
        }

        private void RegisterEventHandler()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            var viewModel = DataContext as MonthlyPickupSummaryViewModel;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MonthlyPickupSummaryViewModel.SelectedMonth))
            {
                var viewModel = DataContext as MonthlyPickupSummaryViewModel;

                // [RS] In the case when the selected month is null, we will not navigate to the daily pickup summary view.
                //      Simply because we do not know for which month data should be shown. 
                //      The selected month can become null here because it will be set by the data binding if the list
                //      gets cleared for example.
                if (viewModel.SelectedMonth == null)
                    return;

                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(DailyPickupSummaryPage), viewModel);
            }
        }

        private void UnregisterEventHandler()
        {
            // Event Handler Deregistration
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;

            var viewModel = DataContext as MonthlyPickupSummaryViewModel;
            viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        private void PreparePageTransitionAnimation()
        {
            try
            {
                var items = MonthlyFiguresListView.GetListViewItems().ToArray();
                var selectedItemExists = items != null && items.Count() > 0 && MonthlyFiguresListView.SelectedIndex > -1;

                var sourceUIElement = selectedItemExists
                    ? items[MonthlyFiguresListView.SelectedIndex] as UIElement
                    : MonthlyFiguresListView as UIElement;

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("SelectedMonth", sourceUIElement);
            }
            catch
            {
                // [RS] It's not that important if the page transition animation does not work. So we do nothing here.
                //      TODO: We should log it somehow...
            }
        }

        private void StartPageTransitionAnimation()
        {
            try
            {
                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SelectedProperty");

                if (animation != null)
                    animation.TryStart(MonthlyFiguresListView);
            }
            catch
            {
                // [RS] It's not that important if the page transition animation does not work. So we do nothing here.
                //      TODO: We should log it somehow...
            }
        }

    }
}

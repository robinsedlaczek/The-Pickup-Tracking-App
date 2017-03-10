using Fairmas.PickupTracking.Shared.ViewModels;
using Fairmas.PickupTracking.TheUniversalApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fairmas.PickupTracking.TheUniversalApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HotelListPage : Page
    {
        public HotelListPage()
        {
            InitializeComponent();

            DataContext = ViewModelLocator.HotelListViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            var viewModel = DataContext as HotelListViewModel;

            if (viewModel.LoadHotelListCommand.CanExecute(viewModel))
                viewModel.LoadHotelListCommand.Execute(viewModel);

            viewModel.PropertyChanged += OnViewModelPropertyChanged;

            base.OnNavigatedTo(e);

            // Page Transition Animation
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SelectedMonth");

            if (animation != null)
                animation.TryStart(PropertyListView as UIElement);
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HotelListViewModel.SelectedHotel))
            {
                var viewModel = DataContext as HotelListViewModel;
                var selectedProperty = viewModel.SelectedHotel;

                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MonthlyPickupSummaryPage), selectedProperty);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // Page Transition Animation
            var items = PropertyListView.GetListViewItems().ToArray();
            var selectedItem = items[PropertyListView.SelectedIndex];
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("SelectedProperty", selectedItem);

            // Event Handler Registration
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;

            var viewModel = DataContext as HotelListViewModel;
            viewModel.PropertyChanged -= OnViewModelPropertyChanged;

            base.OnNavigatingFrom(e);
        }

        private void OnBackRequested(Object sender, BackRequestedEventArgs e)
        {
            CoreApplication.Exit();

            e.Handled = true;
        }
    }
}

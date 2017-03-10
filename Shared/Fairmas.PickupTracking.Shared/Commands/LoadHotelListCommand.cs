using Fairmas.PickupTracking.Shared.Services;
using Fairmas.PickupTracking.Shared.ViewModels;
using System;
using System.Windows.Input;

namespace Fairmas.PickupTracking.Shared.Commands
{
    internal class LoadHotelListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is HotelListViewModel;
        }

        public async void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                throw new ArgumentException("The parameter must be a hotel list view model.");

            var viewModel = parameter as HotelListViewModel;

            try
            {
                viewModel.ErrorMessage = string.Empty;
                viewModel.Hotels.Clear();

                var hotels = await PickupConnector.Default.DataService.GetHotelsAsync();

                // TODO: [RS] Empty hotel list ?? Handle it more elegant.
                if (hotels == null)
                    throw new Exception("Sorry, we could not find any hotel at the server.");

                foreach (var hotel in hotels)
                {
                    var hotelViewModel = new HotelViewModel()
                    {
                        Id = hotel.Id,
                        Guid = hotel.Guid,
                        Name = hotel.Name,
                        City = hotel.City
                    };

                    viewModel.Hotels.Add(hotelViewModel);
                }
            }
            catch (Exception exception)
            {
                var error = $"Could not load hotel list:{Environment.NewLine}{exception.Message}";

                viewModel.ErrorMessage = error;
                viewModel.ShowErrorMessage = true;
            }
        }
    }
}
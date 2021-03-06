﻿using Fairmas.PickupTracking.Shared.Services;
using Fairmas.PickupTracking.Shared.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace Fairmas.PickupTracking.Shared.Commands
{
    internal class LoadMonthlyPickupSummaryCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is MonthlyPickupSummaryViewModel;
        }

        public async void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                throw new ArgumentException("Parameter must be a monthly pickup summary view model.");

            var viewModel = parameter as MonthlyPickupSummaryViewModel;

            try
            {
                viewModel.IsLoading = true;
                viewModel.ErrorMessage = string.Empty;

                var summary = await PickupConnector.Default.DataService.GetMonthlyPickupSummaryAsync(viewModel.SelectedProperty.Guid, viewModel.PickupFrom, viewModel.PickupTo);

                if (summary == null)
                    return;

                // TODO: [RS] Update existing list items rather then clearing the whole list. Prevents flickering.
                viewModel.Pickup.Clear();



                var list = summary
                    .OrderBy(model => model.BusinessDate)
                    .ToList();

                foreach (var figures in list)
                {
                    viewModel.Pickup.Add(
                        new PickupFiguresViewModel()
                        {
                            BusinessDate = figures.BusinessDate,
                            Occ = figures.Occupancy != null ? figures.Occupancy.SnapshotOneValue.HasValue ? $"{Math.Round(figures.Occupancy.SnapshotOneValue.Value, 0)} %" : "0 %" : "0 %",
                            RN = figures.RoomNights != null ? figures.RoomNights.SnapshotOneValue.HasValue ? figures.RoomNights.SnapshotOneValue.Value : 0m : 0m,
                            ADR = figures.AverageDailyRate != null ? figures.AverageDailyRate.SnapshotOneValue.HasValue ? Math.Round(figures.AverageDailyRate.SnapshotOneValue.Value, 2) : 0m : 0m,
                            Rev = figures.Revenue != null ? figures.Revenue.SnapshotOneValue.HasValue ? Math.Round(figures.Revenue.SnapshotOneValue.Value / 1000m, 1) : 0m : 0m,
                            PURN = figures.RoomNights != null ? figures.RoomNights.PickupValue.HasValue ? figures.RoomNights.PickupValue.Value : 0m : 0m,
                            PUADR = figures.AverageDailyRate != null ? figures.AverageDailyRate.PickupValue.HasValue ? Math.Round(figures.AverageDailyRate.PickupValue.Value, 0) : 0m : 0m,
                            PURev = figures.Revenue != null ? figures.Revenue.PickupValue.HasValue ? Math.Round(figures.Revenue.PickupValue.Value / 1000m, 2) : 0m : 0m,
                        });
                }
            }
            catch (Exception exception)
            {
                var error = $"Could not load monthly pickup summary:{Environment.NewLine}{exception.Message}";

                viewModel.ErrorMessage = error;
                viewModel.ShowErrorMessage = true;
            }
            finally
            {
                viewModel.IsLoading = false;
            }
        }
    }
}
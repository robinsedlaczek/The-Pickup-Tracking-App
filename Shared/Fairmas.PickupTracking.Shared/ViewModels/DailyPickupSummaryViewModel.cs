using Fairmas.PickupTracking.Shared.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class DailyPickupSummaryViewModel : SettingsViewModelBase
    {
        private HotelViewModel _selectedProperty;
        private DateTime _pickupFrom = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0));
        private DateTime _pickupTo = DateTime.Today;
        private PickupFiguresViewModel _selectedDay;
        private PickupFiguresViewModel _selectedMonth;

        public DailyPickupSummaryViewModel()
            : base(ViewModelLocator.SettingsViewModel)
        {
            Pickup = new ObservableCollection<PickupFiguresViewModel>();
        }

        public ICommand LoadDailyPickupSummaryCommand
        {
            get
            {
                return new LoadDailyPickupSummaryCommand();
            }
        }

        public HotelViewModel SelectedProperty
        {
            get
            {
                return _selectedProperty;
            }

            set
            {
                Set(ref _selectedProperty, value, nameof(SelectedProperty));
            }
        }

        public PickupFiguresViewModel SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }

            set
            {
                Set(ref _selectedMonth, value, nameof(SelectedMonth));
            }
        }

        public PickupFiguresViewModel SelectedDay
        {
            get
            {
                return _selectedDay;
            }
            
            set
            {
                Set(ref _selectedDay, value, nameof(SelectedDay));
            }
        }

        public ObservableCollection<PickupFiguresViewModel> Pickup
        {
            get;
        }

        public DateTime PickupFrom
        {
            get
            {
                return _pickupFrom.Date;
            }

            set
            {
                Set(ref _pickupFrom, value, nameof(Pickup));

                if (LoadDailyPickupSummaryCommand.CanExecute(this))
                    LoadDailyPickupSummaryCommand.Execute(this);
            }
        }

        public DateTime PickupTo
        {
            get
            {
                return _pickupTo.Date;
            }

            set
            {
                Set(ref _pickupTo, value, nameof(PickupTo));

                if (LoadDailyPickupSummaryCommand.CanExecute(this))
                    LoadDailyPickupSummaryCommand.Execute(this);
            }
        }

    }
}

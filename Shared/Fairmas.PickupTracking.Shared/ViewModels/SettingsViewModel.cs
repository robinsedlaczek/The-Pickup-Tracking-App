using System;
using System.Threading;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private DateTime _reportDate;
        private DateTime _pickupDate;

        public SettingsViewModel()
        {
            ReportDate = DateTime.Today;
            PickupDate = DateTime.Now.AddDays(-1);
        }

        public DateTime ReportDate
        {
            get { return _reportDate; }
            set
            {
                Set(ref _reportDate, value, nameof(ReportDate));
            }
        }

        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set
            {
                Set(ref _pickupDate, value, nameof(PickupDate));
            }
        }
    }
}
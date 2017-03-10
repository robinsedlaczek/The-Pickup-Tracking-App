using System;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class HotelViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public Guid Guid { get; internal set; }

        public string Name { get; set; }

        public string City { get; set; }

        public bool AllowNavigate { get; set; }

        public bool UpToDate { get; set; }

    }
}

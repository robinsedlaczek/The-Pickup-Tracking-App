using System;

namespace Fairmas.PickupTracking.Shared.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string City { get; set; }
    }
}

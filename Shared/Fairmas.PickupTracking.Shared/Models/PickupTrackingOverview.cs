using System.Collections.Generic;

namespace Fairmas.PickupTracking.Shared.Models
{
    public class PickupTrackingOverview
    {
        public List<PickupTrackingOverview> PickupTrackingOverviews { get; set; }

        public bool IsWeekendDay { get; set; }

        public string Id { get; set; }

        public double RoomNights { get; set; }

        public double OutOfOrderRooms { get; set; }

        public double OutOfServiceRooms { get; set; }

        public double OutOfOrderRoomsForPickupSince { get; set; }

        public double OutOfServiceRoomsForPickupSince { get; set; }

        public double RoomNightsForPickupSince { get; set; }

        public double AvailableRooms { get; set; }

        public double AvailableRoomsForPickupSince { get; set; }

        public double Revenue { get; set; }

        public double RevenueForPickupSince { get; set; }

        public double Overbooking { get; set; }

        public string Occupancy { get; set; }

        public double AverageDailyRate { get; set; }

        public double RevPar { get; set; }

        public double RoomNightsPickup { get; set; }

        public double AverageDailyRatePickup { get; set; }

        public double AverageDailyRate2 { get; set; }

        public double RevenuePickup { get; set; }
    }
}
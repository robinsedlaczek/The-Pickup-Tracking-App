using System;

namespace Fairmas.PickupTracking.Shared.Models
{
    /// <summary>
    /// This model represents the pickup figures that are delivered by the data backend service.
    /// All properties are optional and must not be included in responses.
    /// </summary>
    public class ResponseFiguresModel
    {
        public DateTime? BusinessDate { get; set; }

        public string Segment { get; set; }

        public PickupFigures RoomNights { get; set; }

        public PickupFigures OutOfOrderRooms { get; set; }

        public PickupFigures OutOfServiceRooms { get; set; }

        public PickupFigures AvailableRooms { get; set; }

        public PickupFigures Revenue { get; set; }

        public PickupFigures AverageDailyRate { get; set; }

        public PickupFigures AverageDailyRate2 { get; set; }

        public PickupFigures RevPar { get; set; }

        public PickupFigures Overbooking { get; set; }

        public PickupFigures Occupancy { get; set; }
    }
}
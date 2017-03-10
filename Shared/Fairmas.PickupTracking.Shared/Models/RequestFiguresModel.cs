namespace Fairmas.PickupTracking.Shared.Models
{
    /// <summary>
    /// This model represents options that can be used in service requests to define which figures are requested from 
    /// the data backend service. If the properties are not explicitly set in requests, the appropriate figures will 
    /// not be delivered by the service.
    /// </summary>
    public class RequestFiguresModel
    {
        public FigureOptions RoomNights { get; set; }

        public FigureOptions OutOfOrderRooms { get; set; }

        public FigureOptions OutOfServiceRooms { get; set; }

        public FigureOptions AvailableRooms { get; set; }

        public FigureOptions Revenue { get; set; }

        public FigureOptions AverageDailyRate { get; set; }

        public FigureOptions AverageDailyRate2 { get; set; }

        public FigureOptions RevPar { get; set; }

        public FigureOptions Overbooking { get; set; }

        public FigureOptions Occupancy { get; set; }
    }
}
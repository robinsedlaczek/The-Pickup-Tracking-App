namespace Fairmas.PickupTracking.Shared.Models
{
    /// <summary>
    /// Represents the model for the figures that are delivered by the PickupTracking backend services.
    /// </summary>
    public class PickupFigures
    {
        /// <summary>
        /// The appropriate figure from snapshot one. Figure will only be delivered if explicitely specified in the request.
        /// </summary>
        public decimal? SnapshotOneValue { get; set; }

        /// <summary>
        /// The appropriate figure from snapshot two. Figure will only be delivered if explicitely specified in the request.
        /// </summary>
        public decimal? SnapshotTwoValue { get; set; }

        /// <summary>
        /// The appropriate pickup figure (difference between figures from snapshot one and snapshot two). 
        /// Figure will only be delivered if explicitely specified in the request.
        /// </summary>
        public decimal? PickupValue { get; set; }
    }
}
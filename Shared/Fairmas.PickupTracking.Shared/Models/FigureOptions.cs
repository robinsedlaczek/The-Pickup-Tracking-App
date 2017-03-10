namespace Fairmas.PickupTracking.Shared.Models
{
    /// <summary>
    /// Represents options to specify which figures should be delivered from the PickupTracking backend services.
    /// </summary>
    public class FigureOptions
    {
        /// <summary>
        /// Indicates whether a value from snapshot one will be included in response messages or not.
        /// If this property is not set (null) or set to false, the value will not be delivered in response messages.
        /// </summary>
        public bool? IncludeSnapshotOneValue { get; set; }

        /// <summary>
        /// Indicates whether a value from snapshot two will be included in response messages or not.
        /// If this property is not set (null) or set to false, the value will not be delivered in response messages.
        /// </summary>
        public bool? IncludeSnapshotTwoValue { get; set; }

        /// <summary>
        /// Indicates whether the pickup value (difference between values from snapshot one and snapshot two) will be included in response messages or not.
        /// If this property is not set (null) or set to false, the value will not be delivered in response messages.
        /// </summary>
        public bool? IncludePickupValue { get; set; }
    }
}
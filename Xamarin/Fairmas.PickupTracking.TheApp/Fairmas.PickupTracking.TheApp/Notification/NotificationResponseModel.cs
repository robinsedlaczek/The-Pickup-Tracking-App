namespace Fairmas.PickupTracking.TheUniversalApp.Notification
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotificationResponseModel<T> where T : class
    {
        /// <summary>
        /// The model that was worked with.
        /// </summary>
        public T Model { get; set; }
        /// <summary>
        /// An additional nofication messsage
        /// </summary>
        public NotificationMessage Notification { get; set; }
    }

    public class NotificationResponseModel
    {
        /// <summary>
        /// An additional nofication messsage
        /// </summary>
        public NotificationMessage Notification { get; set; }
    }
}

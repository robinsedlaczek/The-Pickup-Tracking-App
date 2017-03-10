using System;
using System.Net;

namespace Fairmas.PickupTracking.TheUniversalApp.Notification
{
    public class NotificationMessage
    {
        /// <summary>
        /// Create a NotificationMessage Object
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cssClass"></param>
        /// <param name="displayTime">Display time in milliseconds.</param>
        public NotificationMessage(string message, MessageType cssClass = MessageType.Info, uint? displayTime = null)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                throw new Exception("Message text missing");
            }

            Message = WebUtility.HtmlEncode(message)
                .Replace("\r\n", "<br />") //Replace windows line endings since HtmlEncode doesn't do that for us.
                .Replace("\n", "<br />") //... unix line endings too.
                .Replace(@"\", @"\\"); //Since backslashes are escape characters in java script, we need to escape them.

            CssClass = cssClass.ToString().ToLower();
            DisplayTime = displayTime;
        }

        public string Message { get; private set; }
        public string CssClass { get; private set; }
        public uint? DisplayTime { get; private set; }
    }
}
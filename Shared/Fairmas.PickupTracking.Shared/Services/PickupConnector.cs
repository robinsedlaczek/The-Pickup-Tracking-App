using Fairmas.PickupTracking.Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace Fairmas.PickupTracking.Shared.Services
{
    internal class PickupConnector
    {
        #region Construction

        private PickupConnector()
        {
            IsConnected = false;
        }

        #endregion

        #region Internal static Members

        internal static PickupConnector Default { get; } = new PickupConnector();

        #endregion

        #region Internal Members

        internal async Task<bool> ConnectAsync(string username, string password)
        {
            var client = ServiceLocator.FindService<IWebApiClientService>();

            IsConnected = await client.AuthenticateAsync(Constants.PickupTrackingBaseAddress, username, password);

            return IsConnected;
        }

        internal bool IsConnected
        {
            get;
            private set;
        }

        internal IPickupDataService DataService
        {
            get
            {
                if (!IsConnected)
                    throw new InvalidOperationException("You are not connected to your PickupTracking server. Please authenticate before querying data.");

                return ServiceLocator.FindService<IPickupDataService>();
            }
        }

        #endregion
    }
}

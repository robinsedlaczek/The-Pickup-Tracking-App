using System.Collections.Generic;
using System.Linq;

namespace Fairmas.PickupTracking.Shared.Services
{
    internal class ServiceLocator
    {
        private static List<object> Services { get; } = new List<object>();

        static ServiceLocator()
        {
            //Services.Add(new PickupTestDataService());
            Services.Add(new PickupDataService());

            Services.Add(new WebApiClientService());
        }

        internal static T FindService<T>() where T : class
        {
            var foundService = Services.Where(service => service is T).FirstOrDefault() as T;

            return foundService;
        }
    }
}

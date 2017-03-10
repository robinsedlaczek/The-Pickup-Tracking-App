using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fairmas.PickupTracking.Shared.Interfaces
{
    public interface IWebApiClientService
    {
        Task<bool> AuthenticateAsync(string baseAddress, string userName, string password);

        Task<bool> ChangePropertyAsync(Guid propertyId);

        Task<T> GetAsync<T>(string relativeRoute, string methodName, KeyValuePair<string, string>[] parameter = null);

        Task<HttpResponseMessage> GetAsync(string relativeRoute, string methodName, KeyValuePair<string, string>[] Parameter = null);

        Task<TResult> PostAsync<TResult, TContent>(string relativeRoute, string methodName, TContent content, KeyValuePair<string, string>[] parameter = null);

        Task<HttpResponseMessage> PostAsync<TContent>(string relativeRoute, string methodName, TContent content, KeyValuePair<string, string>[] parameter = null);

        Task<HttpResponseMessage> PostAsync(string relativeRoute, string methodName, KeyValuePair<string, string>[] parameter = null);
    }
}

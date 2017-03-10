using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Fairmas.PickupTracking.Shared.Models;
using System.Threading.Tasks;
using System.Text;
using Fairmas.PickupTracking.Shared.Interfaces;

namespace Fairmas.PickupTracking.Shared.Services
{
    public class WebApiClientService : IWebApiClientService, IDisposable
    {
        #region Private Fields

        private HttpClient _httpClient;

        #endregion

        #region Construction

        internal WebApiClientService()
        {
            try
            {
                _httpClient = new HttpClient(new HttpClientHandler { UseCookies = false });
                _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            }
            catch (Exception)
            {
                // TODO: [RS] Handle error..!?
            }           
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
        }

        #endregion

        #region IWebApiClientService

        public async Task<bool> AuthenticateAsync(string baseAddress, string userName, string password)
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri($"{baseAddress}/api/");

                var content = ToJsonContent(new { LoginName = userName, HashedPassword = password });
                var response = await _httpClient.PostAsync("Core/Authentication", content);

                return SetAuthenticateCookie(response);
            }
            catch (Exception exception)
            {
                var error = "An error occurred while authenticating at Pickup Tracking server:" + Environment.NewLine + exception.Message;
                throw new Exception(error);
            }
        }

        public async Task<bool> ChangePropertyAsync(Guid propertyId)
        {
            try
            {
                //// API
                //// https://put-development.fairplanner.net/api/Administration/Property/ChangeProperty

                var parameter = new[]
                {
                    new KeyValuePair<string, string>("newPropertyGuid", propertyId.ToString()),
                };

                var response = await PostAsync("Administration/Property/", "ChangeProperty", parameter);

                return SetAuthenticateCookie(response);
            }
            catch (Exception exception)
            {
                var error = "An error occurred while changing property:" + Environment.NewLine + exception.Message;
                throw new Exception(error);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string relativeRoute, string methodName, KeyValuePair<string, string>[] parameter = null)
        {
            // TODO: [RS] Renew authentication cookie if response contains one.

            try
            {
                var url = relativeRoute.EndsWith("/") || methodName.StartsWith("/") ?
                    relativeRoute + methodName :
                    relativeRoute + "/" + methodName;

                url = url + "?" + parameter.Select(kv => kv.Key + "=" + kv.Value).Aggregate((first, secound) => first + "&" + secound);

                HttpResponseMessage response;

                try
                {
                    response = await _httpClient.GetAsync(url);

                    if ((int)response.StatusCode == 419)
                    {
                        response = await _httpClient.GetAsync(url);
                    }
                }
                catch (Exception e)
                {
                    response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                        throw new Exception("Internal Server error");
                }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> GetAsync<T>(string relativeRoute, string methodName, KeyValuePair<string,string>[] parameter = null )
        {
            // TODO: [RS] Renew authentication cookie if response contains one.

            try
            {
                var response = await GetAsync(relativeRoute, methodName, parameter);

                var json = response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsStringAsync() 
                    : string.Empty;

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<TContent>(string relativeRoute, string methodName, TContent content, KeyValuePair<string, string>[] parameter = null)
        {
            // TODO: [RS] Renew authentication cookie if response contains one.

            try
            {
                var url = relativeRoute.EndsWith("/") || methodName.StartsWith("/") ?
                    relativeRoute + methodName :
                    relativeRoute + "/" + methodName;

                url = url + "?" + parameter.Select(kv => kv.Key + "=" + kv.Value).Aggregate((first, secound) => first + "&" + secound);

                HttpResponseMessage response;

                try
                {
                    var settings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
                    var httpContent = await Task.Factory.StartNew(() => new StringContent(JsonConvert.SerializeObject(content, Formatting.None, settings), Encoding.UTF8));

                    httpContent.Headers.Remove("content-type");
                    httpContent.Headers.Add("content-type", "application/json");

                    response = await _httpClient.PostAsync(url, httpContent);

                    if ((int)response.StatusCode == 419)
                    {
                        response = await _httpClient.PostAsync(url, httpContent);
                    }
                }
                catch (Exception e)
                {
                    response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                        throw new Exception("Internal Server error");
                }

                return response;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string relativeRoute, string methodName, KeyValuePair<string, string>[] parameter = null)
        {
            // TODO: [RS] Renew authentication cookie if response contains one.

            try
            {
                var url = relativeRoute.EndsWith("/") || methodName.StartsWith("/") ?
                    relativeRoute + methodName :
                    relativeRoute + "/" + methodName;

                url = url + "?" + parameter.Select(kv => kv.Key + "=" + kv.Value).Aggregate((first, secound) => first + "&" + secound);

                HttpResponseMessage response;

                try
                {
                    var settings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
                    var httpContent = null as HttpContent;

                    response = await _httpClient.PostAsync(url, httpContent);

                    if ((int)response.StatusCode == 419)
                    {
                        response = await _httpClient.PostAsync(url, httpContent);
                    }
                }
                catch (Exception exception)
                {
                    response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                        throw new Exception("Internal Server error");
                }

                return response;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<TResult> PostAsync<TResult, TContent>(string relativeRoute, string methodName, TContent content, KeyValuePair<string, string>[] parameter = null)
        {
            // TODO: [RS] Renew authentication cookie if response contains one.

            try
            {
                var response = await PostAsync<TContent>(relativeRoute, methodName, content, parameter);

                var json = response.IsSuccessStatusCode 
                    ? await response.Content.ReadAsStringAsync() 
                    : String.Empty;

                return JsonConvert.DeserializeObject<TResult>(json);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// This method validates the HTTP status code of a response message. In case of problems, it throws an <see cref="ApplicationException"/>
        /// with detailed information about the problem, if available. If the HTTP status code is 200 OK, it returns the string content from the response.
        /// </summary>
        /// <param name="response">The response from which content must be returned and that must be validated.</param>
        /// <returns>Returns the response content in case of 200 OK, otherwise an exception is thrown.</returns>
        private string GetResponseContentAndValidateStatusCode(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var message = string.Empty;

            try
            {
                SetAuthenticateCookie(response);
            }
            catch
            {
                //this is to ensure this quick fix wont influence the tested behaviors
            }

            if (response.IsSuccessStatusCode)
            {
                return content;
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                message = "Request forbidden. Reason phrase: " + response.ReasonPhrase;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                message = "An error occurred at the Omega Centauri server. Reason phrase: " + response.ReasonPhrase;
            }
            else if ((int)response.StatusCode == 419)
            {
                throw new Exception("AuthorizationTimeoutException");
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception("An error occurred at the Omega Centauri server, but no further information was delivered. Reason phrase: " + response.ReasonPhrase);
            }
            else
            {
                throw new Exception("An unexpected result was delivered while requesting the Omega Centauri server. Reason phrase: " + response.ReasonPhrase);
            }

            try
            {
                // [RS] Try to get a notification from the response to add further information to the error message. 
                //      A notification is not always present in the response, so we have to do it in this exception handler
                //      and do nothing in case of an exception.
                var notificationResponse = JsonConvert.DeserializeObject<OmegaCentauriErrorResponse>(content);
 
                if (notificationResponse != null && notificationResponse.Message != null)
                    message = notificationResponse.Message;
            }
            catch 
            {
                // [RS] Do nothing if parsing of JSON message fails. This can only occur for HTTP status codes Forbidden and BadRequest.
                //      So we simply throw the exception with the message at the end of this method.
            }

            throw new Exception(message);
        }

        /// <summary>
        /// (Re)Set the authentication cookie for the web client request.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>Returns if successfull (true) or not (false).</returns>
        private bool SetAuthenticateCookie(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return false;

            var cookies = response.Headers.GetValues("Set-Cookie").ToList();
            var authCookie = cookies.Find(d => d.Contains("ASPXAUTH"));

            if (authCookie == null)
                return false;

            if (_httpClient.DefaultRequestHeaders.Contains("Cookie"))
                _httpClient.DefaultRequestHeaders.Remove("Cookie");

            _httpClient.DefaultRequestHeaders.Add("Cookie", authCookie);

            return true;
        }

        private HttpContent ToJsonContent<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        #endregion

    }
}

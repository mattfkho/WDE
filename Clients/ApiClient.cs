using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WDE.Models;

namespace WDE.Clients
{
    /// <summary>
    /// Client Interface
    /// </summary>
    public partial interface IClient
    {
        //Task<ICollection<ExerciseResult>> ApiExerciseExercise1PostAsync(TrolleyExerciseRequest request);
        //Task<ICollection<ExerciseResult>> ApiExerciseExercise1PostAsync(TrolleyExerciseRequest request, CancellationToken cancellationToken);
        //Task<ICollection<ExerciseResult>> ApiExerciseExercise2PostAsync(TrolleyExerciseRequest request);
        //Task<ICollection<ExerciseResult>> ApiExerciseExercise2PostAsync(TrolleyExerciseRequest request, CancellationToken cancellationToken);
        //Task<ICollection<ExerciseResult>> ApiExerciseExercise3PostAsync(TrolleyExerciseRequest request);
        //Task<ICollection<ExerciseResult>> ApiExerciseExercise3PostAsync(TrolleyExerciseRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<ICollection<ShopperOrders>> ApiResourceShopperHistoryGetAsync(Guid token);

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<ICollection<ShopperOrders>> ApiResourceShopperHistoryGetAsync(Guid token, CancellationToken cancellationToken);

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<ICollection<Product>> ApiResourceProductsGetAsync(Guid token);

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<ICollection<Product>> ApiResourceProductsGetAsync(Guid token, CancellationToken cancellationToken);

        /// <summary>
        /// This will return the lowest total given products, specials and quantities.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Value of the calculated trolley.
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<double> ApiResourceTrolleyCalculatorPostAsync(Guid token, Trolley request);

        /// <summary>
        /// This will return the lowest total given products, specials and quantities.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Value of the calculated trolley.
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<double> ApiResourceTrolleyCalculatorPostAsync(Guid token, Trolley request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// The API client
    /// </summary>
    /// <seealso cref="WDE.Clients.IClient" />
    public partial class ApiClient : IClient
    {
        /// <summary>
        /// The base URL
        /// </summary>
        private string _baseUrl = "/";

        /// <summary>
        /// The HTTP client
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// The settings
        /// </summary>
        private Lazy<JsonSerializerSettings> _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        /// <summary>
        /// Gets the json serializer settings.
        /// </summary>
        /// <value>
        /// The json serializer settings.
        /// </value>
        protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        /// <summary>
        /// Updates the json serializer settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);

        /// <summary>
        /// Prepares the request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <param name="url">The URL.</param>
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url);

        /// <summary>
        /// Prepares the request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <param name="urlBuilder">The URL builder.</param>
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder);

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="response">The response.</param>
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response);

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<ShopperOrders>> ApiResourceShopperHistoryGetAsync(Guid token)
        {
            return ApiResourceShopperHistoryGetAsync(token, CancellationToken.None);
        }

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ArgumentNullException">token</exception>
        /// <exception cref="WDE.Clients.ApiException">The HTTP status code of the response was not expected (" + (int)response.StatusCode + "). - null</exception>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<ShopperOrders>> ApiResourceShopperHistoryGetAsync(Guid token, CancellationToken cancellationToken)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            var urlBuilder = new StringBuilder();
            urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/resource/shopperHistory?");
            urlBuilder.Append(Uri.EscapeDataString("token") + "=").Append(Uri.EscapeDataString(ConvertToString(token, CultureInfo.InvariantCulture))).Append("&");
            urlBuilder.Length--;

            var client = _httpClient;
            try
            {
                using (var requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = new HttpMethod("GET");
                    requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client, requestMessage, urlBuilder);
                    var url = urlBuilder.ToString();
                    requestMessage.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client, requestMessage, url);

                    var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        if (response.Content != null && response.Content.Headers != null)
                        {
                            foreach (var item in response.Content.Headers)
                                headers[item.Key] = item.Value;
                        }

                        ProcessResponse(client, response);

                        var status = ((int)response.StatusCode).ToString();
                        if (status == "200")
                        {
                            var objectResponse = await ReadObjectResponseAsync<ICollection<ShopperOrders>>(response, headers).ConfigureAwait(false);
                            return objectResponse.Object;
                        }
                        else
                        if (status != "200" && status != "204")
                        {
                            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response.StatusCode + ").", (int)response.StatusCode, responseData, headers, null);
                        }

                        return default(ICollection<ShopperOrders>);
                    }
                    finally
                    {
                        if (response != null)
                            response.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<Product>> ApiResourceProductsGetAsync(Guid token)
        {
            return ApiResourceProductsGetAsync(token, CancellationToken.None);
        }

        /// <summary>
        /// This will return all customers buying habits for a range of products
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Success
        /// </returns>
        /// <exception cref="ArgumentNullException">token</exception>
        /// <exception cref="WDE.Clients.ApiException">The HTTP status code of the response was not expected (" + (int)response.StatusCode + "). - null</exception>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<Product>> ApiResourceProductsGetAsync(System.Guid token, CancellationToken cancellationToken)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            var urlBuilder = new StringBuilder();
            urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/resource/products?");
            urlBuilder.Append(Uri.EscapeDataString("token") + "=").Append(Uri.EscapeDataString(ConvertToString(token, CultureInfo.InvariantCulture))).Append("&");
            urlBuilder.Length--;

            var client = _httpClient;
            try
            {
                using (var requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = new HttpMethod("GET");
                    requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client, requestMessage, urlBuilder);
                    var url = urlBuilder.ToString();
                    requestMessage.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client, requestMessage, url);

                    var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        if (response.Content != null && response.Content.Headers != null)
                        {
                            foreach (var item in response.Content.Headers)
                                headers[item.Key] = item.Value;
                        }

                        ProcessResponse(client, response);

                        var status = ((int)response.StatusCode).ToString();
                        if (status == "200")
                        {
                            var objectResponse = await ReadObjectResponseAsync<ICollection<Product>>(response, headers).ConfigureAwait(false);
                            return objectResponse.Object;
                        }
                        else
                        if (status != "200" && status != "204")
                        {
                            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response.StatusCode + ").", (int)response.StatusCode, responseData, headers, null);
                        }

                        return default(ICollection<Product>);
                    }
                    finally
                    {
                        if (response != null)
                            response.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// This will return the lowest total given products, specials and quantities.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <returns>
        /// Value of the calculated trolley.
        /// </returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<double> ApiResourceTrolleyCalculatorPostAsync(Guid token, Trolley request)
        {
            return ApiResourceTrolleyCalculatorPostAsync(token, request, CancellationToken.None);
        }

        /// <summary>
        /// This will return the lowest total given products, specials and quantities.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Value of the calculated trolley.
        /// </returns>
        /// <exception cref="ArgumentNullException">token</exception>
        /// <exception cref="WDE.Clients.ApiException">The HTTP status code of the response was not expected (" + (int)response.StatusCode + "). - null</exception>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<double> ApiResourceTrolleyCalculatorPostAsync(Guid token, Trolley request, CancellationToken cancellationToken)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            var urlBuilder = new StringBuilder();
            urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/resource/trolleyCalculator?");
            urlBuilder.Append(Uri.EscapeDataString("token") + "=").Append(Uri.EscapeDataString(ConvertToString(token, CultureInfo.InvariantCulture))).Append("&");
            urlBuilder.Length--;

            var client = _httpClient;
            try
            {
                using (var requestMessage = new HttpRequestMessage())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(request, _settings.Value));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    requestMessage.Content = content;
                    requestMessage.Method = new HttpMethod("POST");
                    requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client, requestMessage, urlBuilder);
                    var url = urlBuilder.ToString();
                    requestMessage.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client, requestMessage, url);

                    var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        if (response.Content != null && response.Content.Headers != null)
                        {
                            foreach (var item in response.Content.Headers)
                                headers[item.Key] = item.Value;
                        }

                        ProcessResponse(client, response);

                        var status = ((int)response.StatusCode).ToString();
                        if (status == "200")
                        {
                            var objectResponse = await ReadObjectResponseAsync<double>(response, headers).ConfigureAwait(false);
                            return objectResponse.Object;
                        }
                        else
                        if (status != "200" && status != "204")
                        {
                            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response.StatusCode + ").", (int)response.StatusCode, responseData, headers, null);
                        }

                        return default(double);
                    }
                    finally
                    {
                        if (response != null)
                            response.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected struct ObjectResponseResult<T>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ObjectResponseResult{T}"/> struct.
            /// </summary>
            /// <param name="responseObject">The response object.</param>
            /// <param name="responseText">The response text.</param>
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            /// <summary>
            /// Gets the object.
            /// </summary>
            /// <value>
            /// The object.
            /// </value>
            public T Object { get; }

            /// <summary>
            /// Gets the text.
            /// </summary>
            /// <value>
            /// The text.
            /// </value>
            public string Text { get; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [read response as string].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read response as string]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadResponseAsString { get; set; }

        /// <summary>
        /// Reads the object response asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        /// <exception cref="WDE.Clients.ApiException">
        /// </exception>
        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        var serializer = JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        private string ConvertToString(object value, CultureInfo cultureInfo)
        {
            if (value is Enum)
            {
                string name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        if (System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                        {
                            return attribute.Value ?? name;
                        }
                    }
                }
            }
            else if (value is bool)
            {
                return Convert.ToString(value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = Enumerable.OfType<object>((Array)value);
                return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return Convert.ToString(value, cultureInfo);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Exception" />
    public partial class ApiException : System.Exception
    {
        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response { get; private set; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="response">The response.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="innerException">The inner exception.</param>
        public ApiException(string message, int statusCode, string response, IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }
}
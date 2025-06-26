using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;


namespace SMART_APP.Services
{
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using Microsoft.Maui.Devices;
    using SMART_APP.Models;
    using SMART_APP.Models.Login;
    using static System.Net.WebRequestMethods;

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private static readonly ApiService instance = new();
        public static ApiService Instance => instance;

        private string BaseUrl =>
        DeviceInfo.Platform == DevicePlatform.Android
              ? "http://10.0.2.2:5300/"
            : "http://localhost:5300/";

  

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = TimeSpan.FromMinutes(5)
            };

        }


    



        public async Task<ApiResponse<T>> RequestSmartAPIs<T>(string operation, object requestModel = null, bool retryUnauthorized = false)
        {
            try
            {
                var requestContent = requestModel != null
                    ? new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json")
                    : null;
                if (_httpClient.BaseAddress == null)
                {
                    throw new InvalidOperationException("BaseAddress is null.");
                }

                var response = await _httpClient.PostAsync($"api/{operation}", requestContent);

                var content = await response.Content.ReadAsStringAsync();
                //await SecureStorage.Default.SetAsync("Token", loginResponse.Token);
                
                return await HandleResponse<T>(response, content, operation, requestModel, retryUnauthorized);
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }
        public async Task AddTokenAsync()
        {
            var token = await SecureStorage.Default.GetAsync("Token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public class ApiResponse<T>
        {
            public bool IsSuccess { get; set; }
            public T Data { get; set; }
            public string ErrorMessage { get; set; }
        }
  

        public class FailedResponseModel
        {
            public string ErrorCode { get; set; }
            public string ErrorDetail { get; set; }
        }
        public interface IResponseModel { }

        private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response, string content, string operation, object requestModel, bool retryUnauthorized)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var data = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return new ApiResponse<T> { IsSuccess = true, Data = data };

                case HttpStatusCode.BadRequest:
                    return new ApiResponse<T> { IsSuccess = false, ErrorMessage = content };

                case HttpStatusCode.Unauthorized:
                    if (retryUnauthorized)
                        return new ApiResponse<T> { IsSuccess = false, ErrorMessage = "Unauthorized" };
                    return await RequestSmartAPIs<T>(operation, requestModel, true);

                case HttpStatusCode.InternalServerError:
                    return new ApiResponse<T> { IsSuccess = false, ErrorMessage = "ServerError: " + content };

                default:
                    return new ApiResponse<T> { IsSuccess = false, ErrorMessage = "UnknownError: " + content };
            }
        }

        private async Task<T> HandleOkResponse<T>(HttpResponseMessage response)
        {
            var responseText = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }
}


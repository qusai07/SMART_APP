using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SMART_APP.Models.Common;

namespace SMART_APP.Services.Base
{
    public class ApiServiceBase
    {
        protected readonly HttpClient _httpClient;

        protected string BaseUrl =>
            DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5300/"
                : "http://localhost:5300/";

        public ApiServiceBase()
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

        protected async Task AddTokenAsync()
        {
            var token = await SecureStorage.Default.GetAsync("Token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        protected async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
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
                    return new ApiResponse<T> { IsSuccess = false, ErrorMessage = "Unauthorized" };

                case HttpStatusCode.InternalServerError:
                    return new ApiResponse<T> { IsSuccess = false, ErrorMessage = "ServerError: " + content };

                default:
                    return new ApiResponse<T> { IsSuccess = false, ErrorMessage = "UnknownError: " + content };
            }
        }
    }
}


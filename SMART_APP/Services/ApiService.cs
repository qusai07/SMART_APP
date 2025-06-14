using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using SMART_APP.Models;
using System.Net;


namespace SMART_APP.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private static readonly ApiService instance = new();
        public static ApiService Instance => instance;

        //public string MacBaseUrl => macBaseUrl;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://192.168.1.115:44377/api/"),
                Timeout = TimeSpan.FromMinutes(5)
            };

        }
        // public async Task<IResponseModel> RequestMac<T>(string operation, object requestModel = null, bool retryUnauthorized = false)
        //where T : IResponseModel
        // {

        //     using var httpClient = new HttpClient();
        //   //  httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //     var requestContent = requestModel != null
        //         ? new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json")
        //         : null;

        //     var response = await httpClient.PostAsync($"{WebBaseUrl}api/{operation}", requestContent);

        //     var content = await response.Content.ReadAsStringAsync();
        //     Console.WriteLine($"Status: {response.StatusCode}, Content: {content}");

        //     return await HandleResponse<T>(response, operation, requestModel, retryUnauthorized);
        // }
        // private async Task<IResponseModel> HandleResponse<T>(HttpResponseMessage response, string operation, object requestModel, bool retryUnauthorized)
        // where T : IResponseModel
        // {
        //     switch (response.StatusCode)
        //     {
        //         case HttpStatusCode.OK:
        //             return await HandleOkResponse<T>(response);

        //         case HttpStatusCode.BadRequest:
        //             return new FailedResponseModel { ErrorCode = await response.Content.ReadAsStringAsync() };

        //         case HttpStatusCode.Unauthorized:
        //             if (retryUnauthorized) return new FailedResponseModel { ErrorCode = "Unauthorized" };
        //            // macToken = null; // clear old token
        //             return await RequestMac<T>(operation, requestModel, true);

        //         case HttpStatusCode.InternalServerError:
        //             return new FailedResponseModel
        //             {
        //                 ErrorCode = "ServerError",
        //                 ErrorDetail = await response.Content.ReadAsStringAsync()
        //             };

        //         default:
        //             return new FailedResponseModel
        //             {
        //                 ErrorCode = "UnknownError",
        //                 ErrorDetail = await response.Content.ReadAsStringAsync()
        //             };
        //     }
        // }

        // private async Task<IResponseModel> HandleOkResponse<T>(HttpResponseMessage response)
        //     where T : IResponseModel
        // {
        //     var responseText = await response.Content.ReadAsStringAsync();
        //     return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions
        //     {
        //         PropertyNameCaseInsensitive = true
        //     });
        // }

        public async Task<LoginResponse> LoginAsync(LoginModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Auth/Login", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }


    }
}

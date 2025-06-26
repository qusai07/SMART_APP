using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_APP.Services.Auth
{
    using SMART_APP.Models.Common;
    using SMART_APP.Models.Login;
    using SMART_APP.Services.Base;
    using System.Text.Json;

    public class AuthService : ApiServiceBase
    {
        public async Task<ApiResponse<string>> LoginAsync(LoginRequest loginRequest)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(loginRequest),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Auth/Login", content);

            var tokenString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                await SecureStorage.Default.SetAsync("Token", tokenString);

                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    Data = tokenString
                };
            }
            else
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    ErrorMessage = tokenString
                };
            }
        }
    }
}


using SMART_APP.Models.Common;
using SMART_APP.Models.Login;
using SMART_APP.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SMART_APP.Services.Profile
{
    public class ProfileService : ApiServiceBase
    {
        public ProfileService() : base() { }

        public async Task<ApiResponse<UserProfileModelcs>> GetUserProfileAsync()
        {
            await AddTokenAsync();

            var response = await _httpClient.GetAsync("api/Auth/GetProfile");
            return await HandleResponse<UserProfileModelcs>(response);
        }
        public async Task<ApiResponse<string>> LogoutAsync()
        {
            await AddTokenAsync();
            var response = await _httpClient.PostAsync("api/Auth/Logout", null);
            return await HandleResponse<string>(response);
        }
    }
}

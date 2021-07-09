using Newtonsoft.Json;
using OnlineShopSolution.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnineShopSolution.AdminApp.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authenticate(PostLoginDto req)
        {
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client =  _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var response = await client.PostAsync("/api/users/authenticate", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }

        
    }
}

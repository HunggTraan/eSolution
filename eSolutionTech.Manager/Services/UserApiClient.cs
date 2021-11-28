﻿using eSolutionTech.ViewModels.System.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, Constants.Constants.ApplicationJson);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(Constants.Constants.BaseAddress);
            var response = await client.PostAsync(Constants.Constants.ManagerUserLink, httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}

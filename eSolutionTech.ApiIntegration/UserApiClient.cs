﻿using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public class UserApiClient : IUserApiClient
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserApiClient(IHttpClientFactory httpClientFactory,
               IHttpContextAccessor httpContextAccessor,
                IConfiguration configuration)
    {
      _configuration = configuration;
      _httpContextAccessor = httpContextAccessor;
      _httpClientFactory = httpClientFactory;
    }
    public async Task<ApiResult<string>> Authenticate(LoginRequest request)
    {
      var json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, Constants.Constants.ApplicationJson);

      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      var response = await client.PostAsync(Constants.Constants.ManagerUserLink, httpContent);
      if (response.IsSuccessStatusCode)
      {
        return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
      }

      return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
    }

    public async Task<ApiResult<bool>> Delete(Guid id)
    {
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      var response = await client.DeleteAsync($"/api/users/{id}");
      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
    }

    public async Task<ApiResult<UserViewModel>> GetById(Guid id)
    {
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      var response = await client.GetAsync($"/api/users/{id}");
      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body);

      return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body);
    }

    public async Task<ApiResult<List<UserViewModel>>> GetAll()
    {
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      var response = await client.GetAsync($"/api/users");
      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<List<UserViewModel>>>(body);

      return JsonConvert.DeserializeObject<ApiErrorResult<List<UserViewModel>>>(body);
    }

    public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPagings(GetUserPagingRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);

      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
          $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

      var body = await response.Content.ReadAsStringAsync();
      var users = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserViewModel>>>(body);
      return users;
    }

    public async Task<ApiResult<bool>> CreateUser(CreateUserRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);

      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, Constants.Constants.ApplicationJson);

      var response = await client.PostAsync($"/api/users/adduser", httpContent);
      var result = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
    }

    public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);

      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, Constants.Constants.ApplicationJson);

      var response = await client.PutAsync($"/api/users/{id}", httpContent);
      var result = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
    }

    public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration["BaseAddress"]);
      var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

      var json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

      var response = await client.PutAsync($"/api/users/{id}/roles", httpContent);
      var result = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
    }
  }
}

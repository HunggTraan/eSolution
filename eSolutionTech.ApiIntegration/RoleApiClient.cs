using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Roles;
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
  public class RoleApiClient : IRoleApiClient
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoleApiClient(IHttpClientFactory httpClientFactory,
               IHttpContextAccessor httpContextAccessor,
                IConfiguration configuration)
    {
      _configuration = configuration;
      _httpContextAccessor = httpContextAccessor;
      _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResult<bool>> Create(CreateRoleRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);

      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, Constants.Constants.ApplicationJson);

      var response = await client.PostAsync($"/api/roles/create", httpContent);
      var result = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
    }

    public async Task<ApiResult<bool>> Delete(string id)
    {
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      var response = await client.DeleteAsync($"/api/roles/{id}");
      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
    }

    public async Task<ApiResult<List<RoleVm>>> GetAll()
    {
      var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration["BaseAddress"]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
      var response = await client.GetAsync($"/api/roles");
      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
      {
        List<RoleVm> myDeserializedObjList = (List<RoleVm>)JsonConvert.DeserializeObject(body, typeof(List<RoleVm>));
        return new ApiSuccessResult<List<RoleVm>>(myDeserializedObjList);
      }
      return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleVm>>>(body);
    }

    public async Task<ApiResult<PagedResult<RoleVm>>> GetRolesPagings(RolePagingRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);

      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      string url = $"/api/roles/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}";
      if (!string.IsNullOrEmpty(request.Keyword))
      {
        url += $"&keyWord={request.Keyword}";
      }
      var response = await client.GetAsync(url);

      var body = await response.Content.ReadAsStringAsync();
      var roles = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<RoleVm>>>(body);
      return roles;
    }

    public async Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request)
    {
      var client = _httpClientFactory.CreateClient();
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);

      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, Constants.Constants.ApplicationJson);

      var response = await client.PutAsync($"/api/roles/{id}", httpContent);
      var result = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

      return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
    }

    public async Task<ApiResult<RoleVm>> GetById(Guid id)
    {
      var sessions = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constants.Token);
      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);
      var response = await client.GetAsync($"/api/roles/{id}");
      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
        return JsonConvert.DeserializeObject<ApiSuccessResult<RoleVm>>(body);

      return JsonConvert.DeserializeObject<ApiErrorResult<RoleVm>>(body);
    }
  }
}

using eSolutionTech.ViewModels.Catalog.ShiftSettings;
using eSolutionTech.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public class ShiftSettingApiClient : BaseApiClient, IShiftSettingApiClient
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ShiftSettingApiClient(IHttpClientFactory httpClientFactory,
       IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) : base(httpClientFactory,
           httpContextAccessor,
           configuration)
    {
      _httpContextAccessor = httpContextAccessor;
      _configuration = configuration;
      _httpClientFactory = httpClientFactory;
    }
    public async Task<bool> CreateShiftSetting(ShiftSettingCreateRequest request)
    {
      var sessions = _httpContextAccessor
                      .HttpContext
                      .Session
                      .GetString(Constants.Constants.Token);

      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var requestContent = new MultipartFormDataContent();


      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Code) ? "" : request.Code.ToString()), "code");
      requestContent.Add(new StringContent(request.TimeIn.ToString()), "timeIn");
      requestContent.Add(new StringContent(request.TimeOut.ToString()), "timeOut");
      requestContent.Add(new StringContent(request.ExceedTimeIn.ToString()), "exceedTimeIn");
      requestContent.Add(new StringContent(request.ExceedTimeOut.ToString()), "exceedTimeOut");

      var response = await client.PostAsync($"/api/shiftSettings/", requestContent);
      return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteShiftSetting(int id)
    {
      return await Delete($"/api/shiftSettings/" + id);
    }

    public async Task<List<ShiftSettingViewModel>> GetAll()
    {
      var data = await GetAsync<List<ShiftSettingViewModel>>(
              $"/api/shiftSettings");

      return data;
    }

    public async Task<ShiftSettingViewModel> GetById(int id)
    {
      var data = await GetAsync<ShiftSettingViewModel>($"/api/shiftSettings/{id}");

      return data;
    }

    public async Task<PagedResult<ShiftSettingViewModel>> GetPagings(GetShiftSettingPagingRequest request)
    {
      var data = await GetAsync<PagedResult<ShiftSettingViewModel>>(
                  $"/api/shiftSettings/paging?pageIndex={request.PageIndex}" +
                  $"&pageSize={request.PageSize}" +
                  $"&keyword={request.KeyWord}");

      return data;
    }

    public async Task<bool> UpdateShiftSetting(ShiftSettingUpdateRequest request)
    {
      var sessions = _httpContextAccessor
          .HttpContext
          .Session
          .GetString(Constants.Constants.Token);

      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var requestContent = new MultipartFormDataContent();

      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Code) ? "" : request.Code.ToString()), "code");
      requestContent.Add(new StringContent(request.TimeIn.ToString()), "timeIn");
      requestContent.Add(new StringContent(request.TimeOut.ToString()), "timeOut");
      requestContent.Add(new StringContent(request.ExceedTimeIn.ToString()), "exceedTimeIn");
      requestContent.Add(new StringContent(request.ExceedTimeOut.ToString()), "exceedTimeOut");

      var response = await client.PutAsync($"/api/shiftSettings/" + request.Id, requestContent);
      return response.IsSuccessStatusCode;
    }
  }
}

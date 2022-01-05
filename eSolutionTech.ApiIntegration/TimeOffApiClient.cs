using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public class TimeOffApiClient : BaseApiClient, ITimeOffApiClient
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public TimeOffApiClient(IHttpClientFactory httpClientFactory,
       IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) : base(httpClientFactory,
           httpContextAccessor,
           configuration)
    {
      _httpContextAccessor = httpContextAccessor;
      _configuration = configuration;
      _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> ApplyRequest(int id, int status)
    {
      var sessions = _httpContextAccessor
        .HttpContext
        .Session
        .GetString(Constants.Constants.Token);

      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var requestContent = new MultipartFormDataContent();

      requestContent.Add(new StringContent(id.ToString()), "id");
      requestContent.Add(new StringContent(status.ToString()), "status");

      var response = await client.PostAsync($"/api/timeOffRequests/apply", requestContent);
      return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateTimeOff(TimeOffCreateRequest request)
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
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TimeOffTypeId) ? "" : request.TimeOffTypeId.ToString()), "timeOffTypeId");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.RequestUnit) ? "" : request.RequestUnit.ToString()), "requestUnit");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.HalfDay) ? "" : request.HalfDay.ToString()), "halfDay");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserId) ? "" : request.UserId.ToString()), "userId");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Duration) ? "" : request.Duration.ToString()), "duration");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Status) ? "" : request.Status.ToString()), "status");
      requestContent.Add(new StringContent(request.FromDate.ToString()), "fromDate");
      requestContent.Add(new StringContent(request.ToDate.ToString()), "toDate");

      var response = await client.PostAsync($"/api/timeOffRequests/", requestContent);
      return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTimeOff(int id)
    {
      return await Delete($"/api/timeOffRequests/" + id);
    }

    public async Task<List<TimeOffViewModel>> GetAll(string userId)
    {
      var url = $"/api/timeOffRequests/getall";

      if (!string.IsNullOrEmpty(userId))
      {
        url += $"?userId={userId}";
      }

      var data = await GetAsync<List<TimeOffViewModel>>(url);

      return data;
    }

    public async Task<TimeOffViewModel> GetById(int id)
    {
      var data = await GetAsync<TimeOffViewModel>($"/api/timeOffRequests/{id}");

      return data;
    }

    public async Task<PagedResult<TimeOffViewModel>> GetPagings(TimeOffPagingRequest request)
    {
      var url = $"/api/timeOffRequests/paging?pageIndex={request.PageIndex}" + $"&pageSize={request.PageSize}";

      if (!string.IsNullOrEmpty(request.UserId))
      {
        url += $"&userId={request.UserId}";
      }

      if (!string.IsNullOrEmpty(request.FromDate))
      {
        url += $"&fromDate={request.FromDate}";
      }

      if (!string.IsNullOrEmpty(request.ToDate))
      {
        url += $"&toDate={request.ToDate}";
      }

      if (!string.IsNullOrEmpty(request.Status))
      {
        url += $"&status={request.Status}";
      }

      if (!string.IsNullOrEmpty(request.TimeOffTypeId))
      {
        url += $"&timeOffTypeId={request.TimeOffTypeId}";
      }

      var data = await GetAsync<PagedResult<TimeOffViewModel>>(url);
      return data;
    }

    public async Task<PagedResult<TimeOffViewModel>> GetPagingsByUser(TimeOffPagingRequest request)
    {
      var url = $"/api/timeOffRequests/paging-user?pageIndex={request.PageIndex}" + $"&pageSize={request.PageSize}";
      if (!string.IsNullOrEmpty(request.UserId))
      {
        url += $"&userId={request.UserId}";
      }

      if (!string.IsNullOrEmpty(request.FromDate))
      {
        url += $"&fromDate={request.FromDate}";
      }

      if (!string.IsNullOrEmpty(request.ToDate))
      {
        url += $"&toDate={request.ToDate}";
      }

      if (!string.IsNullOrEmpty(request.Status))
      {
        url += $"&status={request.Status}";
      }

      if (!string.IsNullOrEmpty(request.TimeOffTypeId))
      {
        url += $"&timeOffTypeId={request.TimeOffTypeId}";
      }

      var data = await GetAsync<PagedResult<TimeOffViewModel>>(url);
      return data;
    }

    public async Task<bool> UpdateTimeOff(TimeOffUpdateRequest request)
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
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TimeOffTypeId) ? "" : request.TimeOffTypeId.ToString()), "timeOffTypeId");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.RequestUnit) ? "" : request.RequestUnit.ToString()), "requestUnit");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.HalfDay) ? "" : request.HalfDay.ToString()), "halfDay");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserId) ? "" : request.UserId.ToString()), "userId");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Duration) ? "" : request.Duration.ToString()), "duration");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Status) ? "" : request.Status.ToString()), "status");
      requestContent.Add(new StringContent(request.FromDate.ToString()), "fromDate");
      requestContent.Add(new StringContent(request.ToDate.ToString()), "toDate");

      var response = await client.PutAsync($"/api/timeOffRequests/" + request.Id, requestContent);
      return response.IsSuccessStatusCode;
    }
  }
}

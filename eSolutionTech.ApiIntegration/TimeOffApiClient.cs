using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests.Dtos;
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
      requestContent.Add(new StringContent(request.TimeOffType.ToString()), "timeOffType");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserId.ToString()) ? "" : request.UserId.ToString()), "userId");
      requestContent.Add(new StringContent(request.Duration.ToString()), "duration");
      requestContent.Add(new StringContent(request.FromDate.ToString()), "fromDate");
      requestContent.Add(new StringContent(request.ToDate.ToString()), "toDate");

      var response = await client.PostAsync($"/api/timeOffRequests/", requestContent);
      return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTimeOff(int id)
    {
      return await Delete($"/api/timeOffRequests/" + id);
    }

    public async Task<List<TimeOffViewModel>> GetAll()
    {
      var data = await GetAsync<List<TimeOffViewModel>>(
        $"/api/timeOffRequests");

      return data;
    }

    public async Task<TimeOffViewModel> GetById(int id)
    {
      var data = await GetAsync<TimeOffViewModel>($"/api/timeOffRequests/{id}");

      return data;
    }

    public async Task<PagedResult<TimeOffViewModel>> GetPagings(TimeOffPagingRequest request)
    {
      var data = await GetAsync<PagedResult<TimeOffViewModel>>(
            $"/api/timeOffTypes/paging?pageIndex={request.PageIndex}" +
            $"&pageSize={request.PageSize}" +
            $"&keyword={request.UserId}");

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
      requestContent.Add(new StringContent(request.TimeOffType.ToString()), "timeOffType");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserId.ToString()) ? "" : request.UserId.ToString()), "userId");
      requestContent.Add(new StringContent(request.Duration.ToString()), "duration");
      requestContent.Add(new StringContent(request.FromDate.ToString()), "fromDate");
      requestContent.Add(new StringContent(request.ToDate.ToString()), "toDate");

      var response = await client.PutAsync($"/api/timeOffRequests/", requestContent);
      return response.IsSuccessStatusCode;
    }
  }
}

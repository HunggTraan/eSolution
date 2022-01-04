using eSolutionTech.ViewModels.Catalog.Shifts;
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
  public class ShiftApiClient : BaseApiClient, IShiftApiClient
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ShiftApiClient(IHttpClientFactory httpClientFactory,
       IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) : base(httpClientFactory,
           httpContextAccessor,
           configuration)
    {
      _httpContextAccessor = httpContextAccessor;
      _configuration = configuration;
      _httpClientFactory = httpClientFactory;
    }
    public async Task<bool> DeleteShift(int id)
    {
      return await Delete($"/api/shift/" + id);
    }

    public async Task<ShiftViewModel> GetById(int id)
    {
      var data = await GetAsync<ShiftViewModel>($"/api/shifts/{id}");

      return data;
    }

    public async Task<PagedResult<ShiftManageViewModel>> GetPagings(GetShiftPagingRequest request)
    {
      var url = $"/api/shifts/paging?pageIndex={request.PageIndex}" + $"&pageSize={request.PageSize}";

      if (!string.IsNullOrEmpty(request.UserId))
      {
        url += $"&userId={request.UserId}";
      }

      if (!string.IsNullOrEmpty(request.Code))
      {
        url += $"&code={request.Code}";
      }

      if (!string.IsNullOrEmpty(request.FullName))
      {
        url += $"&fullName={request.FullName}";
      }

      if (!string.IsNullOrEmpty(request.DepartmentId))
      {
        url += $"&departmentId={request.DepartmentId}";
      }

      if (!string.IsNullOrEmpty(request.JobTitleId))
      {
        url += $"&jobTitleId={request.JobTitleId}";
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

      if (!string.IsNullOrEmpty(request.IsLate))
      {
        url += $"&isLate={request.IsLate}";
      }

      var data = await GetAsync<PagedResult<ShiftManageViewModel>>(url);
      return data;
    }

    public async Task<string> LoginShift(ShiftCreateRequest request)
    {
      var sessions = _httpContextAccessor
          .HttpContext
          .Session
          .GetString(Constants.Constants.Token);

      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var requestContent = new MultipartFormDataContent();

      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserId) ? "" : request.UserId.ToString()), "userId");
      requestContent.Add(new StringContent(request.ProjectId.ToString()), "projectId");

      var response = await client.PostAsync($"/api/shifts/", requestContent);
      return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> LogoutShift(ShiftUpdateRequest request)
    {
      var sessions = _httpContextAccessor
          .HttpContext
          .Session
          .GetString(Constants.Constants.Token);

      var client = _httpClientFactory.CreateClient();
      client.BaseAddress = new Uri(_configuration[Constants.Constants.BASEADDRESS_API]);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, sessions);

      var requestContent = new MultipartFormDataContent();

      requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserId) ? "" : request.UserId.ToString()), "userId");
      requestContent.Add(new StringContent(request.ProjectId.ToString()), "projectId");

      var response = await client.PostAsync($"/api/shifts/logout", requestContent);
      return await response.Content.ReadAsStringAsync();
    }
  }
}

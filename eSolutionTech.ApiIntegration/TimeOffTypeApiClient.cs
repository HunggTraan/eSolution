using eSolutionTech.ViewModels.Catalog.TimeOffTypes;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos;
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
    public class TimeOffTypeApiClient : BaseApiClient, ITimeOffTypeApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TimeOffTypeApiClient(IHttpClientFactory httpClientFactory,
           IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory,
               httpContextAccessor,
               configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateTimeOffType(TimeOffTypeCreateRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.RequestUnit) ? "" : request.RequestUnit.ToString()), "requestUnit");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.StartDateStr) ? "" : request.StartDateStr.ToString()), "startDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.EndDateStr) ? "" : request.EndDateStr.ToString()), "endDate");
            requestContent.Add(new StringContent(request.Unpaid.ToString()), "unpaid");

            var response = await client.PostAsync($"/api/timeOffTypes/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTimeOffType(int id)
        {
            return await Delete($"/api/timeOffTypes/" + id);
        }

        public async Task<List<TimeOffTypeViewModel>> GetAll()
        {
            var data = await GetAsync<List<TimeOffTypeViewModel>>(
                    $"/api/timeOffTypes");

            return data;
        }

        public async Task<TimeOffTypeViewModel> GetById(int id)
        {
            var data = await GetAsync<TimeOffTypeViewModel>($"/api/timeOffTypes/{id}");

            return data;
        }

        public async Task<PagedResult<TimeOffTypeViewModel>> GetPagings(GetTimeOffTypePagingRequest request)
        {
            var data = await GetAsync<PagedResult<TimeOffTypeViewModel>>(
                        $"/api/timeOffTypes/paging?pageIndex={request.PageIndex}" +
                        $"&pageSize={request.PageSize}" +
                        $"&keyword={request.KeyWord}");

            return data;
        }

        public async Task<bool> UpdateTimeOffType(TimeOffTypeUpdateRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.RequestUnit) ? "" : request.RequestUnit.ToString()), "requestUnit");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.StartDateStr) ? "" : request.StartDateStr.ToString()), "startDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.EndDateStr) ? "" : request.EndDateStr.ToString()), "endDate");
            requestContent.Add(new StringContent(request.Unpaid.ToString()), "unpaid");

            var response = await client.PutAsync($"/api/timeOffTypes/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}

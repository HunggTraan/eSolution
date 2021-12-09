using eSolutionTech.ViewModels.Catalog.JobTitles;
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
    public class JobTitleApiClient : BaseApiClient, IJobTitleApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public JobTitleApiClient(IHttpClientFactory httpClientFactory,
           IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory,
               httpContextAccessor,
               configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateJobTitle(JobTitleCreateRequest request)
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

            var response = await client.PostAsync($"/api/jobTitles/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteJobTitle(int id)
        {
            return await Delete($"/api/jobTitles/" + id);
        }

        public async Task<List<JobTitleViewModel>> GetAll()
        {
            var data = await GetAsync<List<JobTitleViewModel>>(
                    $"/api/jobTitles");

            return data;
        }

        public async Task<JobTitleViewModel> GetById(int id)
        {
            var data = await GetAsync<JobTitleViewModel>($"/api/jobTitles/{id}");

            return data;
        }

        public async Task<PagedResult<JobTitleViewModel>> GetPagings(GetJobTitlePagingRequest request)
        {
            var data = await GetAsync<PagedResult<JobTitleViewModel>>(
                        $"/api/jobTitles/paging?pageIndex={request.PageIndex}" +
                        $"&pageSize={request.PageSize}" +
                        $"&keyword={request.KeyWord}");

            return data;
        }

        public async Task<bool> UpdateJobTitle(JobTitleUpdateRequest request)
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

            var response = await client.PutAsync($"/api/jobTitles/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}

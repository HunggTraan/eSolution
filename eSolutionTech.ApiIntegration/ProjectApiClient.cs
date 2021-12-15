using eSolutionTech.ViewModels.Catalog.Projects;
using eSolutionTech.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
    public class ProjectApiClient : BaseApiClient, IProjectApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProjectApiClient(IHttpClientFactory httpClientFactory,
           IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory,
               httpContextAccessor,
               configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateProject(ProjectCreateRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ManagerId) ? "" : request.ManagerId.ToString()), "managerId");
            requestContent.Add(new StringContent(request.StartDate.ToString()), "startDate");
            requestContent.Add(new StringContent(request.EndDate.ToString()), "endDate");
            requestContent.Add(new StringContent(JsonConvert.SerializeObject(request.UserIds)), "userIds");

            var response = await client.PostAsync($"/api/projects/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProject(int id)
        {
            return await Delete($"/api/projects/" + id);
        }

        public async Task<List<ProjectViewModel>> GetAll()
        {
            var data = await GetAsync<List<ProjectViewModel>>(
                $"/api/projects");

            return data;
        }

        public async Task<ProjectViewModel> GetById(int id)
        {
            var data = await GetAsync<ProjectViewModel>($"/api/projects/{id}");

            return data;
        }

        public async Task<PagedResult<ProjectViewModel>> GetPagings(GetProjectPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProjectViewModel>>(
            $"/api/projects/paging?pageIndex={request.PageIndex}" +
            $"&pageSize={request.PageSize}" +
            $"&keyword={request.KeyWord}");

            return data;
        }

        public async Task<bool> UpdateProject(ProjectUpdateRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ManagerId) ? "" : request.ManagerId.ToString()), "managerId");
            requestContent.Add(new StringContent(request.StartDate.ToString()), "startDate");
            requestContent.Add(new StringContent(request.EndDate.ToString()), "endDate");
            requestContent.Add(new StringContent(JsonConvert.SerializeObject(request.UserIds)), "userIds");

            var response = await client.PutAsync($"/api/projects/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}

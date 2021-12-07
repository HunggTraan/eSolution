using eSolutionTech.ViewModels.Catalog.Departments;
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
    public class DepartmentApiClient : BaseApiClient, IDepartmentApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DepartmentApiClient(IHttpClientFactory httpClientFactory,
           IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration): base(httpClientFactory,
               httpContextAccessor,
               configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateDepartment(DepartmentCreateRequest request)
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

            var response = await client.PostAsync($"/api/departments/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await Delete($"/api/departments/" + id);
        }

        public async Task<List<DepartmentViewModel>> GetAll(GetDepartmentPagingRequest request)
        {
            var data = await GetAsync<List<DepartmentViewModel>>(
                    $"/api/departments");

            return data;
        }

        public async Task<DepartmentViewModel> GetById(int id)
        {
            var data = await GetAsync<DepartmentViewModel>($"/api/departments/{id}");

            return data;
        }

        public async Task<PagedResult<DepartmentViewModel>> GetPagings(GetDepartmentPagingRequest request)
        {
            var data = await GetAsync<PagedResult<DepartmentViewModel>>(
                        $"/api/departments/paging?pageIndex={request.PageIndex}" +
                        $"&pageSize={request.PageSize}" +
                        $"&keyword={request.KeyWord}");

            return data;
        }

        public async Task<bool> UpdateDepartment(DepartmentUpdateRequest request)
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

            var response = await client.PutAsync($"/api/departments/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}

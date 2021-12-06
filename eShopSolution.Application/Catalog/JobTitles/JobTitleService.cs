using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.JobTitles.Dtos;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.JobTitles
{
    public class JobTitleService : IJobTitleService
    {
        private readonly eTechDbContext _context;
        public JobTitleService(eTechDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(JobTitleCreateRequest request)
        {
            var jobTitle = new JobTitle()
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
            };
            _context.JobTitles.Add(jobTitle);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int jobTitleId)
        {
            var jobTitle = await _context.JobTitles.FindAsync(jobTitleId);
            if (jobTitle == null)
                throw new eTechException($"Không tìm thấy chức danh với id là {jobTitleId}");
            _context.JobTitles.Remove(jobTitle);
            return await _context.SaveChangesAsync();
        }

        public Task<List<JobTitleViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<JobTitleViewModel>> GetAllPaging(GetJobTitlePagingRequest request)
        {
            var query = from jobTitle in _context.JobTitles
                        select new { jobTitle };
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.jobTitle.Code.Contains(request.KeyWord) || x.jobTitle.Name.Contains(request.KeyWord));

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new JobTitleViewModel()
                {
                    Id = x.jobTitle.Id,
                    Name = x.jobTitle.Name,
                    Code = x.jobTitle.Code,
                    Description = x.jobTitle.Description
                }).ToListAsync();

            var pagedResult = new PagedResult<JobTitleViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<JobTitleViewModel> GetById(int jobTitleId)
        {
            var jobTitle = await _context.JobTitles.FindAsync(jobTitleId);

            var jobTitleViewModel = new JobTitleViewModel()
            {
                Id = jobTitle.Id,
                Name = jobTitle.Name,
                Code = jobTitle.Code,
                Description = jobTitle.Description,
            };
            return jobTitleViewModel;
        }

        public async Task<int> Update(JobTitleUpdateRequest request)
        {
            var jobTitle = await _context.JobTitles.FindAsync(request.Id);
            if (jobTitle == null) throw new eTechException($"Không tìm thấy chức danh với id: {request.Id}");

            if (string.IsNullOrEmpty(request.Name))
                throw new eTechException($"Tên không được để trống");
            if (string.IsNullOrEmpty(request.Code))
                throw new eTechException($"Mã không được để trống");
            jobTitle.Name = request.Name;
            jobTitle.Code = request.Code;
            jobTitle.Description = request.Description;

            return await _context.SaveChangesAsync();
        }
    }
}

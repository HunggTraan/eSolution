using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly eTechDbContext _context;
        public ProjectService(eTechDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ProjectCreateRequest request)
        {
            try
            {
                var project = new Project()
                {
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                    ManagerId = request.ManagerId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    StartIn = request.StartIn,
                    StartOut = request.StartOut,
                    EndIn = request.EndIn,
                    EndOut = request.EndOut,
                    Status = request.Status
                };
                _context.Projects.Add(project);

                foreach (var userId in request.UserIds)
                {
                    var memberInProject = new MemberInProject()
                    {
                        MemberId = Int32.Parse(userId),
                        ProjectId = project.Id
                    };
                    _context.MemberInProject.Add(memberInProject);
                }
                return await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new eTechException("Có lỗi xảy ra");
            }
        }

        public async Task<int> Delete(int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);
                if (project != null)
                {
                    _context.Projects.Remove(project);

                    var memberInProjects = _context.MemberInProject.Where(x => x.ProjectId == projectId);
                    foreach (var member in memberInProjects)
                    {
                        _context.MemberInProject.Remove(member);
                    }
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    throw new eTechException($"Không tìm thấy dự án với id là {projectId}");
                }
            }
            catch(Exception ex)
            {
                throw new eTechException("Có lỗi xảy ra");
            }
        }

        public Task<List<ProjectViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProjectViewModel>> GetAllPaging(GetProjectPagingRequest request)
        {
            var query = from project in _context.Projects 
                        join memberInProject in _context.MemberInProject on project.Id equals memberInProject.ProjectId
                        select new { project, memberInProject };

            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.project.Code.Contains(request.KeyWord) || x.project.Name.Contains(request.KeyWord));

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProjectViewModel()
                {
                    Id = x.project.Id,
                    Name = x.project.Name,
                    Code = x.project.Code,
                    Description = x.project.Description,
                    ManagerId = x.project.ManagerId,
                    StartDate = x.project.StartDate,
                    EndDate = x.project.EndDate,
                    StartIn = x.project.StartIn,
                    StartOut = x.project.StartOut,
                    EndIn = x.project.EndIn,
                    EndOut = x.project.EndOut,
                    Status = x.project.Status,
                }).ToListAsync();

            var pagedResult = new PagedResult<ProjectViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProjectViewModel> GetById(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            var projectViewModel = new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                Code = project.Code,
                Description = project.Description,
                ManagerId = project.ManagerId,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                StartIn = project.StartIn,
                StartOut = project.StartOut,
                EndIn = project.EndIn,
                EndOut = project.EndOut,
                Status = project.Status
            };
            return projectViewModel;
        }

        public async Task<int> Update(ProjectUpdateRequest request)
        {
            var project = await _context.Projects.FindAsync(request.Id);
            if (project == null) throw new eTechException($"Không tìm thấy dự án với id: {request.Id}");

            project.Name = request.Name;
            project.Code = request.Code;
            project.Description = request.Description;
            project.ManagerId = request.ManagerId;
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;
            project.StartIn = request.StartIn;
            project.StartOut = request.StartOut;
            project.EndIn = request.EndIn;
            project.EndOut = request.EndOut;
            project.Status = request.Status;

            return await _context.SaveChangesAsync();
        }
    }
}

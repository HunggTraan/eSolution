using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        string[] userIds = new string[0];
        if (request.UserIds != null && request.UserIds.Length > 0)
        {
          userIds = JsonConvert.DeserializeObject<string[]>(request.UserIds[0]);
        }

        var project = new Project()
        {
          Name = request.Name,
          Code = request.Code,
          Description = request.Description,
          ManagerId = request.ManagerId,
          shiftSettingId = request.shiftSettingId,
          StartDate = request.StartDate,
          EndDate = request.EndDate,
          Status = request.Status
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        if (userIds != null && userIds.Length > 0)
        {
          foreach (var userId in userIds)
          {
            var memberInProject = new MemberInProject()
            {
              MemberId = userId,
              ProjectId = project.Id
            };
            _context.MemberInProject.Add(memberInProject);
          }
          if(!string.IsNullOrEmpty(request.ManagerId)){
            var memberInProject = new MemberInProject()
            {
              MemberId = request.ManagerId,
              ProjectId = project.Id
            };
            _context.MemberInProject.Add(memberInProject);
          }
        }
        return await _context.SaveChangesAsync();
      }
      catch (Exception ex)
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
      catch (Exception ex)
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
      //var query = (from project in _context.Projects 
      //            join memberInProject in _context.MemberInProject on project.Id equals memberInProject.ProjectId
      //            select new { project, memberInProject }).Distinct();

      var query = from project in _context.Projects
                  join shiftSetting in _context.ShiftSettings on project.shiftSettingId equals shiftSetting.Id
                  select new { project, shiftSetting };

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
            shiftSetting = x.shiftSetting.Name,
            StartDate = x.project.StartDate,
            EndDate = x.project.EndDate,
            Status = x.project.Status,
          }).ToListAsync();

      var pagedResult = new PagedResult<ProjectViewModel>()
      {
        TotalRecords = totalRow,
        Items = data
      };
      return pagedResult;
    }

    public async Task<PagedResult<ProjectViewModel>> GetAllPagingByUser(GetProjectPagingRequest request)
    {
      var query = (from memberInProject in _context.MemberInProject
                   join project in _context.Projects on memberInProject.ProjectId equals project.Id
                   select new { project, memberInProject }).Distinct();

      if (!string.IsNullOrEmpty(request.UserId))
        query = query.Where(x => x.memberInProject.MemberId.Contains(request.UserId));

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
            Status = x.project.Status
          }).ToListAsync();

      var pagedResult = new PagedResult<ProjectViewModel>()
      {
        TotalRecords = totalRow,
        Items = data
      };
      return pagedResult;
    }

    public async Task<ProjectViewModel> GetById(int projectId)
    {
      var project = await _context.Projects.FindAsync(projectId);

      var memberInProject = _context.MemberInProject.Where(x => x.ProjectId == projectId).Select(x => x.MemberId).ToList();

      string[] userId = memberInProject.ToArray();

      var projectViewModel = new ProjectViewModel()
      {
        Id = project.Id,
        Name = project.Name,
        Code = project.Code,
        Description = project.Description,
        ManagerId = project.ManagerId,
        shiftSettingId = project.shiftSettingId,
        StartDate = project.StartDate,
        EndDate = project.EndDate,
        Status = project.Status,
        UserIds = userId
      };
      return projectViewModel;
    }

    public async Task<int> Update(ProjectUpdateRequest request)
    {
      var project = await _context.Projects.FindAsync(request.Id);
      if (project == null) throw new eTechException($"Không tìm thấy dự án với id: {request.Id}");

      string[] userIds = new string[0];
      if (request.UserIds != null && request.UserIds.Length > 0)
      {
        userIds = JsonConvert.DeserializeObject<string[]>(request.UserIds[0]);
      }

      project.Name = request.Name;
      project.Code = request.Code;
      project.Description = request.Description;
      project.ManagerId = request.ManagerId;
      project.shiftSettingId = request.shiftSettingId;
      project.StartDate = request.StartDate;
      project.EndDate = request.EndDate;
      project.Status = request.Status;

      var memberInProjects = _context.MemberInProject.Where(x => x.ProjectId == request.Id).ToArray();

      List<string> userIdFilter = new List<string>();
      if (userIds != null && userIds.Length > 0)
      {
        foreach (var members in memberInProjects)
        {
          if (Array.IndexOf(userIds, members.MemberId) > 0)
          {
            userIds.ToList().Remove(members.MemberId);
          }
          else
          {
            _context.MemberInProject.Remove(members);
          }
        }
      }

      foreach (var item in userIds)
      {
        var memberInProject = new MemberInProject()
        {
          MemberId = item,
          ProjectId = project.Id
        };
        _context.MemberInProject.Add(memberInProject);
      }

      return await _context.SaveChangesAsync();
    }
  }
}

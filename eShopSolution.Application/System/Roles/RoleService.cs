using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.System.Roles
{
  public class RoleService : IRoleService
  {
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleManager)
    {
      _roleManager = roleManager;
    }

    public async Task<ApiResult<bool>> CreateRole(CreateRoleRequest request)
    {
      try
      {
        var role = await _roleManager.FindByNameAsync(request.Name);
        if (role != null)
        {
          return new ApiErrorResult<bool>("Tên vai trò trùng");
        }

        var stamp = new Guid();

        role = new Role()
        {
          Name = request.Name,
          Description = request.Description,
          NormalizedName = request.Name.ToLower(),
          ConcurrencyStamp = stamp.ToString()
        };

        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
          return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Tạo mới vai trò không thành công");
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<bool>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<bool>> Delete(string id)
    {
      try
      {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
          var message = string.Format(Constants.Message.NOTFOUND, "vai trò");
          return new ApiErrorResult<bool>(message);
        }

        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
          return new ApiSuccessResult<bool>();

        return new ApiErrorResult<bool>(Constants.Message.DELETENOTSUCCESS);
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<bool>(Constants.Message.ERROR);
      }
    }

    public async Task<List<RoleVm>> GetAll()
    {
      var roles = await _roleManager.Roles
          .Select(x => new RoleVm()
          {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
          }).ToListAsync();

      return roles;
    }

    public async Task<ApiResult<PagedResult<RoleVm>>> GetRolesPaging(RolePagingRequest request)
    {
      try
      {
        var query = from roles in _roleManager.Roles
                    select new { roles };

        if (!string.IsNullOrEmpty(request.Keyword))
        {
          query = query.Where(x => x.roles.Name.Contains(request.Keyword) || x.roles.Description.Contains(request.Keyword));
        }

        query = query.OrderBy(x => x.roles.Name);

        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new RoleVm()
            {
              Id = x.roles.Id,
              Name = x.roles.Name,
              Description = x.roles.Description,
              NormalizedName = x.roles.NormalizedName,
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<RoleVm>()
        {
          TotalRecords = totalRow,
          PageIndex = request.PageIndex,
          PageSize = request.PageSize,
          Items = data
        };

        return new ApiSuccessResult<PagedResult<RoleVm>>(pagedResult);
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<PagedResult<RoleVm>>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request)
    {
      try
      {
        if (await _roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != id))
        {
          return new ApiErrorResult<bool>("Vai trò đã tồn tại");
        }

        var role = await _roleManager.FindByIdAsync(id.ToString());

        role.Name = request.Name;
        role.Description = request.Description;
        role.NormalizedName = request.Name.ToLower();

        var result = await _roleManager.UpdateAsync(role);
        if (result.Succeeded)
        {
          return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Cập nhật không thành công");
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<bool>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<RoleVm>> GetById(Guid id)
    {
      try
      {
        var roleId = ReplacingGuid(id.ToString());
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
        {
          var message = string.Format(Constants.Message.NOTFOUND, "Vai trò");
          return new ApiErrorResult<RoleVm>(message);
        }

        var userVm = new RoleVm()
        {
          Id = role.Id,
          Name = role.Name,
          Description = role.Description,
          NormalizedName = role.NormalizedName
        };
        return new ApiSuccessResult<RoleVm>(userVm);
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<RoleVm>(Constants.Message.ERROR);
      }
    }
    public string ReplacingGuid(string Guid)
    {
      var charsToRemove = new string[] { "{", "}" };
      foreach (var c in charsToRemove)
      {
        Guid = Guid.Replace(c, string.Empty);
      }
      return Guid;
    }
  }
}

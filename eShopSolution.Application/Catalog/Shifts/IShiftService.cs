using eSolutionTech.Application.Catalog.Shifts.Dtos;
using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.Shifts
{
    public interface IShiftService
    {
        Task<int> Create(ShiftCreateRequest request);
        Task<int> Update(ShiftUpdateRequest request);
        Task<int> Delete(int shiftId);
        Task<List<ShiftViewModel>> GetAll();
        //Task<List<ShiftViewModel>> GetAllByProjectId(GetShiftPagingRequest request);
        //Task<List<ShiftViewModel>> GetAllByUserId(GetShiftPagingRequest request);
        Task<PagedResult<ShiftViewModel>> GetAllPaging(GetShiftPagingRequest request);
        Task<ShiftViewModel> GetById(int shiftId);
    }
}

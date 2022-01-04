using eSolutionTech.ViewModels.Common;
using System;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
  public class GetShiftPagingRequest : PagingRequestBase
  {
    public string ProjectId { get; set; }
    public string UserId { get; set; }
    public string Code { get; set; }
    public string FullName { get; set; }
    public string DepartmentId { get; set; }
    public string JobTitleId { get; set; }
    public string Status { get; set; }
    public string IsLate { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
  }
}

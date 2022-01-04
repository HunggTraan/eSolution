using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.TimeOffRequests
{
  public class TimeOffPagingRequest : PagingRequestBase
  {
    public string Status { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string TimeOffTypeId { get; set; }
    public string UserId { get; set; }
  }
}

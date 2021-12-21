using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.TimeOffRequests
{
  public class TimeOffPagingRequest : PagingRequestBase
  {
    public string UserId { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public bool IsAdmin { get; set; }
  }
}

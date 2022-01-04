using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.System.Roles
{
  public class RolePagingRequest : PagingRequestBase
  {
    public string Keyword { get; set; }
  }
}

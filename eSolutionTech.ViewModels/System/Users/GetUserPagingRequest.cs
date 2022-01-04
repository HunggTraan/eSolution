using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string DepartmentId { get; set; }
        public string JobTitleId { get; set; }
    }
}

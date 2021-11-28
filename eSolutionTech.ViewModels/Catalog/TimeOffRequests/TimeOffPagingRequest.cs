using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.TimeOffRequests
{
    public class TimeOffPagingRequest : PagingRequestBase
    {
        public Guid UserId { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}

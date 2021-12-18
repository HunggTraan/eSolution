using eSolutionTech.ViewModels.Common;
using System;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
    public class GetShiftPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
    }
}

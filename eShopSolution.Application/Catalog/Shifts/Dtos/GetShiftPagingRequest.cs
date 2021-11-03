using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.Shifts.Dtos
{
    public class GetShiftPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
        public int ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}

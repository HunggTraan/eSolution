using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.TimeOffTypes.Dtos
{
    public class GetTimeOffTypePagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}

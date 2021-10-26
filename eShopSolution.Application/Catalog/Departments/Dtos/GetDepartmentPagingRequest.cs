using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.Departments.Dtos
{
    public class GetDepartmentPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}

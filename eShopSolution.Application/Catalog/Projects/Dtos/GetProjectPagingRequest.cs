using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.Projects.Dtos
{
    public class GetProjectPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}

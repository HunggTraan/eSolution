using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.JobTitles.Dtos
{
    public class GetJobTitlePagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}

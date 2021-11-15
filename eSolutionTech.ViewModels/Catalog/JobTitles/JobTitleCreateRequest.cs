using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.JobTitles.Dtos
{
    public class JobTitleCreateRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}

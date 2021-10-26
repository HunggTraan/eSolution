﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.Departments.Dtos
{
    public class DepartmentCreateRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}

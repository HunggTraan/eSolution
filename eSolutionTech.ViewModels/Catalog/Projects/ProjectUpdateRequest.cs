using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.Projects
{
    public class ProjectUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }
        public DateTime StartOut { get; set; }
        public DateTime EndOut { get; set; }
    }
}

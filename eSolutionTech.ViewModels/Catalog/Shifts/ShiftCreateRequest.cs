using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
    public class ShiftCreateRequest
    {
        public int ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string WorkingHours { get; set; }
        public string Comment { get; set; }
        public string Activity { get; set; }
    }
}

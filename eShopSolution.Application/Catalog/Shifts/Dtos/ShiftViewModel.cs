using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Application.Catalog.Shifts.Dtos
{
    public class ShiftViewModel
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string WorkingHours { get; set; }
        public string Comment { get; set; }
        public string Activity { get; set; }

    }
}

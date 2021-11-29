using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
    public class ShiftUpdateRequest
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string WorkingHours { get; set; }
        public string Comment { get; set; }
        public string Activity { get; set; }
        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }
        public DateTime StartOut { get; set; }
        public DateTime EndOut { get; set; }
    }
}

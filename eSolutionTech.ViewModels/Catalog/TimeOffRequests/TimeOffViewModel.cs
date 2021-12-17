using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.TimeOffRequests.Dtos
{
    public class TimeOffViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TimeOffType { get; set; }
        public Guid UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string AdminNote { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
    }
}

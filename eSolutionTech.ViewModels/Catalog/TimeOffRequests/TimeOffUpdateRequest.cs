using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.TimeOffRequests
{
    public class TimeOffUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeOffType { get; set; }
        public Guid UserId { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string AdminNote { get; set; }
        public string Duration { get; set; }
        public DateTime FromHour { get; set; }
        public DateTime ToHour { get; set; }
        public string RequestUnit { get; set; }
        public string Status { get; set; }
    }
}

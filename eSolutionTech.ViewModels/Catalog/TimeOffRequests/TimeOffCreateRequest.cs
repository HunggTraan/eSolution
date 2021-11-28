using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.TimeOffRequests
{
    public class TimeOffCreateRequest
    {
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
        public string FromDateStr { get; set; }
        public string ToDateStr { get; set; }
        public string FromHourStr { get; set; }
        public string ToHourStr { get; set; }
    }
}

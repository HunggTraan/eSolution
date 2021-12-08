using System;

namespace eSolutionTech.ViewModels.Catalog.TimeOffTypes
{
    public class TimeOffTypeUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string RequestUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }
        public bool Unpaid { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
    public class ShiftViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string WorkingHours { get; set; }
        public string Comment { get; set; }
        public string Activity { get; set; }

    }
}

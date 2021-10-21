﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eStolutionTech.Data.Entities
{
    [Table("TimeOffRequests")]
    public class TimeOffRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TimeOffType { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        public string AdminNote { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public string Duration { get; set; }
        public string FromHour { get; set; }
        public string ToHour { get; set; }
        public string RequestUnit { get; set; }
        public string Status { get; set; }
    }
}

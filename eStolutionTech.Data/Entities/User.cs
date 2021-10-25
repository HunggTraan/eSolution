using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eStolutionTech.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Code { get; set; }
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public string JobTitleId { get; set; }
        public List<Shift> Shifts { get; set; }
        public List<TimeOffRequest> TimeOffRequests { get; set; }
    }
}

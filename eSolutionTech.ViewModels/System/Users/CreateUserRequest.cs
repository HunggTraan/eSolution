using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSolutionTech.ViewModels.System.Users
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string Phone { get; set; }

        [DataType(DataType.Date), Display(Name = "My date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DobString { get; set; }
        public DateTime Dob { get; set; }
        public string Code { get; set; }
        public string DepartmentId { get; set; }
        public string JobTitleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
}

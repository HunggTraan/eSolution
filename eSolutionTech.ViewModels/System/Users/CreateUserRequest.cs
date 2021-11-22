using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.System.Users
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
        public string Code { get; set; }
        public string DepartmentId { get; set; }
        public string JobTitleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSolutionTech.ViewModels.System.Users
{
  public class UserViewModel
  {
    public Guid Id { get; set; }

    [Display(Name = "Họ và tên")]
    public string FullName { get; set; }

    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Tài khoản")]
    public string UserName { get; set; }
    [Display(Name = "Mã nhân viên")]
    public string Code { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Ngày sinh")]
    public DateTime Dob { get; set; }
    [Display(Name = "Chức vụ")]
    public string JobTitle { get; set; }
    [Display(Name = "Phòng ban")]
    public string Department { get; set; }

    public IList<string> Roles { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
  public class ShiftViewModel
  {
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string UserId { get; set; }
    public DateTime TimeIn { get; set; }
    public string Time { get { return TimeIn.ToString("dd/MM/yyyy"); } }
    public DateTime TimeOut { get; set; }
    public string WorkingHours { get; set; }
    public int Status { get; set; }
    public int isLate { get; set; }
    public string UserName { get; set; }
    public string ProjectName { get; set; }
    public string UserCode { get; set; }
    public string UserFullName { get; set; }
    public string Department { get; set; }
    public string JobTitle { get; set; }
    public string StatusText
    {
      get
      {
        return GetStatusName();
      }
    }

    public string IsLateText
    {
      get
      {
        return GetIsLateName();
      }
    }
    public string GetStatusName()
    {
      string result = Status switch
      {
        1 => "Đã chấm công vào",
        2 => "Đã chấm đủ công",
        _ => String.Empty,
      };
      return result;
    }
    public string GetIsLateName()
    {
      string result = Status switch
      {
        0 => "Chấm công muộn",
        1 => "Chấm công đúng giờ",
        _ => String.Empty,
      };
      return result;
    }
  }
}

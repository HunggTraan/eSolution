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
    public DateTime TimeOut { get; set; }
    public string WorkingHours { get; set; }
    public int Status { get; set; }
    public int isLate { get; set; }
    public string UserName { get; set; }
    public string ProjectName { get; set; }

  }
}

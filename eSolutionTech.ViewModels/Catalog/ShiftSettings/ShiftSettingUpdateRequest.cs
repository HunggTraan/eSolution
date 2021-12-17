using System;
using System.ComponentModel.DataAnnotations;

namespace eSolutionTech.ViewModels.Catalog.ShiftSettings
{
  public class ShiftSettingUpdateRequest
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string TimeIn { get; set; }
    public int ExceedTimeIn { get; set; }
    public string TimeOut { get; set; }
    public int ExceedTimeOut { get; set; }
  }
}

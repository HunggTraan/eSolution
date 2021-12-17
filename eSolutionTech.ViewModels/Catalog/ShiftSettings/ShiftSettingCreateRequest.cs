﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.Catalog.ShiftSettings
{
  public class ShiftSettingCreateRequest
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string TimeIn { get; set; }
    public int ExceedTimeIn { get; set; }
    public string TimeOut { get; set; }
    public int ExceedTimeOut { get; set; }
  }
}

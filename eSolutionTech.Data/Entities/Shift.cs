using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eSolutionTech.Data.Entities
{
  [Table("Shifts")]
  public class Shift
  {
    public int Id { get; set; }
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime TimeOut { get; set; }
    public string WorkingHours { get; set; }
  }
}

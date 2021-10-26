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
        public string ProjectId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string WorkingHours { get; set; }
        [Required]
        public string Comment { get; set; }
        public string Activity { get; set; }
        public User User { get; set; }
    }
}

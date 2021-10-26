using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eSolutionTech.Data.Entities
{
    [Table("ShiftTypes")]
    public class ShiftType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartIn { get; set; }
        [Required]
        public DateTime EndIn { get; set; }
        [Required]
        public DateTime StartOut { get; set; }
        [Required]
        public DateTime EndOut { get; set; }
    }
}

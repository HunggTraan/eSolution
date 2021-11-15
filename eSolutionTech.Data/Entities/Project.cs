using eSolutionTech.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eSolutionTech.Data.Entities
{
    [Table("Projects")]
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public string ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
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

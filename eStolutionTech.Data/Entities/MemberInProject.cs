using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eSolutionTech.Data.Entities
{
    [Table("MemberInProjects")]
    public class MemberInProject
    {
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}

using eSolutionTech.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSolutionTech.Data.Entities
{
    [Table("JobTitles")]
    public class JobTitle
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public Status status { get; set; }
    }
}

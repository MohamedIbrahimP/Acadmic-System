using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_day2.Models
{
    public class Trainee
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public byte[]? image { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        [MaxLength(10)]
        public string? Grade { get; set; }


        [Display(Name = "Department Name")]
        [ForeignKey("Department")]
        public int dept_id { get; set; }
        public virtual Department? Department { get; set; }
    }
}

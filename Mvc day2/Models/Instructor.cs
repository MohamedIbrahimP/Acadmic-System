using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Mvc_day2.Annotations;

namespace Mvc_day2.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [MinLength(3,ErrorMessage ="Name Must Be More Than 3!")]
        [Required(ErrorMessage ="Please Fill The Name!")]
        public string? Name { get; set; }
        //public string? image { get; set; }
        public byte[]? profileImage { get; set; }
       


        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please Fill The Address!")]
        public string? Address { get; set; }


        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int dept_id { get; set; }

        public virtual Department? Department { get; set; }



        [ForeignKey("Course")]
        [Display(Name="Course Name")]
        public int crs_id { get; set; }

        public virtual Course? Course { get; set; }
    }
}

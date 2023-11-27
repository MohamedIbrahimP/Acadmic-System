using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mvc_day2.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name Required")]
        //[Unique]
        [MinLength(3,ErrorMessage ="tha lenth must be between 3 and 25")]
        [MaxLength(25, ErrorMessage = "tha lenth must be between 3 and 25")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Degree Required")]
        [Range(minimum:50,maximum:100, ErrorMessage = "Degree must be between 50 and 100! ")]
        public int? Degree { get; set; }

        [Required(ErrorMessage = "Min Degree Required")]
        [Remote("CheckMinDegree", "Course", ErrorMessage ="Min Degree Must Be More Than Degree! ",AdditionalFields ="Degree")]
        public int? MinDegree { get; set; }
        [MaxLength(10)]
        public string? Grade { get; set; }
        public int? Hours { get; set; }

        [Display(Name="Department Name")]
        [ForeignKey("Department")]
        public int dept_id { get; set; }
        public virtual Department? Department { get; set; }
    }
}

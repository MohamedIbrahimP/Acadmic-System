using Mvc_day2.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Mvc_day2.Models
{
    public class Department
    {
       

        [Key]
        public int Id { get; set; }
        [MinLength(3,ErrorMessage ="The Name Must Be More Than 3!")]
        [MaxLength(50)]
        [Required(ErrorMessage ="The Name Of Department Is Req!")]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Manager { get; set; }
    }
}

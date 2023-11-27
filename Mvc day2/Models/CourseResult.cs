using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_day2.Models
{
    public class CourseResult
    {
        [Key]
        public int Id { get; set; }
        public double degree { get; set; }



        [ForeignKey("Course")]
        public int Course_id { get; set; }
        public virtual Course Course { get; set; }


        [ForeignKey("Trainee")]
        public int Trainee_id { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}

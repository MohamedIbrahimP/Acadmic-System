using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Annotations
{
    public class UniqueCourseAttribute:ValidationAttribute
    {
        dbLogic db;
        public UniqueCourseAttribute(dbLogic db)
        {
            this.db = db;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? Name = value?.ToString(); //type script 
            Course crsFromReq = (Course)validationContext.ObjectInstance; // type script   cs   90  id=1

            Course crsFromDb = db.Courses.SingleOrDefault(x => x.Name == Name && x.dept_id == crsFromReq.dept_id && x.Id != crsFromReq.Id);
            if (crsFromDb == null)
            {
                return ValidationResult.Success;

            }

            return new ValidationResult("Course already Found in the department!");
        }
    }
}

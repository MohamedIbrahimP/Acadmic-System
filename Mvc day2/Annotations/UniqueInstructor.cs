using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Annotations
{
    public class UniqueInstructorAttribute:ValidationAttribute
    {
        dbLogic db;
        public UniqueInstructorAttribute(dbLogic db)
        {
            this.db = db;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            string instructorName =(string) value;
            Instructor instructorFromReq = (Instructor)validationContext.ObjectInstance;
            var Exist =db.Instructors.Any(x=>x.Name==instructorName && x.Id!= instructorFromReq.Id);
            if (Exist == false)
            {
                return ValidationResult.Success;

            }

            return new ValidationResult("Instructor already Found!");


        }
    }
}

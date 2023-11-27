using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Annotations
{
    public class DepartmentUniqueAttribute:ValidationAttribute
    {
        private readonly dbLogic db;
        public DepartmentUniqueAttribute(dbLogic db)
        {
            this.db = db;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? Name = value?.ToString();
            var departmentFromReq = (Department)validationContext.ObjectInstance;

            var departmentFromDb = db.departments.FirstOrDefault(x => x.Name == Name&&x.Id!=departmentFromReq.Id);
            if (departmentFromDb == null)
            {
                return ValidationResult.Success;

            }

            return new ValidationResult("Department already Found in DB!");
        }
    }
}

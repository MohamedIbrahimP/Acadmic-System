using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Repository.DepartmentRepository
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        
        void Update (Department department);
        public ValidationResult checkUnique(Department department);
        
    }
}
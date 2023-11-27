using Mvc_day2.Annotations;
using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Repository.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        dbLogic db;
        public DepartmentRepository(dbLogic db)
        {
           this.db = db;
        }

        public IEnumerable<Department> GetAll()
        {
            return db.departments.ToList();
        }
        public Department Get(int id)
        {
            return db.departments.FirstOrDefault(d => d.Id == id);
        }
        public void Delete (int id)
        {
            var department = Get(id);
            db.departments.Remove(department);
        }
        public void Create(Department department)
        {
            
            db.departments.Add(department);
        }
        public void Update (Department department)
        {
            db.departments.Update(department);
        }
        public ValidationResult checkUnique(Department department)
        {
            var attribute = new DepartmentUniqueAttribute(db);
            var obj = new ValidationContext(department);
            var result =attribute.GetValidationResult(department.Name, obj);
            return result == ValidationResult.Success ? ValidationResult.Success : result;
        }
        public void Save()
        {
            db.SaveChanges();
        }

    }
}

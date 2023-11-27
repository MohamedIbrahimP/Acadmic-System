using Microsoft.EntityFrameworkCore;
using Mvc_day2.Annotations;
using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Repository.CourseRepository
{
    public class CourseRepository : ICourseRepository
    {
        dbLogic db;
        public CourseRepository(dbLogic db) //constractor
        {
            this.db = db;
        }


        public IEnumerable<Course> GetAll()
        {
            return db.Courses.Include(D=>D.Department).ToList();
        }
        public Course Get(int id)
        {
            return db.Courses.Include(D => D.Department).FirstOrDefault(C => C.Id == id);
        }
        public void Create(Course course)
        {
            db.Courses.Add(course);
        }
        public IEnumerable<Course> GetByDept(int deptId)
        {
            return db.Courses.Where(C => C.dept_id == deptId).ToList();

        }
        public void Delete(int id)
        {
            var courseToRemove = Get(id);
            if (courseToRemove != null)
            {
                db.Courses.Remove(courseToRemove);
            }
        }
        public void Update(Course course)
        {
            db.Courses.Update(course);
        }
        public ValidationResult checkUnique(Course course)
        {
            var attribute = new UniqueCourseAttribute(db);
            var obj = new ValidationContext(course);
            var Result = attribute.GetValidationResult(course.Name, obj);
            return Result == ValidationResult.Success ? ValidationResult.Success : Result;
        }
        public void Save()
        {
            db.SaveChanges();
        }

    }
}

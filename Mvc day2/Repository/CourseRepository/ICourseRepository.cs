using Mvc_day2.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Repository.CourseRepository
{
    public interface ICourseRepository :IRepository<Course>
    {
        void Update(Course course);
        public IEnumerable<Course> GetByDept(int deptId);
        ValidationResult checkUnique(Course course);
    }
}
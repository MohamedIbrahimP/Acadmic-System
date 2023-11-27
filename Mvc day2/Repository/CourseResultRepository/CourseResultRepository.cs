using Microsoft.EntityFrameworkCore;
using Mvc_day2.Models;

namespace Mvc_day2.Repository.CourseResultRepository
{
    public class CourseResultRepository : ICourseResultRepository
    {
        dbLogic db;
        public CourseResultRepository(dbLogic db)
        {
            this.db = db;
        }

        public IEnumerable<CourseResult> GetAll()
        {
            return db.CoursesResult.Include(T => T.Trainee).Include(C => C.Course).ToList();

        }
        public CourseResult Get(int id)
        {
            return db.CoursesResult.FirstOrDefault(x => x.Course_id == id);

        }

        public List<int> SelectedCourses(int id)
        {
            return db.CoursesResult.Where(x => x.Trainee_id == id).Select(x => x.Course_id).ToList();
        }
        public void DeleteCourse(int id)
        {
            var course = db.CoursesResult.FirstOrDefault(x => x.Course_id == id);
            db.CoursesResult.Remove(course);
        }
        public void Create(CourseResult courseResult)
        {
            db.CoursesResult.Add(courseResult);
        }
        public List<string> SelectedCoursesName(int id)
        {
            return db.CoursesResult.Where(x => x.Trainee_id == id).Select(x => x.Course.Name).ToList();
        }
        
    public void save()
        {
            db.SaveChanges();
        }








    }
}

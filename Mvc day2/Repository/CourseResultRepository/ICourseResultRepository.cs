using Mvc_day2.Models;

namespace Mvc_day2.Repository.CourseResultRepository
{
    public interface ICourseResultRepository
    {
        CourseResult Get(int id);
        IEnumerable<CourseResult> GetAll();
        public List<int> SelectedCourses(int id);
        public void DeleteCourse(int id);

        public void Create(CourseResult courseResult);
        public List<string> SelectedCoursesName(int id);
        public void save();
    }
}
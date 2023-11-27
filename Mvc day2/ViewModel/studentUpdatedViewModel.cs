using Mvc_day2.Models;

namespace Mvc_day2.ViewModel
{
    public class studentUpdatedViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public IEnumerable<Course> AllCourses { get; set; }
        public List<CourseViewModel> Results { get; set; }
        public List<int> SelectedCourses { get; set; }
        public List<string> SelectedCoursesName { get; set; }
    }
}

namespace Mvc_day2.ViewModel
{
    public class StudentWithAllCoursesViewModel
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public List<string> CoursesName=new List<string>();
        public List<double> CoursesDegree =new List<double>();
        public List<string> Color = new List<string>();
    }
}

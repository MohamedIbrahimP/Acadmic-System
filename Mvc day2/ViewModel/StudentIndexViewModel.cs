namespace Mvc_day2.ViewModel
{
    public class StudentIndexViewModel
    {
        public int id { get; set; }
        public string studentName { get; set; }
        public List<StudentCourseIndexViewModel> courses { get; set; }

    }
}

using Mvc_day2.Models;

namespace Mvc_day2.Repository.StudentRepository
{
    public interface IStudentRepository :IRepository<Trainee>
    {
        void Save();
        void Update(Trainee trainee);
        bool CheckCourseExists(int id, int sId);
    }
}
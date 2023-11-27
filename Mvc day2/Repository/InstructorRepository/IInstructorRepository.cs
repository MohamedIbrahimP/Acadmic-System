using Mvc_day2.Models;

namespace Mvc_day2.Repository.InstructorRepository
{
    public interface IInstructorRepository:IRepository<Instructor>
    {
        void Update(Instructor updatedInstructor);
        public void Save();
    }
}
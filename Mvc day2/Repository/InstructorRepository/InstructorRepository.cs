using Microsoft.EntityFrameworkCore;
using Mvc_day2.Models;

namespace Mvc_day2.Repository.InstructorRepository
{
    public class InstructorRepository : IInstructorRepository
    {
        dbLogic db;
        public InstructorRepository(dbLogic db)
        {
            this.db = db;
        }

        public IEnumerable<Instructor> GetAll()
        {
            return db.Instructors.Include(d => d.Department).Include(c => c.Course).ToList();
        }
        public Instructor Get(int id)
        {
            return db.Instructors.Include(d => d.Department).Include(c => c.Course).FirstOrDefault(i => i.Id == id);
        }
        public void Create(Instructor newInstructor)
        {
            db.Instructors.Add(newInstructor);
        }
        public void Update(Instructor updatedInstructor)
        {
            db.Instructors.Update(updatedInstructor);
        }
        public void Delete(int id)
        {
            var instructor =Get(id);
            db.Instructors.Remove(instructor);
        }
        public void Save()
        {
            db.SaveChanges();
        }



    }
}

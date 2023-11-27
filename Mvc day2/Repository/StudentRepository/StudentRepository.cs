using Microsoft.EntityFrameworkCore;
using Mvc_day2.Models;

namespace Mvc_day2.Repository.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        dbLogic db;
        public StudentRepository(dbLogic db)
        {
            this.db = db;
        }

        public IEnumerable<Trainee> GetAll()
        {
            return db.Trainees.ToList();
            
        }

        public Trainee Get(int id)
        {
            return db.Trainees.FirstOrDefault(T => T.Id == id);
        }
        public void Create(Trainee trainee)
        {
            db.Trainees.Add(trainee);
        }
        public void Update(Trainee trainee)
        {
            db.Trainees.Update(trainee);
        }
        public void Delete(int id)
        {
            db.Trainees.Remove(Get(id));
        }
        public bool CheckCourseExists(int id,int sId)
        {
            return db.CoursesResult.Any(x => x.Course_id==id && x.Trainee_id==sId);
        }
        public void Save()
        {
            db.SaveChanges();
        }


    }
}

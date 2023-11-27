using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mvc_day2.Models
{
    public class dbLogic: IdentityDbContext<ApplicationIdentityUser>
    {
       // public dbLogic() { }
        public dbLogic(DbContextOptions<dbLogic> options) : base(options)
        {
        }

        public DbSet<Department> departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }    
        public DbSet<CourseResult> CoursesResult { get; set;}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Mvc_DB;Integrated Security=True;TrustServerCertificate=True");
        //}

    }
}

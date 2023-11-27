using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc_day2.Common;
using Mvc_day2.Models;
using Mvc_day2.Repository.CourseRepository;
using Mvc_day2.Repository.CourseResultRepository;
using Mvc_day2.Repository.DepartmentRepository;
using Mvc_day2.Repository.StudentRepository;
using Mvc_day2.ViewModel;


namespace Mvc_day2.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        dbLogic db;
        private readonly RoleManager<IdentityRole> roleManager;
        IStudentRepository studentRepository;
        ICourseRepository courseRepository;
        IDepartmentRepository departmentRepository;
        ICourseResultRepository courseResultRepository;
        public StudentController(RoleManager<IdentityRole> roleManager,IStudentRepository studentRepository, ICourseRepository courseRepository, IDepartmentRepository departmentRepository, ICourseResultRepository courseResultRepository, dbLogic db)
        {
            this.roleManager = roleManager;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.departmentRepository = departmentRepository;
            this.courseResultRepository = courseResultRepository;
            this.db = db;
        }

        [Authorize(Permissions.Student.View)]
        public  IActionResult Index()
        {
            var Students = studentRepository.GetAll();
            List<StudentIndexViewModel> studentsList = new List<StudentIndexViewModel>();
            foreach (var item in Students)
            {
                var student = new StudentIndexViewModel()
                {
                    id=item.Id,
                    studentName=item.Name,
                    courses = db.CoursesResult.Where(x => x.Trainee_id == item.Id).Select(x => new StudentCourseIndexViewModel
                    {
                        courseName=x.Course.Name,
                        degree = x.degree,
                        minDegree = (int)x.Course.MinDegree,
                        color = (x.degree < x.Course.MinDegree ? "red" : "green")
                    })
                      .ToList(),
                };
                studentsList.Add(student);
            }
            

            return View(studentsList);
        }
        [Authorize(Permissions.Student.Create)]
        public IActionResult Create()
        {
            ViewData["deptList"] = departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trainee newStudent)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    studentRepository.Create(newStudent);
                    studentRepository.Save();
                    return RedirectToAction("Update",newStudent);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("dept_id", "Please Select Department!");
                }
              
            }
            ViewData["deptList"] = departmentRepository.GetAll();
            return View(newStudent);
        }
        [Authorize(Permissions.Student.Delete)]
        public IActionResult Delete(int id)
        {
            studentRepository.Delete(id);
            studentRepository.Save();
            return RedirectToAction("Index");
        }
        [Authorize(Permissions.Student.Edit)]
        public IActionResult Update(int id)
        {
            var student = studentRepository.Get(id);
            if (student != null)
            {
                var studentToUpdate = new studentUpdatedViewModel()
                {
                    id = student.Id,
                    name = student.Name,
                    AllCourses = courseRepository.GetByDept(student.dept_id),
                    SelectedCourses = courseResultRepository.SelectedCourses(student.Id),
                    Results = db.CoursesResult.Where(x => x.Trainee_id == id).Select(x => new CourseViewModel
                    {
                        id = x.Id,
                        courseId = x.Course_id,
                        degree = x.degree,
                        courseName = x.Course.Name
                    })
                      .ToList(),
                   SelectedCoursesName = courseResultRepository.SelectedCoursesName(id),
                };
                return View(studentToUpdate);
            }
            return View("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(studentUpdatedViewModel updatedStudent)
        {
            if (updatedStudent.SelectedCourses != null)
            {
                foreach (var item in updatedStudent.SelectedCourses)
                {
                    if (!studentRepository.CheckCourseExists(item, updatedStudent.id))
                    {
                        var courseResult = new CourseResult()
                        {
                            Course_id = item,
                            Trainee_id = updatedStudent.id,
                        };
                        courseResultRepository.Create(courseResult);
                        courseResultRepository.save();
                    }
                    else
                        continue;
                }
            }
            if (updatedStudent.Results != null)
            {
                foreach (var item in updatedStudent.Results)
                {
                    var course = courseResultRepository.Get(item.courseId);
                    course.degree = item.degree;

                }
            }
            
            courseResultRepository.save();
            return RedirectToAction("Index");


        }
        [Authorize(Permissions.Student.Delete)]
        public IActionResult DeleteCourse(int id)
        {
            courseResultRepository.DeleteCourse(id);
            courseResultRepository.save();
            return Json(new { success = true });
        }
        
    }
    }

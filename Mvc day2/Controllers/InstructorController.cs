using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_day2.Common;
using Mvc_day2.Models;
using Mvc_day2.Repository.CourseRepository;
using Mvc_day2.Repository.DepartmentRepository;
using Mvc_day2.Repository.InstructorRepository;
using Mvc_day2.ViewModel;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Mvc_day2.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        ICourseRepository courseRepository;
        IInstructorRepository instructorRepository;
        IDepartmentRepository departmentRepository;
        
        public InstructorController(ICourseRepository courseRepository, IInstructorRepository instructorRepository, IDepartmentRepository departmentRepository)
        {
            this.courseRepository = courseRepository;
            this.instructorRepository = instructorRepository;
            this.departmentRepository= departmentRepository;
        }
        [Authorize(Permissions.Instructor.View)]
        public IActionResult Index()
        {
            var instructors = instructorRepository.GetAll();
            return View(instructors);
        }
        [Authorize(Permissions.Instructor.Create)]
        public IActionResult Create()
        {
            ViewData["deptList"] = departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor newInstructor, IFormFile profileImageFile)
        {
            if (ModelState.IsValid == true)
            {
                if (profileImageFile != null && profileImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        profileImageFile.CopyTo(memoryStream);
                        newInstructor.profileImage = memoryStream.ToArray();
                    }
                }
                try
                {
                    instructorRepository.Create(newInstructor);
                    instructorRepository.Save();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("dept_id", "Please Select Department!");
                    ModelState.AddModelError("crs_id", "Please Select Course!");
                    ModelState.AddModelError("", "check");
                }
                
            }
            ViewData["deptList"] = departmentRepository.GetAll();
            return View("Create", newInstructor);

        }
        [Authorize(Permissions.Instructor.Edit)]
        public IActionResult Update(int id)
        {
            var instructor = instructorRepository.Get(id);
            ViewData["departmentName"] = departmentRepository.Get(instructor.dept_id).Name;
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Instructor updatedInstructor , IFormFile profileImageFile)
        {
            if (ModelState.IsValid ==true)
            {
                if (profileImageFile != null && profileImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        profileImageFile.CopyTo(memoryStream);
                        updatedInstructor.profileImage = memoryStream.ToArray();
                    }
                }
                instructorRepository.Update(updatedInstructor);
                instructorRepository.Save();
                return RedirectToAction("index");
            }
            ViewData["departmentName"]= departmentRepository.Get(updatedInstructor.dept_id).Name;
            return View("Update", updatedInstructor);
        }
        [Authorize(Permissions.Instructor.Delete)]
        public IActionResult Delete(int id)
        {
                instructorRepository.Delete(id);
                instructorRepository.Save();
                return RedirectToAction("Index");
        }

        public IActionResult ShowCoursesInDepartment(int deptId)
        {
            IEnumerable<Course> CourseList = courseRepository.GetByDept(deptId);
            return Json(CourseList);
        }

    }
}

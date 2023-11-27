using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_day2.Annotations;
using Mvc_day2.Filters;
using Mvc_day2.Models;
using Mvc_day2.Repository.CourseRepository;
using Mvc_day2.Repository.DepartmentRepository;
using System.ComponentModel.DataAnnotations;
using Mvc_day2.Common;







namespace Mvc_day2.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        ICourseRepository courseRepository;
        IDepartmentRepository departmentRepository;
        public CourseController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            this.courseRepository = courseRepository;
            this.departmentRepository = departmentRepository;
        }

        public IActionResult CheckMinDegree(int Degree, int MinDegree)
        {
            if (MinDegree <= Degree)
            {
                return Json(true);
            }
            return Json(false);
        }
       
        [Authorize(Permissions.Course.View)]
        public IActionResult Index()
        {
            ViewData["SS"] = HttpContext.Session.GetInt32("Pass");
            return View(courseRepository.GetAll());
        }
        [Authorize(Permissions.Course.Create)]
        public IActionResult New_crs()
        {

            ViewData["Depts"] = departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New_crs(Course crs)
        {
            if (ModelState.IsValid == true)
            {

                try
                {
                    var validated= courseRepository.checkUnique(crs);
                    if(validated == ValidationResult.Success)
                    {
                        courseRepository.Create(crs);
                        courseRepository.Save();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("Name", validated.ErrorMessage);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("dept_id", "Please Select Department!");
                }
                
            }
            ViewData["Depts"] = departmentRepository.GetAll();
            return View("New_crs", crs);
        }
        [Authorize(Permissions.Course.Delete)]
        
        public IActionResult Delete(int id)
        {
            courseRepository.Delete(id);
            courseRepository.Save();
            return RedirectToAction("Index");
        }
        [Authorize(Permissions.Course.Edit)]
        public IActionResult Update(int id)
        {
            var course =courseRepository.Get(id);
            ViewData["Depts"] = departmentRepository.GetAll();
            return View(course);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Course course)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    var validated = courseRepository.checkUnique(course);
                                     
                    if (validated == ValidationResult.Success)
                    {
                        courseRepository.Update(course);
                        courseRepository.Save();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("Name", validated.ErrorMessage);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("dept_id", "Please Select Department!");
                }
               
            }
            ViewData["Depts"] = departmentRepository.GetAll();
            return View("Update", course);

        }
    }
}

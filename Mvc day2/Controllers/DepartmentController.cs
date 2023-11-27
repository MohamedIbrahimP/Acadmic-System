using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc_day2.Annotations;
using Mvc_day2.Common;
using Mvc_day2.Models;
using Mvc_day2.Repository.DepartmentRepository;
using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        [Authorize(Permissions.Department.View)]

        public IActionResult Index()
        {
           var departments= departmentRepository.GetAll();
            return View(departments);
        }
        [Authorize(Permissions.Department.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Department newDepartment)
        {
            if (ModelState.IsValid == true)
            {
                var unique = departmentRepository.checkUnique(newDepartment);
               if (unique == ValidationResult.Success)
                {
                departmentRepository.Create(newDepartment);
                departmentRepository.Save();
                return RedirectToAction("Index");
                }
                ModelState.AddModelError("Name", unique.ErrorMessage);
            }
            return View("Create",newDepartment);
        }
        [Authorize(Permissions.Department.Edit)]
        public IActionResult Update(int id)
        {
            var department = departmentRepository.Get(id);
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Department updatedDepartment)
        {
            if (ModelState.IsValid == true)
            {
                var unique = departmentRepository.checkUnique(updatedDepartment);
               
                if (unique == ValidationResult.Success)
                {
                    departmentRepository.Update(updatedDepartment);
                    departmentRepository.Save();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Name", unique.ErrorMessage);
            }
            return View("Update", updatedDepartment);
        }
        [Authorize(Permissions.Department.Delete)]
        public IActionResult Delete(int id)
        {
            departmentRepository.Delete(id);
            departmentRepository.Save();
            return RedirectToAction("Index");
        }

    }
}

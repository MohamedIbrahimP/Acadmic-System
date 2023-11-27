using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_day2.Common;
using Mvc_day2.Models;
using Mvc_day2.Repository.IdentityRepository;
using Mvc_day2.ViewModel;
using System.Data;
using System.Security.Claims;

namespace Mvc_day2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class IdentityController : Controller
    {
       
        private readonly IIdentityRepository identityRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationIdentityUser> userManager;

        public IdentityController(IIdentityRepository identityRepository, RoleManager<IdentityRole> roleManager ,UserManager<ApplicationIdentityUser>userManager)
        {
            this.identityRepository = identityRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult > Role()
        {
            var roles = await roleManager.Roles.ToListAsync();
            ViewData["roles"]= await roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Role(RoleViewModel roleVm)
        {
            if (ModelState.IsValid)
            {
                var roleModel = new IdentityRole();
                roleModel.Name = roleVm.name;
                await roleManager.CreateAsync(roleModel);
                return RedirectToAction("Index", "Home");
            }
            ViewData["roles"] = await roleManager.Roles.ToListAsync();
            return View(roleVm);
        }

        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
                return NotFound();

            var roleClaims = roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();

            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(c => c == permission.DisplayValue))
                    permission.IsSelected = true;
            }

            var viewModel = new PermissionsFormViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                RoleCalims = allPermissions
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePermissions(PermissionsFormViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
                return NotFound();

            var roleClaims = await roleManager.GetClaimsAsync(role);

            foreach (var claim in roleClaims)
                await roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();

            foreach (var claim in selectedClaims)
                await roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));

            return RedirectToAction("Role");
        }
     
        /// //////////////////////////////////////////
       
        public IActionResult SetRole()
        {
            ViewData["users"] = identityRepository.GetAllUsers();
            ViewData["allroles"] = identityRepository.GetAllRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> SetRole(UserRoleViewModel user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (user.roles != null)
                    {
                        var userModel = identityRepository.GetUser(user.id);
                        foreach (var item in user.roles)
                        {
                            var roleid = identityRepository.GetRole(item).Id;
                            bool exist = identityRepository.CkechHaveRole(user.id, roleid);
                            if (!exist)
                            {
                                await userManager.AddToRoleAsync(userModel, item);
                            }
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("name", "Please Select A User!");
                }
            }
            ViewData["users"] = identityRepository.GetAllUsers();
            ViewData["allroles"] = identityRepository.GetAllRoles();
            return View(user);
        }

        //public async Task<IActionResult> SetClaim()
        //{
        //    var role = identityRepository.GetRole("Admin");
        //    //var claim = new Claim("Student", "Student1");


        //    //await roleManager.AddClaimAsync(role, claim);

        //    Claim IDClaim =
        //        User.Claims
        //        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    //{type:nameIdentifier,value=111111 }
        //    string id = IDClaim.Value;
        //    //User.Claims.FirstOrDefault(c => c.Type == "MyClaim");

        //    //return Content($"Welcome {name}");




        //    var i=  await roleManager.GetClaimsAsync(role);
        //    var x = i.FirstOrDefault().Type;

        //    return Content($"{x}");

        //}
        

        


    }
}

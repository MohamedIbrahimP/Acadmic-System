using Microsoft.AspNetCore.Identity;
using Mvc_day2.Models;
using Mvc_day2.ViewModel;

namespace Mvc_day2.Repository.IdentityRepository
{
    public interface IIdentityRepository
    {
        IEnumerable<RoleViewModel> GetAllRoles();
        IEnumerable<UserRoleViewModel> GetAllUsers();
        ApplicationIdentityUser GetUser(String id);
        IdentityRole GetRole(string name);
        bool CkechHaveRole(string userId, string roleId);
        void Save();
    }
}
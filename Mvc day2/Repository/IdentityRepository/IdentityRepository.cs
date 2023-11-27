using Mvc_day2.Models;
using Mvc_day2.ViewModel;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mvc_day2.Repository.IdentityRepository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly dbLogic db;

        public IdentityRepository(dbLogic db)
        {
            this.db = db;
        }

        public IEnumerable<RoleViewModel> GetAllRoles()
        {
            var rolesFromDb = db.Roles.ToList();
            var roles = new List<RoleViewModel>();
            if (rolesFromDb != null)
            {
                foreach (var item in rolesFromDb)
                {
                    var role = new RoleViewModel()
                    {
                        id = item.Id,
                        name = item.Name

                    };
                    roles.Add(role);
                }

            }
            return roles;
        }
        public IEnumerable<UserRoleViewModel> GetAllUsers()
        {
            var usersFromDb = db.Users.ToList();
            var users = new List<UserRoleViewModel>();
            if (usersFromDb != null)
            {
                foreach (var item in usersFromDb)
                {
                    var user = new UserRoleViewModel()
                    {
                        id = item.Id,
                        name = item.UserName,
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public ApplicationIdentityUser GetUser(String id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public IdentityRole GetRole(string name)
        {
            return db.Roles.FirstOrDefault(x => x.Name == name);
        }

        public bool CkechHaveRole(string userId, string roleId)
        {
           return db.UserRoles.Any(x => x.UserId == userId && x.RoleId == roleId);
        }
        public void Save()
        {
            db.SaveChanges();
        }







    }
}

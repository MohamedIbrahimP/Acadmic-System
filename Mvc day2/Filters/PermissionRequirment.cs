using Microsoft.AspNetCore.Authorization;

namespace Mvc_day2.Filters
{
    public class PermissionRequirment : IAuthorizationRequirement
    {
        public string Permission { get; private set; }
        public PermissionRequirment(string permission)
        {
            Permission = permission;
        }
    }
}
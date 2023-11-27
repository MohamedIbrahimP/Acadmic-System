using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Mvc_day2.Filters
{

    //public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    //{
    //    private readonly RoleManager<IdentityRole> roleManager;
    //    private readonly string _permission;

    //    public PermissionAttribute(string permission)
    //    {
    //        roleManager = roleManager;
    //        _permission = permission;
    //    }

    //    //public void OnAuthorization(AuthorizationFilterContext context )
    //    //{

    //    //        //// Check if the role has the specific claim
    //    //        //var hasClaim = await roleManager.GetClaimsAsync())
    //    //        //   .Any(c => c.Type == "GetStudent" && c.Value == "GetStudent1");
    //    //    }

    //    //void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
    //    //    {
    //    //        throw new NotImplementedException();
    //    //}

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        // Check if the user has the required permission
    //        if (!context.HttpContext.User.HasClaim(c => c.Type == "Permission" && c.Value == _permission))
    //        {
    //            context.Result = new ForbidResult();
    //            return;
    //        }
    //    }

    //}
}


using Microsoft.AspNetCore.Authorization;

namespace Mvc_day2.Filters
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirment>
    {

        public PermissionAuthorizationHandler()
        {
            
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
        {
            if (context.User == null)
                return;

            var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");

            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }



        }





    }
}

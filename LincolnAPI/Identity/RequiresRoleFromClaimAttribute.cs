using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace LincolnAPI.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresRoleFromClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _roleName;

        public RequiresRoleFromClaimAttribute(string roleName)
        {
            _roleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.HasClaim(ClaimTypes.Role, _roleName))
            {
                context.Result = new ForbidResult();
            }
            
        }

    }
}

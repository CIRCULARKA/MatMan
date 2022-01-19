using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;

namespace MatMan.UI.Controllers.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly SignInManager<IdentityUser> _signManager;

        private readonly UserManager<IdentityUser> _usersManager;

        public AuthorizationFilter(
            SignInManager<IdentityUser> signManager,
            UserManager<IdentityUser> usersManager
        )
        {
            _signManager = signManager;
            _usersManager = usersManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
           
        }
    }
}

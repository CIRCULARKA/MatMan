using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MatMan.UI.Controllers
{
    public class AccountController : ApplicationController
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly UserManager<IdentityUser> _usersManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> usersManager
        )
        {
            _signInManager = signInManager;
            _usersManager = usersManager;
        }

        public IActionResult GetAuthorizationView() =>
            View("AuthorizationView");
    }
}

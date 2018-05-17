using Library.BLL.Infrastructure;
using Library.BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Library.ViewModels.IdentityViewModels;
using Library.BLL.Services;
using System.Collections.Generic;

namespace Library.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = new UserViewModel { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await _userService.Authenticate(userViewModel);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                if (claim != null)
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                }
            }
            return RedirectToAction("Index", "Book");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Book");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Email = model.Email,
                    Password = model.Password
                };
                OperationDetails operationDetails = await _userService.Create(userViewModel);
                if (operationDetails.Succedeed)
                {
                    return View("SuccessRegister");
                }
                if (!operationDetails.Succedeed)
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserViewModel
            {
                Email = "admin@gmail.com",
                Password = "123456",
                Role = "admin",
            }, new List<string> { "admin" });
        }
    }
}
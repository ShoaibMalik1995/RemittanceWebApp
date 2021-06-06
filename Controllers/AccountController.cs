using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RemittanceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemittanceWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("email,password")] loginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await this._userManager.FindByEmailAsync(model.email);
                    if (user == null)
                    {
                        TempData.Add("AlertMessage", new AlertModel("User name and password is invalid", AlertModel.AlertType.Error));
                        return View(model);
                    }                        

                    var result = await this._userManager.CheckPasswordAsync(user, model.password);
                    if (result)
                    {
                        return RedirectToAction("Index", "CardRequest");
                    }
                    TempData.Add("AlertMessage", new AlertModel(result.ToString(), AlertModel.AlertType.Error));
                }
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel(ex.Message, AlertModel.AlertType.Error));
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("email,password, confirmpassword")] UserRegister model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var identityUser = new IdentityUser
                    {
                        Email = model.email,
                        UserName = model.email
                    };

                    var result = await _userManager.CreateAsync(identityUser, model.password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    TempData.Add("AlertMessage", new AlertModel(result.ToString(), AlertModel.AlertType.Error));
                }
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel(ex.Message, AlertModel.AlertType.Error));
            }
            return View(model);
        }

    }
}

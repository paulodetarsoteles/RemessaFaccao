using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models.ViewModels;

namespace RemessaFaccao.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            IdentityUser user = await _userManager.FindByNameAsync(login.Username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(login.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Falha ao realizar login!");
            return View(login);
        }
    }
}

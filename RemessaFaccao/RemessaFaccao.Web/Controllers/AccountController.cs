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

        #region Login

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
                    DateTime dateTime = DateTime.Now;
                    Console.WriteLine("Usuário {0} registrado com sucesso. {1}", user.UserName, dateTime.ToString());

                    if (string.IsNullOrEmpty(login.ReturnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(login.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Falha ao realizar login!");
            return View(login);
        }

        #endregion

        #region Registro

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel userRegister)
        {
            if (userRegister.Password == userRegister.Confirm)
            {
                if (ModelState.IsValid)
                {
                    DateTime dateTime = DateTime.Now;
                    IdentityUser user = new();
                    user.UserName = userRegister.Username;
                    user.Email = userRegister.Email;
                    IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

                    if (result.Succeeded)
                    {
                        Console.WriteLine("Usuário {0} registrado com sucesso. {1}", userRegister.Username, dateTime.ToString());
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao registrar usuário {0}. {1}", userRegister.Username, dateTime.ToString());
                        this.ModelState.AddModelError("Registro", "Erro ao registrar usuário.");
                        return View(userRegister);
                    }
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Usuário inválido.");
                    return View(userRegister);
                }
            }
            else
            {
                this.ModelState.AddModelError("Registro", "Senha e confirmação não estão iguais.");
                return View(userRegister);
            }
        }

        #endregion

        #region Logout 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}

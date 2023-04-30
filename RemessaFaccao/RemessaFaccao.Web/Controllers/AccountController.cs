using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models.ViewModels;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            try
            {
                IdentityUser user = await _userManager.FindByNameAsync(login.Username);
                if (user is not null)
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
            catch
            {
                ModelState.AddModelError("", "Falha ao realizar comunicação com o banco de dados!");
                return View(login);
            }
        }

        #endregion

        #region Register

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
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
                        return RedirectToAction("Index", "Home");
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Index_Details_Edit_Delete

        // GET: AccountController
        [HttpGet]
        public IActionResult Index()
        {
            List<IdentityUser> result = _userManager.Users.ToList();
            return View(result);
        }

        // GET: AccountController/Details/5
        [HttpGet]
        public ActionResult Details(string userId)
        {
            IdentityUser result = _userManager.FindByIdAsync(userId).Result;

            if (result == null)
            {
                ModelState.AddModelError("", "Falha ao localizar!");
                return View();
            }
            return View(result);
        }

        // GET: AccountController/Edit/
        [HttpGet]
        public ActionResult Edit(string id)
        {
            IdentityUser result = _userManager.FindByIdAsync(id).Result;

            if (result == null)
            {
                ModelState.AddModelError("", "Falha ao localizar usuário!");
                return View();
            }
            return View(result);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, LoginViewModel user)
        {
            if (ModelState.IsValid && !ModelState.IsNullOrEmpty())
            {
                DateTime dateTime = DateTime.Now;

                try
                {
                    IdentityUser result = _userManager.FindByIdAsync(id).Result;

                    if (result == null)
                    {
                        ModelState.AddModelError("", "Falha ao localizar usuário!");
                        return View();
                    }
                    else
                    {
                        result.UserName = user.Username;
                        result.Email = user.Email;
                        result.PasswordHash = user.Password;

                        Task<IdentityResult> identityResult = _userManager.UpdateAsync(result);
                        if (identityResult != null)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            Console.WriteLine("Falha ao tentar atualizar user {0}. {1}", user.Username, dateTime);
                            ModelState.AddModelError("", "Falha ao tentar atualizar usuário!");
                            return View(user);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro ao atualizar user {0}. {1}", user.Username, dateTime);
                    ModelState.AddModelError("", "Erro ao atualizar usuário!");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Falha ao carregar usuário!");
                return View();
            }
        }

        // GET: AccountController/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            //TODO
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, LoginViewModel user)
        {
            //TODO
            return View();
        }

        #endregion
    }
}

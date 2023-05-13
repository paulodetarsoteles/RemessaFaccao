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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Login

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            try
            {
                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Admin";
                    role.NormalizedName = "ADMIN";
                    IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
                }

                if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
                {
                    IdentityUser user = new();
                    user.UserName = "admin";
                    user.Email = "admin@localhost";
                    user.NormalizedUserName = "ADMIN";
                    user.NormalizedEmail = "ADMIN@LOCALHOST";
                    user.EmailConfirmed = true;
                    user.LockoutEnabled = false;
                    user.SecurityStamp = Guid.NewGuid().ToString();

                    IdentityResult result = _userManager.CreateAsync(user, "admin123").Result;

                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }

                return View(new LoginViewModel() { ReturnUrl = returnUrl });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", "Falha ao realizar comunicação com o banco de dados!");
                return View();
            }
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        #region Index_Details_Edit

        // GET: AccountController
        [HttpGet]
        public IActionResult Index()
        {
            List<IdentityUser> identities = _userManager.Users.ToList();
            List<LoginViewModel> logins = new();

            foreach (IdentityUser identity in identities)
            {
                LoginViewModel login = new();

                login.Username = identity.UserName;
                logins.Add(login);
            }

            return View(logins);
        }

        // GET: AccountController/Details/admin
        [HttpGet]
        public ActionResult Details(string userName)
        {
            IdentityUser identity = _userManager.FindByNameAsync(userName).Result;
            LoginViewModel login = new();

            login.Username = identity.UserName;
            login.Email = identity.Email;

            return View(login);
        }

        // GET: AccountController/Edit/admin
        [HttpGet]
        public ActionResult Edit(string userName)
        {
            IdentityUser identity = _userManager.FindByNameAsync(userName).Result;
            LoginViewModel login = new();

            login.Username = identity.UserName;
            login.Email = identity.Email;

            return View(login);
        }

        // POST: AccountController/Edit/admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string userName, LoginViewModel user)
        {
            if (!ModelState.IsValid && ModelState.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Falha ao carregar usuário!");
                return View(user);
            }

            if (user.Password != user.Confirm)
            {
                ModelState.AddModelError("", "Senha e conformação devem ser iguais. ");
                return View(user);
            }

            if (user.PasswordOld is null)
            {
                ModelState.AddModelError("", "Favor preencher senha atual. ");
                return View(user);
            }

            DateTime dateTime = DateTime.Now;

            try
            {
                IdentityUser result = _userManager.FindByNameAsync(userName).Result;

                result.UserName = user.Username;
                result.Email = user.Email;

                Task<IdentityResult> identityPass = _userManager.ChangePasswordAsync(result, user.PasswordOld, user.Password);

                if (identityPass is null || !identityPass.Result.Succeeded)
                {
                    ModelState.AddModelError("", "Senha atual inválida. ");
                    return View(user);
                }

                Task<IdentityResult> identityResult = _userManager.UpdateAsync(result);

                if (identityResult is null)
                {
                    Console.WriteLine("Falha ao tentar atualizar user {0}. {1}", user.Username, dateTime);
                    ModelState.AddModelError("", "Falha ao tentar atualizar usuário!");
                    return View(user);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao atualizar user {0}. {1}" + e.Message, user.Username, dateTime);
                ModelState.AddModelError("", "Erro ao atualizar usuário!");
                return View(user);
            }
        }

        #endregion
    }
}

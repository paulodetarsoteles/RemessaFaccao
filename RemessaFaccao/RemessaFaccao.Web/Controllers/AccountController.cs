using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models.ViewModels;
using RemessaFaccao.DAL.Setting;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
                    IdentityRole role = new()
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    };
                    IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
                }

                if (_userManager.FindByEmailAsync("admin@localhost").Result is null)
                {
                    IdentityUser user = new()
                    {
                        UserName = "admin",
                        Email = "admin@localhost",
                        NormalizedUserName = "ADMIN",
                        NormalizedEmail = "ADMIN@LOCALHOST",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    if (_userManager.CreateAsync(user, "admin123").Result.Succeeded)
                        _userManager.AddToRoleAsync(user, "Admin").Wait();
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
            DateTime dateTime = DateTime.Now;

            try
            {
                if (!ModelState.IsValid)
                    return View(login);

                IdentityUser user = await _userManager.FindByNameAsync(login.Username) ?? throw new Exception("Usuário não encontrado");
                SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (!result.Succeeded)
                    throw new Exception("Senha incorreta");

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("Login"), string.Format("Usuário {0} logado com sucesso. {1}", user.UserName, dateTime.ToString()));

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Erro ao efetuar login - " + e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("Login"), String.Format("Erro ao efetuar login - {0} {1}", e.Message, dateTime.ToString()));

                return View(login);
            }
        }

        #endregion

        #region Register

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Register(LoginViewModel userRegister)
        {
            DateTime dateTime = DateTime.Now;

            try
            {
                if (userRegister.Password != userRegister.Confirm)
                    throw new Exception("Senha e confirmação não estão iguais.");

                if (!ModelState.IsValid)
                    throw new Exception("Usuário inválido.");

                IdentityUser user = new()
                {
                    UserName = userRegister.Username,
                    Email = userRegister.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

                if (!result.Succeeded)
                    throw new Exception("Erro ao registrar usuário.");

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("Register"), string.Format("Usuário {0} registrado com sucesso. {1}", user.UserName, dateTime.ToString()));

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Registro", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("Register"), string.Format("Erro ao registrar usuário {0}. {1} {2}", e.Message, userRegister.Username, dateTime.ToString()));

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
            try
            {
                HttpContext.Session.Clear();
                await _signInManager.SignOutAsync();

                return RedirectToAction("Login", "Account");
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("Register"), string.Format("Erro ao efetuar logoff. {0} {1}", DateTime.Now, e.Message));

                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        #region Index_Details_Edit

        // GET: AccountController
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<IdentityUser> identities = _userManager.Users.ToList();
                List<LoginViewModel> logins = new();

                foreach (IdentityUser identity in identities)
                {
                    LoginViewModel login = new()
                    {
                        Username = identity.UserName
                    };
                    logins.Add(login);
                }

                return View(logins);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AccountIndex"), String.Format("Erro ao acessar Index - {0} {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // GET: AccountController/Details/admin
        [HttpGet]
        public IActionResult Details(string userName)
        {
            try
            {
                IdentityUser identity = _userManager.FindByNameAsync(userName).Result ?? throw new Exception("Usuário não encontrado. ");
                LoginViewModel login = new()
                {
                    Username = identity.UserName,
                    Email = identity.Email
                };

                return View(login);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AccountDetails"), String.Format("Erro ao acessar Index - {0} {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // GET: AccountController/Edit/admin
        [HttpGet]
        public IActionResult Edit(string userName)
        {
            try
            {
                IdentityUser identity = _userManager.FindByNameAsync(userName).Result ?? throw new Exception("Usuário não encontrado. ");

                LoginViewModel login = new()
                {
                    Username = identity.UserName,
                    Email = identity.Email
                };

                return View(login);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AccountEdit"), String.Format("Erro ao acessar Index - {0} {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // POST: AccountController/Edit/admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string userName, LoginViewModel user)
        {
            DateTime dateTime = DateTime.Now;

            try
            {
                if (!ModelState.IsValid && ModelState.IsNullOrEmpty())
                    throw new Exception("Falha ao carregar usuário!");

                if (user.Password != user.Confirm)
                    throw new Exception("Senha e conformação devem ser iguais. ");

                if (user.PasswordOld is null)
                    throw new Exception("Favor preencher senha atual. ");

                IdentityUser result = _userManager.FindByNameAsync(userName).Result ?? throw new Exception("Usuário não encontrado. ");

                result.UserName = user.Username;
                result.Email = user.Email;

                Task<IdentityResult> identityPass = _userManager.ChangePasswordAsync(result, user.PasswordOld, user.Password);

                if (identityPass is null || !identityPass.Result.Succeeded)
                    throw new Exception("Senha atual inválida. ");

                if (_userManager.UpdateAsync(result) is null)
                    throw new Exception("Falha ao tentar atualizar usuário!");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AccountEdit"), string.Format("Erro ao atualizar usuário {0}. {1}", user.Username, dateTime.ToString()));
                return View(user);
            }
        }

        #endregion
    }
}

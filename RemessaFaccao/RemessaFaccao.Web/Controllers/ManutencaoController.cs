using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RemessaFaccao.Web.Controllers
{
    [AllowAnonymous]
    public class ManutencaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

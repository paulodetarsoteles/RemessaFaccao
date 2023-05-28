using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RemessaFaccao.Web.Controllers
{
    [AllowAnonymous]
    public class ManutencaoController : Controller
    {
        public IActionResult Index(string? exc)
        {
            if (exc is not null)
                return View(exc); 
            
            return View();
        }
    }
}

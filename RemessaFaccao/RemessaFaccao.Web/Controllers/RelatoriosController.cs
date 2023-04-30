using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Manutencao"); 
        }
    }
}

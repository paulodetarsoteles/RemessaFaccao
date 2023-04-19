using Microsoft.AspNetCore.Mvc;

namespace RemessaFaccao.Web.Controllers
{
    public class RelatoriosController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Manutencao"); 
        }
    }
}

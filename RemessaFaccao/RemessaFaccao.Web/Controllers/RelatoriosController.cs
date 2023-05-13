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

        public IActionResult RelatorioEnviarParaProducao()
        {
            return RedirectToAction("Index", "Manutencao");
        }

        public IActionResult RelatorioEmProducao()
        {
            return RedirectToAction("Index", "Manutencao");
        }

        public IActionResult RelatorioAtrasadas()
        {
            return RedirectToAction("Index", "Manutencao");
        }

        public IActionResult RelatorioReceberHoje()
        {
            return RedirectToAction("Index", "Manutencao");
        }

        public IActionResult RelatorioPersonalizado()
        {
            return RedirectToAction("Index", "Manutencao");
        }
    }
}

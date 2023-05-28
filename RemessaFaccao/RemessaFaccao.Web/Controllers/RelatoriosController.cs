using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {
        private readonly IRemessaRepository _remessaRepository;

        public RelatoriosController(IRemessaRepository remessaRepository)
        {
            _remessaRepository = remessaRepository;
        }

        // GET: RelatorioEnviarParaProducao
        public IActionResult RelatorioEnviarParaProducao()
        {
            try
            {
                return View(_remessaRepository.GetNaoEnviadaParaProducao());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RelatorioEmProducao
        public IActionResult RelatorioEmProducao()
        {
            try
            {
                return View(_remessaRepository.GetEmProducao());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RelatorioAtrasadas
        public IActionResult RelatorioAtrasadas()
        {
            try
            {
                return View(_remessaRepository.GetAtrasadas());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RelatorioReceberHoje
        public IActionResult RelatorioReceberHoje()
        {
            try
            {
                return View(_remessaRepository.GetReceberHoje());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RelatorioPersonalizado
        public IActionResult RelatorioPersonalizado()
        {
            try
            {
                return RedirectToAction("Index", "Manutencao");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: RelatorioPersonalizado
        [HttpPost]
        public IActionResult RelatorioPersonalizado(object model)
        {
            try
            {
                return RedirectToAction("Index", "Manutencao");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
    }
}

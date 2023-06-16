using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RemessaFaccao.DAL.Models;
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

        // GET: RelatorioRecebidas
        public IActionResult RelatorioRecebidas()
        {
            ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoes(), "FaccaoId", "Nome");

            DateTime fromDate = DateTime.Now.AddDays(-7);
            DateTime toDate = DateTime.Now;

            return View(_remessaRepository.GetRecebidas(fromDate, toDate, null));
        }

        // POST: RelatorioRecebidas
        [HttpPost]
        public IActionResult RelatorioRecebidas(DateTime fromDate , DateTime toDate, int? faccaoId = null)
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoes(), "FaccaoId", "Nome");

                return View(_remessaRepository.GetRecebidas(fromDate, toDate, faccaoId));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(_remessaRepository.GetRecebidas(DateTime.Now.AddDays(-7), DateTime.Now, null));
            }
        }

        // GET: Recebidas
        public IActionResult Recebidas(List<Remessa> remessas)
        {
            return View(remessas);
        }

        public IActionResult RecebidaDetails(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("Index", "Manutencao", e.Message);
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

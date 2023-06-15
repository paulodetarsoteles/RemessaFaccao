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
            return View();
        }

        // POST: RelatorioRecebidas
        public IActionResult RelatorioRecebidas(DateTime fromDate, DateTime toDate, int? faccaoId = null)
        {
            ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoes(), "FaccaoId", "Nome");
            List<Remessa> result = new(_remessaRepository.GetRecebidas(fromDate, toDate, faccaoId)); 

            return RedirectToAction(nameof(Recebidas), result);
        }

        // GET: Recebidas
        public IActionResult Recebidas(List<Remessa> remessas)
        {
            return View(remessas); 
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

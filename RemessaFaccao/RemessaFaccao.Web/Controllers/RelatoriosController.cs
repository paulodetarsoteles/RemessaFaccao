using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;

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

        public IActionResult RelatorioEnviarParaProducao()
        {
            try
            {
                return View(_remessaRepository.GetNaoEnviadaParaProducao());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioEnviarParaProducao"), string.Format("Erro ao acessar RelatorioEnviarParaProducao. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        public IActionResult RelatorioEmProducao()
        {
            try
            {
                return View(_remessaRepository.GetEmProducao());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioEmProducao"), string.Format("Erro ao acessar RelatorioEmProducao. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        public IActionResult RelatorioAtrasadas()
        {
            try
            {
                return View(_remessaRepository.GetAtrasadas());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioAtrasadas"), string.Format("Erro ao acessar RelatorioAtrasadas. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        public IActionResult RelatorioReceberHoje()
        {
            try
            {
                return View(_remessaRepository.GetReceberHoje());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioReceberHoje"), string.Format("Erro ao acessar RelatorioReceberHoje. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        public IActionResult RelatorioRecebidas()
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoes(), "FaccaoId", "Nome");

                DateTime fromDate = DateTime.Now.AddDays(-7);
                DateTime toDate = DateTime.Now;

                return View(_remessaRepository.GetRecebidas(fromDate, toDate, null));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioRecebidas"), string.Format("Erro ao acessar RelatorioRecebidas. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioRecebidas"), string.Format("Erro ao acessar RelatorioRecebidas. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View(_remessaRepository.GetRecebidas(DateTime.Now.AddDays(-7), DateTime.Now, null));
            }
        }

        public IActionResult Recebidas(List<Remessa> remessas) => View(remessas); 

        public IActionResult RecebidaDetails(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RecebidaDetails"), string.Format("Erro ao acessar dealhes da remessa {0}. {1} - {2}", id, e.Message, DateTime.Now.ToString()));
                return RedirectToAction("Index", "Manutencao", e.Message);
            }
        }

        public IActionResult RelatorioPersonalizado()
        {
            try
            {
                return RedirectToAction("Index", "Manutencao");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioPersonalizado"), string.Format("Erro ao acessar RelatorioPersonalizado. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        [HttpPost]
        public IActionResult RelatorioPersonalizado(object model)
        {
            try
            {
                return RedirectToAction("Index", "Manutencao");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RelatorioPersonalizado"), string.Format("Erro ao acessar RelatorioPersonalizado. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }
    }
}

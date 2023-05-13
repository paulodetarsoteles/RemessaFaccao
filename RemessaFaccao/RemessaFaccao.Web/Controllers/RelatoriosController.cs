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
            return View(_remessaRepository.GetNaoEnviadaParaProducao());
        }

        // GET: RelatorioEmProducao
        public IActionResult RelatorioEmProducao()
        {
            return View(_remessaRepository.GetEmProducao());
        }

        // GET: RelatorioAtrasadas
        public IActionResult RelatorioAtrasadas()
        {
            return View(_remessaRepository.GetAtrasadas());
        }

        // GET: RelatorioReceberHoje
        public IActionResult RelatorioReceberHoje()
        {
            return View(_remessaRepository.GetReceberHoje());
        }

        // GET: RelatorioPersonalizado
        public IActionResult RelatorioPersonalizado()
        {
            return RedirectToAction("Index", "Manutencao");
        }

        // POST: RelatorioPersonalizado
        [HttpPost]
        public IActionResult RelatorioPersonalizado(object model)
        {
            return RedirectToAction("Index", "Manutencao");
        }
    }
}

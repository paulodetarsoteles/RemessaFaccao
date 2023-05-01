using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRemessaRepository _remessaRepository;

        public HomeController(IRemessaRepository remessaRepository)
        {
            _remessaRepository = remessaRepository;
        }

        public IActionResult Index()
        {
            _remessaRepository.UpdateStatus();

            ViewData["EnviarParaProducao"] = _remessaRepository.CountEnviarParaProducao();
            ViewData["EmProducao"] = _remessaRepository.CountEmProducao();
            ViewData["Atrasadas"] = _remessaRepository.CountAtrasadas();
            ViewData["ReceberHoje"] = _remessaRepository.CountReceberHoje();

            return View();
        }

        public IActionResult Contatos()
        {
            return View();
        }
    }
}

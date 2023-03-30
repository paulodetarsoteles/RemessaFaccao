using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRemessaRepository _remessaRepository;

        public HomeController(IRemessaRepository remessaRepository)
        {
            _remessaRepository = remessaRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            _remessaRepository.UpdateStatus();

            int enviarParaProducao = _remessaRepository.CountEnviarParaProducao();
            int emProducao = _remessaRepository.CountEmProducao();
            int atrasadas = _remessaRepository.CountAtrasadas();
            int receberHoje = _remessaRepository.CountReceberHoje();

            ViewData["EnviarParaProducao"] = enviarParaProducao;
            ViewData["EmProducao"] = emProducao;
            ViewData["Atrasadas"] = atrasadas;
            ViewData["ReceberHoje"] = receberHoje;

            return View();
        }

        [Authorize]
        public IActionResult Contatos()
        {
            return View();
        }
    }
}

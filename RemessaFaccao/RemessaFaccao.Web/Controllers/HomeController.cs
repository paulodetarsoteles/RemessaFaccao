using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;

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
            try
            {
                _remessaRepository.UpdateStatus();

                ViewData["EnviarParaProducao"] = _remessaRepository.CountEnviarParaProducao();
                ViewData["EmProducao"] = _remessaRepository.CountEmProducao();
                ViewData["Atrasadas"] = _remessaRepository.CountAtrasadas();
                ViewData["ReceberHoje"] = _remessaRepository.CountReceberHoje();

                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("HomeIndex"), string.Format("Erro ao acessar Home Index. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        public IActionResult Contatos() => View(); 
    }
}

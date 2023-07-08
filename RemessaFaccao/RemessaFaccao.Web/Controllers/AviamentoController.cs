using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;
using X.PagedList;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class AviamentoController : Controller
    {
        private readonly IAviamentoRepository _aviamentoRepository;

        public AviamentoController(IAviamentoRepository aviamentoRepository)
        {
            _aviamentoRepository = aviamentoRepository;
        }

        // GET: AviamentoController
        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int page = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "NomeDesc" : "";

                IEnumerable<Aviamento> aviamentos = from a in _aviamentoRepository.GetAll() select a;

                if (sortOrder == "NomeDesc")
                    aviamentos = aviamentos.OrderByDescending(a => a.Nome);
                else
                    aviamentos = aviamentos.OrderBy(a => a.Nome);

                if (searchString != null)
                    page = 1;
                else
                    searchString = currentFilter;

                ViewBag.CurrentFilter = searchString;

                if (!String.IsNullOrEmpty(searchString))
                    aviamentos = aviamentos.Where(a => a.Nome.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);

                ViewBag.PageSize = pageSize;

                return View(aviamentos.ToList().ToPagedList(page, pageSize));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoIndex"), $"Erro ao acessar Aviamento Index. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: AviamentoController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDetails"), $"Erro ao acessar Aviamento Details. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: AviamentoController/Create
        [HttpGet]
        public IActionResult Create() => View();

        // POST: AviamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aviamento aviamento)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido. ");

                _aviamentoRepository.Insert(aviamento);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoCreate"), $"Aviamento {aviamento.AviamentoId} adicionado com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoCreate"), $"Erro ao adicionar Aviamento {aviamento.AviamentoId}. {e.StackTrace} - {DateTime.Now}");
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoEdit"), $"Erro ao acessar Aviamento Edit. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // POST: AviamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Aviamento aviamento)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                _aviamentoRepository.Update(id, aviamento);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoEdit"), $"Aviamento {id} atualizado com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoEdit"), $"Erro ao atualizar Aviamento {id}. {e.StackTrace} - {DateTime.Now}");
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDelete"), $"Erro ao acessar Aviamento Delete. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // POST: AviamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Aviamento aviamento)
        {
            try
            {
                _aviamentoRepository.Delete(id);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDelete"), $"Erro ao excluir Aviamento {id}. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDelete"), $"Erro ao excluir Aviamento {id}. {e.StackTrace} - {DateTime.Now}");
                return View(_aviamentoRepository.GetById(id));
            }
        }
    }
}

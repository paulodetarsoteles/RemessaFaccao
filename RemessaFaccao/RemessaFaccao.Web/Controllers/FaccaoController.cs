using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;
using X.PagedList;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class FaccaoController : Controller
    {
        private readonly IFaccaoRepository _facaoRepository;

        public FaccaoController(IFaccaoRepository facaoRepository)
        {
            _facaoRepository = facaoRepository;
        }

        // GET: FaccaoController
        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int page = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "NomeDesc" : "";
                ViewBag.AtivoSort = sortOrder == "Ativo" ? "AtivoDesc" : "Ativo";

                IEnumerable<Faccao> faccoes = from f in _facaoRepository.GetAll() select f;

                faccoes = sortOrder switch
                {
                    "NomeDesc" => faccoes.OrderByDescending(f => f.Nome),
                    "Ativo" => faccoes.OrderBy(f => f.Ativo),
                    "AtivoDesc" => faccoes.OrderByDescending(f => f.Ativo),
                    _ => faccoes.OrderBy(f => f.Nome),
                };

                if (searchString != null)
                    page = 1;
                else
                    searchString = currentFilter;

                ViewBag.CurrentFilter = searchString;

                if (!String.IsNullOrEmpty(searchString))
                    faccoes = faccoes.Where(f => f.Nome.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);

                ViewBag.PageSize = pageSize;

                return View(faccoes.ToPagedList(page, pageSize));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoIndex"), $"Erro ao acessar Faccao Index. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: FaccaoController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                return View(_facaoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDetails"), $"Erro ao acessar Faccao Details. {e.StackTrace} - {DateTime.Now}");
                return RedirectToAction("Index", "Manutencao");
            }
        }

        // GET: FaccaoController/Create
        [HttpGet]
        public IActionResult Create() => View();

        // POST: FaccaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Faccao faccao)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new("Objeto inválido!");

                _facaoRepository.Insert(faccao);

                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoCreate"), $"Faccao {faccao.FaccaoId} adicionada com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoCreate"), $"Erro ao adicionar Faccao {faccao.FaccaoId}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: FaccaoController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                return View(_facaoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoEdit"), $"Erro ao acessar Faccao {id}. {e.StackTrace} - {DateTime.Now}");
                return RedirectToAction("Index", "Manutencao");
            }
        }

        // POST: FaccaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Faccao faccao)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                _facaoRepository.Update(id, faccao);

                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoEdit"), $"Faccao {id} atualizada com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoEdit"), $"Erro ao atualizar Faccao {id}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: FaccaoController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_facaoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDelete"), $"Erro ao acessar Faccao {id}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // POST: FaccaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Faccao faccao)
        {
            try
            {
                _facaoRepository.Delete(id);

                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDelete"), $"Faccao {id} excluída com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                // ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDelete"), $"Erro ao excluir Faccao {id}. {e.StackTrace} - {DateTime.Now}");
                return View(_facaoRepository.GetById(id));
            }
        }
    }
}

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

                switch (sortOrder)
                {
                    case "NomeDesc":
                        faccoes = faccoes.OrderByDescending(f => f.Nome);
                        break;
                    case "Ativo":
                        faccoes = faccoes.OrderBy(f => f.Ativo);
                        break;
                    case "AtivoDesc":
                        faccoes = faccoes.OrderByDescending(f => f.Ativo);
                        break;
                    default:
                        faccoes = faccoes.OrderBy(f => f.Nome);
                        break;
                }

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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoIndex"), string.Format("Erro ao acessar Faccao Index. {0}", DateTime.Now.ToString()));
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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDetails"), string.Format("Erro ao acessar Faccao Details. {0}", DateTime.Now.ToString()));
                return View();
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

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoCreate"), string.Format("Faccao {0} adicionada com sucesso. ", faccao.Nome, DateTime.Now.ToString()));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoCreate"), string.Format("Erro ao adicionar Faccao {0}. {1}", faccao.Nome, DateTime.Now.ToString()));
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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoEdit"), string.Format("Erro ao acessar Faccao Edit. {0}", DateTime.Now.ToString()));
                return View();
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

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoEdit"), string.Format("Erro ao atualizar Faccao {0}. {1}", id, DateTime.Now.ToString()));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoEdit"), string.Format("Erro ao atualizar Faccao {0}. {1}", id, DateTime.Now.ToString()));
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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDelete"), string.Format("Erro ao excluir Faccao {0}. {1}", id, DateTime.Now.ToString()));
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

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDelete"), string.Format("Erro ao excluir Faccao {0}. {1}", id, DateTime.Now.ToString()));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("FaccaoDelete"), string.Format("Erro ao excluir Faccao {0}. {1}", id, DateTime.Now.ToString()));
                return View(_facaoRepository.GetById(id));
            }
        }
    }
}

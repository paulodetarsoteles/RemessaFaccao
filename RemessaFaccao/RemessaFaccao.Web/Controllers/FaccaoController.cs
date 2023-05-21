using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

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
        public ActionResult Index()
        {
            try
            {
                return View(_facaoRepository.GetAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                Faccao result = _facaoRepository.GetById(id);

                if (result is null)
                {
                    throw new("Falha ao localizar!");
                }
                return View(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FaccaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faccao faccao)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                {
                    throw new("Objeto inválido!");
                }

                DateTime dateTime = DateTime.Now;

                if (!_facaoRepository.Insert(faccao))
                {
                    Console.WriteLine("Erro ao adicionar faccao {0}. {1}", faccao.Nome, dateTime);
                    throw new("Falha ao tentar adicionar facção!");
                }

                Console.WriteLine("Faccao {0} adicionada com sucesso. {0}", faccao.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // POST: FaccaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Faccao faccao)
        {
            if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Objeto inválido!");
                return View(faccao);
            }

            bool result = _facaoRepository.Update(id, faccao);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Facção {0} adicionada com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar a facção {0}. {1}", id, dateTime);
                ModelState.AddModelError("", "Falha ao tentar atualizar facção!");
                return View(faccao);
            }
        }

        // GET: FaccaoController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // POST: FaccaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Faccao faccao)
        {
            DateTime dateTime = DateTime.Now;

            if (_facaoRepository.Delete(id))
            {
                Console.WriteLine("Facção {0} excluída com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao excluir a facção {0}. {1}", id, dateTime);
                ModelState.AddModelError("", "Falha ao tentar excluir facção!");
                return View(faccao);
            }
        }
    }
}

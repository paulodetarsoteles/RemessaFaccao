using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    public class FaccaoController : Controller
    {
        private readonly IFaccaoRepository _facaoRepository;

        public FaccaoController(IFaccaoRepository facaoRepository)
        {
            _facaoRepository = facaoRepository;
        }

        // GET: FaccaoController
        [Authorize]
        public ActionResult Index()
        {
            return View(_facaoRepository.GetAll());
        }

        // GET: FaccaoController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // GET: FaccaoController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FaccaoController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faccao faccao)
        {
            bool result = _facaoRepository.Insert(faccao);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Faccao {0} adicionada com sucasso. {0}", faccao.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar faccao {0}. {1}", faccao.Nome, dateTime);
                return View(faccao);
            }
        }

        // GET: FaccaoController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // POST: FaccaoController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Faccao faccao)
        {
            bool result = _facaoRepository.Update(id, faccao);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Facção {0} adicionada com sucasso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar a facção {0}. {1}", id, dateTime);
                return View(faccao);
            }
        }

        // GET: FaccaoController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // POST: FaccaoController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Faccao faccao)
        {
            bool result = _facaoRepository.Delete(id);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Facção {0} excluída com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao excluir a facção {0}. {1}", id, dateTime);
                return View(faccao);
            }
        }
    }
}

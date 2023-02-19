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
        public ActionResult Index()
        {
            return View(_facaoRepository.GetAll());
        }

        // GET: FaccaoController/Details/5
        public ActionResult Details(int id)
        {
            return View(_facaoRepository.GetById(id)); 
        }

        // GET: FaccaoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FaccaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faccao faccao)
        {
            bool result = _facaoRepository.Insert(faccao); 
            DateTime dateTime = DateTime.Now; 

            if (result)
            {
                Console.WriteLine("Faccao {0} adicionada com sucasso. {1}", faccao.FaccaoId, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar faccao {0}. {1}", faccao.FaccaoId, dateTime);
                return View(faccao);
            }
        }

        // GET: FaccaoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FaccaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FaccaoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FaccaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

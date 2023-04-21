using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    public class AviamentoController : Controller
    {
        private readonly IAviamentoRepository _aviamentoRepository;

        public AviamentoController(IAviamentoRepository aviamentoRepository)
        {
            _aviamentoRepository = aviamentoRepository;
        }

        // GET: AviamentoController
        public ActionResult Index()
        {
            return View(_aviamentoRepository.GetAll());
        }

        // GET: AviamentoController/Details/5
        public ActionResult Details(int id)
        {
            Aviamento result = _aviamentoRepository.GetById(id);

            if (result == null)
            {
                ModelState.AddModelError("", "Falha ao localizar!");
                return View(); 
            }

            return View(result);
        }

        // GET: AviamentoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AviamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AviamentoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AviamentoController/Edit/5
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

        // GET: AviamentoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AviamentoController/Delete/5
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

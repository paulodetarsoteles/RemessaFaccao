using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

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
        public ActionResult Index()
        {
            _aviamentoRepository.Count();
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
        public ActionResult Create(Aviamento aviamento)
        {
            bool result = _aviamentoRepository.Insert(aviamento); 
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Aviamento {0} adicionado com sucesso. {1}", aviamento.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar aviamento {0}. {1}", aviamento.Nome, dateTime);
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Index", "Manutencao");
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
            return RedirectToAction("Index", "Manutencao");
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

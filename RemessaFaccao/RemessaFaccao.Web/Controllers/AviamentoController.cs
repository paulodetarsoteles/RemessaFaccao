using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(_aviamentoRepository.GetAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: AviamentoController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: AviamentoController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AviamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aviamento aviamento)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido. ");

                DateTime dateTime = DateTime.Now;
                _aviamentoRepository.Insert(aviamento);

                Console.WriteLine("Aviamento {0} adicionado com sucesso. {1}", aviamento.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: AviamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Aviamento aviamento)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                DateTime dateTime = DateTime.Now;
                _aviamentoRepository.Update(id, aviamento);

                Console.WriteLine("Aviamento {0} adicionado com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: AviamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Aviamento aviamento)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                _aviamentoRepository.Delete(id);

                Console.WriteLine("Aviamento {0} excluído com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View(aviamento);
            }
        }
    }
}

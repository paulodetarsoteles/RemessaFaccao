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
            return View(_aviamentoRepository.GetAll());
        }

        // GET: AviamentoController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            Aviamento result = _aviamentoRepository.GetById(id);

            if (result is null)
            {
                ModelState.AddModelError("", "Falha ao localizar!");
                return View();
            }

            return View(result);
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
            if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Objeto inválido!");
                return View(aviamento);
            }

            DateTime dateTime = DateTime.Now;

            if (_aviamentoRepository.Insert(aviamento))
            {
                Console.WriteLine("Aviamento {0} adicionado com sucesso. {1}", aviamento.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar aviamento {0}. {1}", aviamento.Nome, dateTime);
                ModelState.AddModelError("", "Falha ao adicionar!");
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_aviamentoRepository.GetById(id));
        }

        // POST: AviamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Aviamento aviamento)
        {
            if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Objeto inválido!");
                return View(aviamento); 
            }

            DateTime dateTime = DateTime.Now;

            if (_aviamentoRepository.Update(id, aviamento))
            {
                Console.WriteLine("Aviamento {0} adicionado com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar a aviamento {0}. {1}", id, dateTime);
                ModelState.AddModelError("", "Falha ao tentar atualizar aviamento!");
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_aviamentoRepository.GetById(id));
        }

        // POST: AviamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Aviamento aviamento)
        {
            DateTime dateTime = DateTime.Now;

            if (_aviamentoRepository.Delete(id))
            {
                Console.WriteLine("Aviamento {0} excluído com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao excluir aviamento {0}. {1}", id, dateTime);
                ModelState.AddModelError("", "Falha ao tentar excluir aviamento!");
                return View(aviamento);
            }
        }
    }
}

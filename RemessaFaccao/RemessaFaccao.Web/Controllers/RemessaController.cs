using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.Enums;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class RemessaController : Controller
    {
        private readonly IRemessaRepository _remessaRepository;

        public RemessaController(IRemessaRepository remessaRepository)
        {
            _remessaRepository = remessaRepository;
        }

        // GET: RemessaController
        [HttpGet]
        public ActionResult Index()
        {
            _remessaRepository.UpdateStatus();
            return View(_remessaRepository.GetAll());
        }

        // GET: RemessaController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            Remessa result = _remessaRepository.GetById(id);

            if (result == null)
            {
                ModelState.AddModelError("", "Falha ao localizar!");
                return View();
            }
            return View(result);
        }

        // GET: RemessaController/Create
        [HttpGet]
        public ActionResult Create()
        {
            List<Faccao> faccoes = _remessaRepository.GetFaccoesAtivas();
            ViewBag.Faccoes = new SelectList(faccoes, "FaccaoId", "Nome");

            return View();
        }

        // POST: RemessaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Remessa remessa)
        {
            if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Objeto inválido!");
                return View(remessa);
            }

            remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;
            remessa.StatusRemessa = StatusRemessa.Preparada;

            List<Faccao> faccoes = _remessaRepository.GetFaccoesAtivas();
            ViewBag.Faccoes = new SelectList(faccoes, "FaccaoId", "Nome");

            bool result = _remessaRepository.Insert(remessa);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Remessa {0} adicionada com sucesso. {1}", remessa.Referencia, dateTime.ToString());
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar remessa {0}. {1}", remessa.Referencia, dateTime.ToString());
                return View(remessa);
            }
        }

        // GET: RemessaController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<Faccao> faccoes = _remessaRepository.GetFaccoesAtivas();
            ViewBag.Faccoes = new SelectList(faccoes, "FaccaoId", "Nome");

            return View(_remessaRepository.GetById(id));
        }

        // POST: RemessaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Remessa remessa)
        {
            if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Objeto inválido!");
                return View(remessa);
            }

            remessa.RemessaId = id;
            remessa.Quantidade = remessa.Tamanho1 + remessa.Tamanho2 + remessa.Tamanho4 + remessa.Tamanho6 + remessa.Tamanho8 + remessa.Tamanho10 + remessa.Tamanho12; 
            remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;

            List<Faccao> faccoes = _remessaRepository.GetFaccoesAtivas();
            ViewBag.Faccoes = new SelectList(faccoes, "FaccaoId", "Nome", remessa.FaccaoId);

            bool result = _remessaRepository.Update(id, remessa);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Remessa {0} editada com sucesso. {1}", id, dateTime.ToString());
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao editar perfil {0}. {1}", id, dateTime.ToString());
                return View(remessa);
            }
        }

        // GET: RemessaController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_remessaRepository.GetById(id));
        }

        // POST: RemessaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Remessa remessa)
        {
            bool result = _remessaRepository.Delete(id);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Remessa {0} excluída com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao excluír remessa {0}. {1}", id, dateTime);
                return View(remessa);
            }
        }

        // GET: RemessaController/ToPrint/5
        [HttpGet]
        public ActionResult ToPrint(int id)
        {
            Remessa result = _remessaRepository.GetById(id);

            if (result == null)
            {
                ModelState.AddModelError("", "Falha ao localizar!");
                return View();
            }
            return View(result);
        }
    }
}

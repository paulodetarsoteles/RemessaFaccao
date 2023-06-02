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
        public ActionResult Index(string sortOrder, string search)
        {
            try
            {
                _remessaRepository.UpdateStatus();

                ViewData["contagem"] = _remessaRepository.CountReceberHoje() + _remessaRepository.CountEnviarParaProducao() + _remessaRepository.CountEmProducao() + _remessaRepository.CountAtrasadas();
                ViewData["statusSort"] = String.IsNullOrEmpty(sortOrder) ? "StatusDesc" : "";
                ViewData["referenciaSort"] = sortOrder == "Referencia" ? "ReferenciaDesc" : "Referencia";
                ViewData["quantidadeSort"] = sortOrder == "Quantidade" ? "QuantidadeDesc" : "Quantidade";
                ViewData["valorSort"] = sortOrder == "ValorTotal" ? "ValorTotalDesc" : "ValorTotal";

                IEnumerable<Remessa> remessas = from r in _remessaRepository.GetAll() select r;

                switch (sortOrder)
                {
                    case "StatusDesc":
                        remessas = remessas.OrderByDescending(r => r.StatusRemessa);
                        break;
                    case "Referencia":
                        remessas = remessas.OrderBy(r => r.Referencia);
                        break;
                    case "ReferenciaDesc":
                        remessas = remessas.OrderByDescending(r => r.Referencia);
                        break;
                    case "Quantidade":
                        remessas = remessas.OrderBy(r => r.Quantidade);
                        break;
                    case "QuantidadeDesc":
                        remessas = remessas.OrderByDescending(r => r.Quantidade);
                        break;
                    case "ValorTotal":
                        remessas = remessas.OrderBy(r => r.ValorTotal);
                        break;
                    case "ValorTotalDesc":
                        remessas = remessas.OrderByDescending(r => r.ValorTotal);
                        break;
                    default:
                        remessas = remessas.OrderBy(r => r.StatusRemessa);
                        break;
                }

                ViewData["CurrentFilter"] = search;

                if (!String.IsNullOrEmpty(search))
                    remessas = remessas.Where(s => s.Referencia.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1);

                return View(remessas.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RemessaController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("Index", "Manutencao", e.Message);
            }
        }

        // GET: RemessaController/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: RemessaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Remessa remessa)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                DateTime dateTime = DateTime.Now;

                remessa.Quantidade = remessa.Tamanho1 + remessa.Tamanho2 + remessa.Tamanho4 + remessa.Tamanho6 + remessa.Tamanho8 + remessa.Tamanho10 + remessa.Tamanho12;
                remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;
                remessa.StatusRemessa = StatusRemessa.Preparada;

                _remessaRepository.Insert(remessa);

                Console.WriteLine("Remessa {0} adicionada com sucesso. {1}", remessa.Referencia, dateTime.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RemessaController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: RemessaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Remessa remessa)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty() || remessa is null)
                    throw new Exception("Objeto inválido!");

                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome", remessa.FaccaoId);
                DateTime dateTime = DateTime.Now;

                remessa.RemessaId = id;
                remessa.Quantidade = remessa.Tamanho1 + remessa.Tamanho2 + remessa.Tamanho4 + remessa.Tamanho6 + remessa.Tamanho8 + remessa.Tamanho10 + remessa.Tamanho12;
                remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;

                _remessaRepository.Update(id, remessa);

                Console.WriteLine("Remessa {0} editada com sucesso. {1}", id, dateTime.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RemessaController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: RemessaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Remessa remessa)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                _remessaRepository.Delete(id);

                Console.WriteLine("Remessa {0} excluída com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: RemessaController/ToPrint/5
        [HttpGet]
        public ActionResult ToPrint(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
    }
}

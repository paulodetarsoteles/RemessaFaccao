using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.Enums;
using RemessaFaccao.DAL.Repositories.Interfaces;
using X.PagedList;

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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int page = 1, int pageSize = 10)
        {
            try
            {
                _remessaRepository.UpdateStatus();

                ViewData["contagem"] = _remessaRepository.CountReceberHoje() + _remessaRepository.CountEnviarParaProducao() + _remessaRepository.CountEmProducao() + _remessaRepository.CountAtrasadas();

                ViewBag.StatusSort = String.IsNullOrEmpty(sortOrder) ? "StatusDesc" : "";
                ViewBag.ReferenciaSort = sortOrder == "Referencia" ? "ReferenciaDesc" : "Referencia";
                ViewBag.QuantidadeSort = sortOrder == "Quantidade" ? "QuantidadeDesc" : "Quantidade";
                ViewBag.ValorSort = sortOrder == "ValorTotal" ? "ValorTotalDesc" : "ValorTotal";

                IEnumerable<Remessa> remessas = from r in _remessaRepository.GetAll() select r;

                remessas = sortOrder switch
                {
                    "StatusDesc" => remessas.OrderByDescending(r => r.StatusRemessa),
                    "Referencia" => remessas.OrderBy(r => r.Referencia),
                    "ReferenciaDesc" => remessas.OrderByDescending(r => r.Referencia),
                    "Quantidade" => remessas.OrderBy(r => r.Quantidade),
                    "QuantidadeDesc" => remessas.OrderByDescending(r => r.Quantidade),
                    "ValorTotal" => remessas.OrderBy(r => r.ValorTotal),
                    "ValorTotalDesc" => remessas.OrderByDescending(r => r.ValorTotal),
                    _ => remessas.OrderBy(r => r.StatusRemessa),
                };

                if (searchString != null)
                    page = 1;
                else
                    searchString = currentFilter;

                ViewBag.CurrentFilter = searchString;

                if (!String.IsNullOrEmpty(searchString))
                    remessas = remessas.Where(s => s.Referencia.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);

                ViewBag.PageSize = pageSize; 

                return View(remessas.ToList().ToPagedList(page, pageSize));
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
                ViewBag.Aviamentos = new SelectList(_remessaRepository.GetAviamentosParaRemessa(), "AviamentoId", "Nome"); 
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
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                ViewBag.Aviamentos = new SelectList(_remessaRepository.GetAviamentosParaRemessa(), "AviamentoId", "Nome");

                string aviamentosId = Request.Form["chkAviamento"].ToString(); 

                if (!string.IsNullOrEmpty(aviamentosId))
                {
                    int[] splitAviamantos = aviamentosId.Split(',').Select(int.Parse).ToArray();

                    if (splitAviamantos.Length > 0)
                    {
                        List<Aviamento> aviamentos = _remessaRepository.GetAviamentosParaRemessa();

                        foreach (int aviamentoId in splitAviamantos)
                        {
                            remessa.Aviamentos.Add(aviamentos.First(a => a.AviamentoId == aviamentoId));
                        }
                    }
                }

                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

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

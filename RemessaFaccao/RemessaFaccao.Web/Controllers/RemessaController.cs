using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.Enums;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;
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
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int page = 1, int pageSize = 10)
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
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaIndex"), $"Erro ao acessar Remessa Index. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: RemessaController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaDetails"), $"Erro ao acessar Remessa Details. {e.StackTrace} - {DateTime.Now}");
                return RedirectToAction("Index", "Manutencao", e.Message);
            }
        }

        // GET: RemessaController/RecebidaDetails/5
        [HttpGet]
        public IActionResult RecebidaDetails(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRecebidaDetails"), $"Erro ao acessar Remessa RecebidaDetails. {e.StackTrace} - {DateTime.Now}");
                return RedirectToAction("Index", "Manutencao", e.Message);
            }
        }

        // GET: RemessaController/Create
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                ViewBag.Aviamentos = new SelectList(_remessaRepository.GetAviamentosParaRemessa(), "AviamentoId", "Nome");
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaCreate"), $"Erro ao acessar Remessa Create. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // POST: RemessaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Remessa remessa)
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                ViewBag.Aviamentos = new SelectList(_remessaRepository.GetAviamentosParaRemessa(), "AviamentoId", "Nome");

                if (_remessaRepository.ValidateReferencia(remessa.Referencia))
                    throw new Exception("Já existe uma referência com este nome.");

                if (remessa.Tamanho1 < 0 || remessa.Tamanho2 < 0 || remessa.Tamanho4 < 0 || remessa.Tamanho6 < 0 || remessa.Tamanho8 < 0 || remessa.Tamanho10 < 0 || remessa.Tamanho12 < 0)
                    throw new Exception("Nenhum tamanho pode ter um número menor que zero.");

                string aviamentosId = Request.Form["chkAviamento"].ToString();

                if (!string.IsNullOrEmpty(aviamentosId))
                {
                    int[] splitAviamantos = aviamentosId.Split(',').Select(int.Parse).ToArray();

                    if (splitAviamantos.Length > 0)
                    {
                        List<Aviamento> aviamentos = _remessaRepository.GetAviamentosParaRemessa();

                        foreach (int aviamentoId in splitAviamantos)
                            remessa.Aviamentos.Add(aviamentos.First(a => a.AviamentoId == aviamentoId));
                    }
                }

                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                remessa.Quantidade = remessa.Tamanho1 + remessa.Tamanho2 + remessa.Tamanho4 + remessa.Tamanho6 + remessa.Tamanho8 + remessa.Tamanho10 + remessa.Tamanho12;

                if (remessa.Quantidade == 0)
                    throw new Exception("Quantidade não pode ser igual a zero.");

                if (remessa.ValorUnitario <= 0)
                    throw new Exception("Valor não pode ser menor ou igual a zero.");

                remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;
                remessa.StatusRemessa = StatusRemessa.Preparada;

                _remessaRepository.Insert(remessa);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaCreate"), $"Remessa {remessa.RemessaId} adicionada com sucesso.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaCreate"), $"Erro ao adicionar Remessa {remessa.RemessaId}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: RemessaController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Faccoes = new SelectList(_remessaRepository.GetFaccoesAtivas(), "FaccaoId", "Nome");
                ViewBag.Aviamentos = new SelectList(_remessaRepository.GetAviamentosParaRemessa(), "AviamentoId", "Nome");

                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaEdit"), $"Erro ao acessar Remessa Edit. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // POST: RemessaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Remessa remessa)
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

                if (!ModelState.IsValid || ModelState.IsNullOrEmpty() || remessa is null)
                    throw new Exception("Objeto inválido!");

                remessa.RemessaId = id;
                remessa.Quantidade = remessa.Tamanho1 + remessa.Tamanho2 + remessa.Tamanho4 + remessa.Tamanho6 + remessa.Tamanho8 + remessa.Tamanho10 + remessa.Tamanho12;
                remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;

                _remessaRepository.Update(id, remessa);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaEdit"), $"Remessa {remessa.RemessaId} atualizada com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaEdit"), $"Erro ao atualizar Remessa {id}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: RemessaController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaDelete"), $"Erro ao acessar Remessa {id}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // POST: RemessaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Remessa remessa)
        {
            try
            {
                _remessaRepository.Delete(id);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaDelete"), $"Remessa {id} excluída com sucesso. {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaDelete"), $"Erro ao Exlcuir Remessa {id}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }

        // GET: RemessaController/ToPrint/5
        [HttpGet]
        public IActionResult ToPrint(int id)
        {
            try
            {
                return View(_remessaRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaToPrint"), $"Erro ao acessar ToPrint da Remessa {id}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }
    }
}

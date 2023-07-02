﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;
using X.PagedList;

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
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int page = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "NomeDesc" : "";

                IEnumerable<Aviamento> aviamentos = from a in _aviamentoRepository.GetAll() select a;

                if (sortOrder == "NomeDesc")
                    aviamentos = aviamentos.OrderByDescending(a => a.Nome);
                else
                    aviamentos = aviamentos.OrderBy(a => a.Nome);

                if (searchString != null)
                    page = 1;
                else
                    searchString = currentFilter;

                ViewBag.CurrentFilter = searchString;

                if (!String.IsNullOrEmpty(searchString))
                    aviamentos = aviamentos.Where(a => a.Nome.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);

                ViewBag.PageSize = pageSize;

                return View(aviamentos.ToList().ToPagedList(page, pageSize));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoIndex"), string.Format("Erro ao acessar Aviamento Index. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // GET: AviamentoController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDetails"), string.Format("Erro ao acessar Aviamento Details. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // GET: AviamentoController/Create
        [HttpGet]
        public IActionResult Create() => View();

        // POST: AviamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aviamento aviamento)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido. ");

                _aviamentoRepository.Insert(aviamento);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoCreate"), string.Format("Aviamento {0} adicionado com sucesso. {1}", aviamento.Nome, DateTime.Now.ToString()));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoCreate"), string.Format("Erro ao adicionar Aviamento {0}. {1} - {2}", aviamento.Nome, e.Message, DateTime.Now.ToString()));
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoEdit"), string.Format("Erro ao acessar Aviamento Edit. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // POST: AviamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Aviamento aviamento)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                _aviamentoRepository.Update(id, aviamento);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoEdit"), string.Format("Aviamento {0} atualizado com sucesso. {1}", id, DateTime.Now.ToString()));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoEdit"), string.Format("Erro ao atualizar Aviamento {0}. {1} - {2}", id, e.Message, DateTime.Now.ToString()));
                return View(aviamento);
            }
        }

        // GET: AviamentoController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_aviamentoRepository.GetById(id));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDelete"), string.Format("Erro ao acessar Aviamento Delete. {0} - {1}", e.Message, DateTime.Now.ToString()));
                return View();
            }
        }

        // POST: AviamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Aviamento aviamento)
        {
            try
            {
                _aviamentoRepository.Delete(id);

                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDelete"), string.Format("Erro ao excluir Aviamento {0}. {1}", id, DateTime.Now.ToString()));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoDelete"), string.Format("Erro ao excluir Aviamento {0}. {1} - {2}", id, e.Message, DateTime.Now.ToString()));
                return View(_aviamentoRepository.GetById(id));
            }
        }
    }
}

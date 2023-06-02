﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class FaccaoController : Controller
    {
        private readonly IFaccaoRepository _facaoRepository;

        public FaccaoController(IFaccaoRepository facaoRepository)
        {
            _facaoRepository = facaoRepository;
        }

        // GET: FaccaoController
        [HttpGet]
        public ActionResult Index(string sortOrder, string search)
        {
            try
            {
                ViewData["nomeSort"] = String.IsNullOrEmpty(sortOrder) ? "NomeDesc" : "";
                ViewData["ativoSort"] = sortOrder == "Ativo" ? "AtivoDesc" : "Ativo";
                ViewData["CurrentFilter"] = search;

                IEnumerable<Faccao> faccoes = from f in _facaoRepository.GetAll() select f;

                if (String.IsNullOrEmpty(search))
                    faccoes = faccoes.Where(f => f.Nome.Contains(search)); 

                switch (sortOrder)
                {
                    case "NomeDesc":
                        faccoes = faccoes.OrderByDescending(f => f.Nome);
                        break;
                    case "Ativo":
                        faccoes = faccoes.OrderBy(f => f.Ativo);
                        break;
                    case "AtivoDesc":
                        faccoes = faccoes.OrderByDescending(f => f.Ativo);
                        break;
                    default:
                        faccoes = faccoes.OrderBy(f => f.Nome); 
                        break;
                }

                return View(faccoes.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                return View(_facaoRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FaccaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faccao faccao)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new("Objeto inválido!");

                DateTime dateTime = DateTime.Now;
                _facaoRepository.Insert(faccao);

                Console.WriteLine("Faccao {0} adicionada com sucesso. {0}", faccao.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_facaoRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: FaccaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Faccao faccao)
        {
            try
            {
                if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
                    throw new Exception("Objeto inválido!");

                _facaoRepository.Update(id, faccao);
                DateTime dateTime = DateTime.Now;

                Console.WriteLine("Facção {0} adicionada com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: FaccaoController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_facaoRepository.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // POST: FaccaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Faccao faccao)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                _facaoRepository.Delete(id);

                Console.WriteLine("Facção {0} excluída com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError("", e.Message);
                return View(_facaoRepository.GetById(id));
            }
        }
    }
}

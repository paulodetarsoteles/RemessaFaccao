﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Index()
        {
            return View(_facaoRepository.GetAll());
        }

        // GET: FaccaoController/Details/5
        public ActionResult Details(int id)
        {
            Faccao result = _facaoRepository.GetById(id); 

            if (result == null)
            {
                ModelState.AddModelError("", "Falha ao localizar!");
                return View();
            }
            return View(result);
        }

        // GET: FaccaoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FaccaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faccao faccao)
        {
            bool result = _facaoRepository.Insert(faccao);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Faccao {0} adicionada com sucesso. {0}", faccao.Nome, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar faccao {0}. {1}", faccao.Nome, dateTime);
                return View(faccao);
            }
        }

        // GET: FaccaoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // POST: FaccaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Faccao faccao)
        {
            bool result = _facaoRepository.Update(id, faccao);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Facção {0} adicionada com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar a facção {0}. {1}", id, dateTime);
                return View(faccao);
            }
        }

        // GET: FaccaoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_facaoRepository.GetById(id));
        }

        // POST: FaccaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Faccao faccao)
        {
            bool result = _facaoRepository.Delete(id);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Facção {0} excluída com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao excluir a facção {0}. {1}", id, dateTime);
                return View(faccao);
            }
        }
    }
}

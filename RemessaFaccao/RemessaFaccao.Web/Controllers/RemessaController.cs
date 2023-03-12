﻿using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.Enums;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    public class RemessaController : Controller
    {
        private readonly IRemessaRepository _remessaRepository; 

        public RemessaController(IRemessaRepository remessaRepository)
        {
            _remessaRepository = remessaRepository; 
        }

        // GET: RemessaController
        public ActionResult Index()
        {
            return View(_remessaRepository.GetAll()); 
        }

        // GET: RemessaController/Details/5
        public ActionResult Details(int id)
        {
            return View(_remessaRepository.GetById(id));
        }

        // GET: RemessaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RemessaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Remessa remessa)
        {
            remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade;
            remessa.StatusRemessa = StatusRemessa.Preparada; 

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
        public ActionResult Edit(int id)
        {
            Remessa remessa = _remessaRepository.GetById(id); 
            return View(remessa);
        }

        // POST: RemessaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Remessa remessa)
        {
            remessa.ValorTotal = remessa.ValorUnitario * remessa.Quantidade; 
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
        public ActionResult Delete(int id)
        {
            Remessa remessa = _remessaRepository.GetById(id); 
            return View(remessa);
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
    }
}

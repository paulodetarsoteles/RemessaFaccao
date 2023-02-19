using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        // GET: PerfilController
        public ActionResult Index()
        {
            return View(_perfilRepository.GetAll());
        }

        // GET: PerfilController/Details/5
        public ActionResult Details(int id)
        {
            return View(_perfilRepository.GetById(id));
        }

        // GET: PerfilController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerfilController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Perfil perfil)
        {
            bool result = _perfilRepository.Insert(perfil);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Perfil {0} adicionado com sucesso. {1}", perfil.Nome, dateTime.ToString());
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao adicionar perfil {0}. {1}", perfil.Nome, dateTime.ToString());
                return View(perfil);
            }
        }

        // GET: PerfilController/Edit/5
        public ActionResult Edit(int id)
        {
            Perfil perfil = _perfilRepository.GetById(id);
            return View(perfil);
        }

        // POST: PerfilController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Perfil perfil)
        {
            bool result = _perfilRepository.Update(id, perfil); ; 
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Perfil {0} editado com sucesso. {1}", id, dateTime.ToString());
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao editar perfil {0}. {1}", id, dateTime.ToString());
                return View(perfil);
            }
        }

        // GET: PerfilController/Delete/5
        public ActionResult Delete(int id)
        {
            Perfil perfil = _perfilRepository.GetById(id); 
            return View(perfil);
        }

        // POST: PerfilController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Perfil perfil)
        {
            bool result = _perfilRepository.Delete(id); 
            DateTime dateTime = DateTime.Now; 

            if(result)
            {
                Console.WriteLine("Perfil {0} excluído com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao excluír perfil {0}. {1}", id, dateTime); 
                return View(perfil); 
            }
        }
    }
}

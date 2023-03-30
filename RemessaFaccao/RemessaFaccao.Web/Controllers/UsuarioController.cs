using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;

namespace RemessaFaccao.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET: UsuarioController
        [Authorize]
        public ActionResult Index()
        {
            return View(_usuarioRepository.GetAll());
        }

        // GET: UsuarioController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View(_usuarioRepository.GetById(id));
        }

        // GET: UsuarioController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            bool result = _usuarioRepository.Insert(usuario);
            DateTime dateTime = DateTime.Now;

            if (result)
            {
                Console.WriteLine("Usuário {0} adicionado com sucesso. {1}", usuario.Nome, dateTime);
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                Console.WriteLine("Erro ao adicionar usuário {0}. {1}", usuario.Nome, dateTime);
                return View(usuario);
            }
        }

        // GET: UsuarioController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Usuario usuario = _usuarioRepository.GetById(id); ;
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {
            bool result = _usuarioRepository.Update(id, usuario);
            DateTime dateTime = DateTime.Now; 

            if (result)
            {
                Console.WriteLine("Usuário {0} atualizado com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                Console.WriteLine("Erro ao editar o usuário {0}. {1}", id, dateTime);
                return View(usuario);
            }
        }

        // GET: UsuarioController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Usuario usuario = _usuarioRepository.GetById(id); 
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario usuario)
        {
            bool result = _usuarioRepository.Delete(id);
            DateTime dateTime = DateTime.Now; 

            if (result)
            {
                Console.WriteLine("Usuário {0} excluído com sucesso. {1}", id, dateTime);
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                Console.WriteLine("Erro ao excluir o usuário {0}. {1}", id, dateTime);
                return View(usuario); 
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RemessaFaccao.Web.Controllers
{
    public class RemessaController : Controller
    {
        // GET: RemessaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RemessaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RemessaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RemessaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RemessaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RemessaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RemessaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RemessaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

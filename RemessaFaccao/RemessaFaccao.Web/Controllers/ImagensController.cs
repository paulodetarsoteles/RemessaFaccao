using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Setting;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class ImagensController : Controller
    {
        private readonly PathFiles _pathFiles;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImagensController(IWebHostEnvironment webHostEnvironment, IOptions<PathFiles> pathFiles)
        {
            _webHostEnvironment = webHostEnvironment;
            _pathFiles = pathFiles.Value; 
        }

        //GET: ImagensController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models.ViewModels;
using RemessaFaccao.DAL.Setting;

namespace RemessaFaccao.Web.Controllers
{
    [Authorize]
    public class ImagensController : Controller
    {
        private readonly PathFiles _pathFiles;
        private readonly IWebHostEnvironment _environment;

        public ImagensController(IWebHostEnvironment environment, IOptions<PathFiles> pathFiles)
        {
            _environment = environment;
            _pathFiles = pathFiles.Value;
        }

        //GET: ImagensController
        [HttpGet]
        public IActionResult Index() => View();

        //POST: ImagensController/UploadImagens
        [HttpPost]
        public async Task<IActionResult> UploadImagens(IFormFile file)
        {
            try
            {
                if (file is null)
                {
                    ViewData["Erro"] = "Nenhuma imagem não selecionada.";
                    return View(ViewData);
                }

                if (file.Length > 10000000)
                {
                    ViewData["Erro"] = "Tamanho máximo da imagem ultrapassou 10Mb.";
                    return View(ViewData);
                }

                string pathImagens = Path.Combine(_environment.WebRootPath, _pathFiles.PathImagesUpload);

                if (!Directory.Exists(pathImagens))
                    Directory.CreateDirectory(pathImagens);

                string fileNameWithPath = $"{pathImagens}\\{file.FileName}";

                if (file.FileName.Contains(".jpg") || file.FileName.Contains(".jpeg") || file.FileName.Contains(".gif") || file.FileName.Contains(".png") || file.FileName.Contains(".bmp"))
                {
                    using FileStream stream = new(fileNameWithPath, FileMode.Create);

                    await file.CopyToAsync(stream);
                }

                ViewData["Resultado"] = $"Imagem enviada.";

                ViewBag.Arquivo = file.FileName;

                return View(ViewData);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Erro inesperado ao enviar imagens!");
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("ImagemController"), $"Erro ao acessar {MethodBase.GetCurrentMethod()}. {e.StackTrace} - {DateTime.Now}");
                return View(ViewData["Erro"] = "Um erro inesperado ocorreu ao enviar as imagens.");
            }
        }

        public IActionResult GetImagens()
        {
            try
            {
                ManagerImagesViewModel model = new();

                DirectoryInfo directory = new(Path.Combine(_environment.WebRootPath, _pathFiles.PathImagesUpload));

                FileInfo[] files = directory.GetFiles();

                if (files is null || files.Length == 0 || files.Count() == 0)
                {
                    return View();
                }

                model.PathImages = _pathFiles.PathImagesUpload;

                if (files.Length == 0 || files is null)
                    throw new Exception("Nenhum arquivo encontrado.");

                model.Files = files;

                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Erro inesperado ao buscar imagens!");
                //ConfigHelper.WriteLog(ConfigHelper.PathOutLog("ImagemController"), $"Erro ao acessar {MethodBase.GetCurrentMethod()}. {e.StackTrace} - {DateTime.Now}");
                return View(ViewData["Erro"] = "Um erro inesperado ocorreu ao enviar as imagens.");
            }
        }

        public IActionResult DeleteFile(string imgName)
        {
            string pathCompleteImg = Path.Combine(_environment.WebRootPath, _pathFiles.PathImagesUpload, imgName);

            if (!System.IO.File.Exists(pathCompleteImg))
            {
                ViewData["Erro"] = $"Imagem {imgName} não encontrada!";

                return View(ViewData);
            }

            System.IO.File.Delete(pathCompleteImg);

            ViewData["Deletado"] = imgName;

            return View("Index");
        }
    }
}

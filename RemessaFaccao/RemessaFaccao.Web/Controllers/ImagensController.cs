using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Setting;
using System.Reflection;

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
        public IActionResult Index() => View();

        //GET: ImagensController/UploadImagens
        [HttpGet]
        public async Task<IActionResult> UploadImagens(List<IFormFile> files)
        {
            try
            {
                if (files is null || files.Count == 0)
                {
                    ViewData["Erro"] = "Nenhuma imagem não selecionada.";
                    return View(ViewData);
                }

                if (files.Count > 5)
                {
                    ViewData["Erro"] = "Quantidade de imagens não pode ser maior que 5.";
                    return View(ViewData);
                }

                if (files.Sum(f => f.Length) > 10000000)
                {
                    ViewData["Erro"] = "Tamanho máximo das imagens ultrapassou 10Mb.";
                    return View(ViewData);
                }

                List<string> filePathsName = new();

                foreach (IFormFile file in files)
                {
                    if (file.FileName.Contains(".jpg") || file.FileName.Contains(".jpeg") || file.FileName.Contains(".gif") || file.FileName.Contains(".png") || file.FileName.Contains(".bmp"))
                    {
                        string fileNameWithPath = $"{_pathFiles}\\{file.FileName}";

                        filePathsName.Add(fileNameWithPath);

                        using FileStream stream = new(fileNameWithPath, FileMode.Create);

                        await file.CopyToAsync(stream);
                    }
                }

                ViewData["Resultado"] = $"{files.Count} imagens enviadas.";

                ViewBag.Arquivos = filePathsName;

                return View(ViewData);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Erro inesperado ao enviar imagens!");
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("ImagemController"), $"Erro ao acessar {MethodBase.GetCurrentMethod()}. {e.StackTrace} - {DateTime.Now}");
                return View();
            }
        }
    }
}

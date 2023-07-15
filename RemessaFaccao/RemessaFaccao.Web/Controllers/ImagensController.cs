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

        public ImagensController(IOptions<PathFiles> pathFiles)
        {
            _pathFiles = pathFiles.Value;
        }

        //GET: ImagensController
        [HttpGet]
        public IActionResult Index() => View();

        //POST: ImagensController/UploadImagens
        [HttpPost]
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

                string pathImagens = _pathFiles.PathImagesUpload;

                if (!Directory.Exists(pathImagens))
                    Directory.CreateDirectory(pathImagens);

                foreach (IFormFile file in files)
                {
                    if (file.FileName.Contains(".jpg") || file.FileName.Contains(".jpeg") || file.FileName.Contains(".gif") || file.FileName.Contains(".png") || file.FileName.Contains(".bmp"))
                    {
                        string fileNameWithPath = $"{pathImagens}\\{file.FileName}";

                        filePathsName.Add(fileNameWithPath);

                        using FileStream stream = new(fileNameWithPath, FileMode.Create);

                        await file.CopyToAsync(stream);
                    }
                }
                if (files.Count == 1)
                    ViewData["Resultado"] = $"Imagem enviada.";
                else
                    ViewData["Resultado"] = $"{files.Count} imagens enviadas.";

                ViewBag.Arquivos = filePathsName;

                return View(ViewData);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Erro inesperado ao enviar imagens!");
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("ImagemController"), $"Erro ao acessar {MethodBase.GetCurrentMethod()}. {e.StackTrace} - {DateTime.Now}");
                return View(ViewData["Erro"] = "Um erro inesperado ocorreu ao enviar as imagens.");
            }
        }
    }
}

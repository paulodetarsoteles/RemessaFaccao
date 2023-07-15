using Microsoft.AspNetCore.Http;

namespace RemessaFaccao.DAL.Models.ViewModels
{
    public class ManagerImagesViewModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile FormFile { get; set; }
        public List<IFormFile> FormFiles { get; set; }
        public string PathImages { get; set; }
    }
}

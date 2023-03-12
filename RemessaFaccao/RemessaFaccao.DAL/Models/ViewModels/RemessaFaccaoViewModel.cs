using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models.ViewModels
{
    public class RemessaFaccaoViewModel
    {
        [Display(Name = "Facção")]
        public string Nome { get; set; }
        public int Quantidade { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }
    }
}

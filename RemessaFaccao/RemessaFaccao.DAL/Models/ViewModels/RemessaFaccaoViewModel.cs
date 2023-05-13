using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models.ViewModels
{
    public class RemessaFaccaoViewModel
    {
        [Display(Name = "Código")]
        public int RemessaId { get; set; }
        [Display(Name = "Referência")]
        public string Referencia { get; set; }
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
        [Display(Name = "Facção")]
        public string Nome { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemessaFaccao.DAL.Models
{
    public class Aviamento
    {
        [Key]
        [Display(Name = "Código")]
        public int AviamentoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [NotMapped]
        public virtual List<Remessa> Remessa { get; set; } = null; 
    }
}

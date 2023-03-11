using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemessaFaccao.DAL.Models
{
    public class Faccao
    {
        [Key]
        [Display(Name = "Código")]
        public int FaccaoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public string FormaDePagamento { get; set; }

        [Display(Name = "Qualificação")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, 10, ErrorMessage = "Digite uma nota entre 0 a 10 (sem vírgulas)")]
        public int Qualificacao { get; set; }

        public bool Ativo { get; set; } = true; 

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [NotMapped]
        public virtual List<Remessa> Remessas { get; set; }
    }
}

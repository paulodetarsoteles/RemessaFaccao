using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models
{
    public class Faccao
    {
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

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Qualificacao { get; set; }

        public bool Ativo { get; set; } = true; 

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}

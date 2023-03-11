using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "Código")]
        public int UsuarioId { get; set; }

        [Display(Name = "Código do Pefil")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}

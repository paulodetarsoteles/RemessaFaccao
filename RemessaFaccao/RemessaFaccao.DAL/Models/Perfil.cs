using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models
{
    public class Perfil
    {
        [Display(Name = "Código")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
    }
}

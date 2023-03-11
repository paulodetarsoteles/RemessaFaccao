using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemessaFaccao.DAL.Models
{
    public class Perfil
    {
        [Key]
        [Display(Name = "Código")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [NotMapped]
        public virtual List<Usuario> Usuarios { get; set; }
    }
}

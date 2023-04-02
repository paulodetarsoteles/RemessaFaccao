using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite o usuário para acesso")]
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha não pode ficar vazia")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }
}

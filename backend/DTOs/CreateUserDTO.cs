using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "O usuario precisa de um nome!")]
        public string Name {get;set;} = null!;

        [Required(ErrorMessage = "O usuario precisa de um Email!")]
        public string Email {get;set;} = null!;

        [Required(ErrorMessage = "O usuario precisa de uma Senha!")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres!")]
        public string Password {get;set;} = null!;
    }
}
using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        [UniqueEmail(ErrorMessage = "O email já está em uso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O telefone não é válido.")]
        [UniqueTelefone(ErrorMessage = "O telefone já está em uso.")]
        public string Telefone { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }
}

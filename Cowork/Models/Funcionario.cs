using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public string Cargo { get; set; }
        public ICollection<Reserva>? Reservas { get; set; }
    }
}

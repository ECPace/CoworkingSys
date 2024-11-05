using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }
        public ICollection<Reserva>? Reservas { get; set; }
    }
}

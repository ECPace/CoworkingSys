using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Sala
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A capacidade é obrigatória.")]
        public int Capacidade { get; set; }
        [Required(ErrorMessage = "O preço por hora é obrigatório.")]
        public decimal PrecoPorHora { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }
}

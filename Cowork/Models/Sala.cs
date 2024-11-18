using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Sala
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A capacidade é obrigatória.")]
        [Range(1, 100, ErrorMessage = "A capacidade deve estar entre 1 e 100.")]
        public int Capacidade { get; set; }

        [Required(ErrorMessage = "O preço por hora é obrigatório.")]
        [Range(0.01, 1000.00, ErrorMessage = "O preço por hora deve estar entre 0,01 e 1000,00.")]
        [Display(Name = "Preço por hora")]
        public decimal PrecoPorHora { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }
}

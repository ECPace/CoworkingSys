using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        public DateTime DataReserva { get; set; }
        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        public TimeSpan HorarioInicio { get; set; }
        [Required(ErrorMessage = "O horário de fim é obrigatório.")]
        public TimeSpan HorarioFim { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }
        public ICollection<Funcionario>? Funcionarios { get; set; }
        public decimal ValorTotal
        {
            get
            {
                if (Sala == null) return 0;

                // Calcula a duração da reserva em horas
                var duracaoHoras = (HorarioFim - HorarioInicio).TotalHours;
                return (decimal)duracaoHoras * Sala.PrecoPorHora;
            }
        }
    }
}

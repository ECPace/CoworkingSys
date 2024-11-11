using FoolProof.Core;
using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data da reserva inválida.")]
        public DateTime DataReserva { get; set; }

        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        [DataType(DataType.Time, ErrorMessage = "Horário de início inválido.")]
        public TimeSpan HorarioInicio { get; set; }

        [Required(ErrorMessage = "O horário de fim é obrigatório.")]
        [DataType(DataType.Time, ErrorMessage = "Horário de fim inválido.")]
        [GreaterThan("HorarioInicio", ErrorMessage = "O horário de fim deve ser maior que o horário de início.")]
        public TimeSpan HorarioFim { get; set; }

        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public int SalaId { get; set; }

        public Sala? Sala { get; set; }

        public ICollection<Funcionario>? Funcionarios { get; set; }

        public List<int> FuncionariosIds { get; set; } = new List<int>();

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

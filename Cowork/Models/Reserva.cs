namespace Cowork.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DataReserva { get; set; }
        public TimeSpan HorarioInicio { get; set; }
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

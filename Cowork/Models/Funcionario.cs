namespace Cowork.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}

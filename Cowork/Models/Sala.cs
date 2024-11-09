namespace Cowork.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public decimal PrecoPorHora { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }
}

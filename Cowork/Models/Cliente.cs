using Cowork.Data;
using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(CoworkContext)) as CoworkContext;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Contexto não pode ser nulo.");
            }
            var email = value?.ToString();
            var cliente = context.Clientes.FirstOrDefault(c => c.Email == email);

            if (cliente != null)
            {
                return new ValidationResult("O email já está em uso.");
            }

            return ValidationResult.Success;
        }
    }
    public class UniqueTelefoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(CoworkContext)) as CoworkContext;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Contexto não pode ser nulo.");
            }
            var telefone = value?.ToString();
            var cliente = context.Clientes.FirstOrDefault(c => c.Telefone == telefone);

            if (cliente != null)
            {
                return new ValidationResult("O telefone já está em uso.");
            }

            return ValidationResult.Success;
        }
    }
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

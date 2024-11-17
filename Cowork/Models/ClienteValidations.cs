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
            var clienteId = (int)validationContext.ObjectType.GetProperty("Id")?.GetValue(validationContext.ObjectInstance, null);
            var cliente = context.Clientes.FirstOrDefault(c => c.Email == email && c.Id != clienteId);

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
            var clienteId = (int)validationContext.ObjectType.GetProperty("Id")?.GetValue(validationContext.ObjectInstance, null);
            var cliente = context.Clientes.FirstOrDefault(c => c.Telefone == telefone && c.Id != clienteId);

            if (cliente != null)
            {
                return new ValidationResult("O telefone já está em uso.");
            }

            return ValidationResult.Success;
        }
    }
}

using Cowork.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public static class ReservaValidations
    {
        public static ValidationResult? ValidateDataReserva(DateTime dataReserva, ValidationContext context)
        {
            if (dataReserva.Date < DateTime.Now.Date)
            {
                return new ValidationResult("A data da reserva não pode ser no passado.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateHorarioInicio(TimeSpan horarioInicio, ValidationContext context)
        {
            var instance = context.ObjectInstance as Reserva;
            if (instance != null && instance.DataReserva.Date == DateTime.Now.Date && horarioInicio < DateTime.Now.TimeOfDay)
            {
                return new ValidationResult("O horário de início não pode ser no passado.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateHorarioFim(TimeSpan horarioFim, ValidationContext context)
        {
            var instance = context.ObjectInstance as Reserva;
            if (instance != null && instance.DataReserva.Date == DateTime.Now.Date && horarioFim < DateTime.Now.TimeOfDay)
            {
                return new ValidationResult("O horário de fim não pode ser no passado.");
            }
            return ValidationResult.Success;
        }
        public static ValidationResult? ValidateReservaUnica(object value, ValidationContext context)
        {
            var reserva = context.ObjectInstance as Reserva;
            if (reserva == null)
            {
                throw new ArgumentException("A instância de reserva não é válida.");
            }

            var dbContext = context.GetService(typeof(CoworkContext)) as CoworkContext;
            if (dbContext == null)
            {
                throw new ArgumentException("DbContext não encontrado no ValidationContext.");
            }

            var reservasExistentes = dbContext.Reservas
                .Where(r => r.SalaId == reserva.SalaId && r.DataReserva.Date == reserva.DataReserva.Date && r.Id != reserva.Id)
                .ToList();

            foreach (var reservaExistente in reservasExistentes)
            {
                if ((reserva.HorarioInicio < reservaExistente.HorarioFim && reserva.HorarioFim > reservaExistente.HorarioInicio))
                {
                    return new ValidationResult("Já existe uma reserva para esta sala no horário especificado.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

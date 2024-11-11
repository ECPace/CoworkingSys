using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    public class Reserva : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data da reserva inválida.")]
        [CustomValidation(typeof(Reserva), nameof(ValidateDataReserva))]
        public DateTime DataReserva { get; set; }

        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        [DataType(DataType.Time, ErrorMessage = "Horário de início inválido.")]
        [CustomValidation(typeof(Reserva), nameof(ValidateHorarioInicio))]
        public TimeSpan HorarioInicio { get; set; }

        [Required(ErrorMessage = "O horário de fim é obrigatório.")]
        [DataType(DataType.Time, ErrorMessage = "Horário de fim inválido.")]
        [GreaterThan("HorarioInicio", ErrorMessage = "O horário de fim deve ser maior que o horário de início.")]
        [CustomValidation(typeof(Reserva), nameof(ValidateHorarioFim))]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HorarioFim <= HorarioInicio)
            {
                yield return new ValidationResult("O horário de fim deve ser maior que o horário de início.", new[] { nameof(HorarioFim) });
            }
        }
    }
}

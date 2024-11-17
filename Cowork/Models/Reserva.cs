using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cowork.Models
{
    [CustomValidation(typeof(ReservaValidations), nameof(ReservaValidations.ValidateReservaUnica))]
    public class Reserva : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data da reserva inválida.")]
        [CustomValidation(typeof(ReservaValidations), nameof(ReservaValidations.ValidateDataReserva))]
        [Display(Name = "Data da Reserva")]
        public DateTime DataReserva { get; set; }

        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        [DataType(DataType.Time, ErrorMessage = "Horário de início inválido.")]
        [CustomValidation(typeof(ReservaValidations), nameof(ReservaValidations.ValidateHorarioInicio))]
        [Display(Name = "Horário Inicial")]
        public TimeSpan HorarioInicio { get; set; }

        [Required(ErrorMessage = "O horário de fim é obrigatório.")]
        [DataType(DataType.Time, ErrorMessage = "Horário de fim inválido.")]
        [CustomValidation(typeof(ReservaValidations), nameof(ReservaValidations.ValidateHorarioFim))]
        [Display(Name = "Horário Final")]
        public TimeSpan HorarioFim { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "A sala é obrigatória.")]
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }
        public bool AvisoExclusaoFuncionario { get; set; } = false;

        public ICollection<Funcionario>? Funcionarios { get; set; } = new List<Funcionario>();

        [Display(Name = "Funcionários")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HorarioFim <= HorarioInicio)
            {
                yield return new ValidationResult("O horário de fim deve ser maior que o horário de início.", new[] { nameof(HorarioFim) });
            }
            if ((HorarioFim - HorarioInicio).TotalMinutes < 30)
            {
                yield return new ValidationResult("A diferença entre o horário de início e o horário de fim deve ser de no mínimo 30 minutos.", new[] { nameof(HorarioFim) });
            }
        }
    }
}

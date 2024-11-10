using Cowork.Models;
using System;
using Xunit;

namespace Cowork.Test
{
    public class ValorTotalTests
    {
        [Fact]
        public void ValorTotal_CalculatesCorrectly()
        {
            // Arrange
            var sala = new Sala { PrecoPorHora = 100 };
            var reserva = new Reserva
            {
                Sala = sala,
                HorarioInicio = new TimeSpan(9, 0, 0),
                HorarioFim = new TimeSpan(11, 0, 0)
            };

            // Act
            var valorTotal = reserva.ValorTotal;

            // Assert
            Assert.Equal(200, valorTotal);
        }
    }
}

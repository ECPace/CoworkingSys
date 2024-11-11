//using Cowork.Controllers;
//using Cowork.Data;
//using Cowork.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace Cowork.Test
//{
//    public class ReservaControllerTests : IDisposable
//    {
//        private readonly ReservaController _controller;
//        private readonly CoworkContext _context;

//        public ReservaControllerTests()
//        {
//            var options = new DbContextOptionsBuilder<CoworkContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Usar um banco de dados em memória único para cada teste
//                .Options;

//            _context = new CoworkContext(options);
//            _controller = new ReservaController(_context);

//            // Adicionar dados de teste para Clientes e Salas
//            _context.Clientes.AddRange(
//                new Cliente { Id = 1, Nome = "Cliente 1", Email = "cliente1@example.com", Telefone = "123456789" },
//                new Cliente { Id = 2, Nome = "Cliente 2", Email = "cliente2@example.com", Telefone = "987654321" }
//            );

//            _context.Salas.AddRange(
//                new Sala { Id = 1, Nome = "Sala 1", Capacidade = 10, PrecoPorHora = 100 },
//                new Sala { Id = 2, Nome = "Sala 2", Capacidade = 20, PrecoPorHora = 200 }
//            );

//            _context.SaveChanges();

//            // Adicionar dados de teste para Reservas
//            _context.Reservas.AddRange(
//                new Reserva { Id = 1, ClienteId = 1, SalaId = 1, DataReserva = DateTime.Now, HorarioInicio = new TimeSpan(9, 0, 0), HorarioFim = new TimeSpan(10, 0, 0) },
//                new Reserva { Id = 2, ClienteId = 2, SalaId = 2, DataReserva = DateTime.Now, HorarioInicio = new TimeSpan(10, 0, 0), HorarioFim = new TimeSpan(11, 0, 0) }
//            );
//            _context.SaveChanges();
//        }

//        public void Dispose()
//        {
//            _context.Dispose();
//        }

//        [Fact]
//        public async Task Index_ReturnsViewResult_WithAListOfReservas()
//        {
//            // Act
//            var result = await _controller.Index();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<List<Reserva>>(viewResult.Model);
//            Assert.Equal(2, model.Count);
//        }

//        [Fact]
//        public void Create_ReturnsViewResult()
//        {
//            // Act
//            var result = _controller.Create();

//            // Assert
//            Assert.IsType<ViewResult>(result);
//        }

//        [Fact]
//        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
//        {
//            // Arrange
//            var reserva = new Reserva { Id = 3, ClienteId = 1, SalaId = 1, DataReserva = DateTime.Now, HorarioInicio = new TimeSpan(11, 0, 0), HorarioFim = new TimeSpan(12, 0, 0) };

//            // Act
//            var result = await _controller.Create(reserva);

//            // Assert
//            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("Index", redirectToActionResult.ActionName);
//        }

//        [Fact]
//        public async Task Details_ReturnsViewResult_WithReserva()
//        {
//            // Act
//            var result = await _controller.Details(1);

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<Reserva>(viewResult.ViewData.Model);
//            Assert.Equal(1, model.Id);
//        }

//        [Fact]
//        public async Task Edit_ReturnsViewResult_WithReserva()
//        {
//            // Act
//            var result = await _controller.Edit(1);

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<Reserva>(viewResult.ViewData.Model);
//            Assert.Equal(1, model.Id);
//        }

//        [Fact]
//        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
//        {
//            // Arrange
//            var reserva = new Reserva { Id = 1, ClienteId = 1, SalaId = 1, DataReserva = DateTime.Now, HorarioInicio = new TimeSpan(9, 0, 0), HorarioFim = new TimeSpan(11, 0, 0) };

//            // Desanexar todas as entidades rastreadas
//            foreach (var entity in _context.ChangeTracker.Entries().ToList())
//            {
//                entity.State = EntityState.Detached;
//            }

//            // Buscar a entidade do contexto
//            var reservaToUpdate = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.Id == 1);

//            if (reservaToUpdate != null)
//            {
//                // Atualizar os valores da entidade
//                reservaToUpdate.ClienteId = reserva.ClienteId;
//                reservaToUpdate.SalaId = reserva.SalaId;
//                reservaToUpdate.DataReserva = reserva.DataReserva;
//                reservaToUpdate.HorarioInicio = reserva.HorarioInicio;
//                reservaToUpdate.HorarioFim = reserva.HorarioFim;

//                // Anexar a entidade atualizada e definir seu estado como Modified
//                _context.Attach(reservaToUpdate).State = EntityState.Modified;

//                // Act
//                var result = await _controller.Edit(1, reservaToUpdate);

//                // Assert
//                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
//                Assert.Equal("Index", redirectToActionResult.ActionName);
//            }
//            else
//            {
//                // Tratar o caso onde reservaToUpdate é nulo
//                Assert.True(false, "Reserva não encontrada para atualização.");
//            }
//        }

//        [Fact]
//        public async Task Delete_ReturnsViewResult_WithReserva()
//        {
//            // Act
//            var result = await _controller.Delete(1);

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<Reserva>(viewResult.ViewData.Model);
//            Assert.Equal(1, model.Id);
//        }

//        [Fact]
//        public async Task DeleteConfirmed_DeletesReserva_AndRedirects()
//        {
//            // Act
//            var result = await _controller.DeleteConfirmed(1);

//            // Assert
//            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("Index", redirectToActionResult.ActionName);
//            Assert.Null(await _context.Reservas.FindAsync(1));
//        }
//    }
//}

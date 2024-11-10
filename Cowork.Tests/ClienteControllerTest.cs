using Cowork.Controllers;
using Cowork.Data;
using Cowork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cowork.Test
{
    public class ClienteControllerTests
    {
        private readonly ClienteController _controller;
        private readonly CoworkContext _context;

        public ClienteControllerTests()
        {
            var options = new DbContextOptionsBuilder<CoworkContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CoworkContext(options);
            _controller = new ClienteController(_context);

            // Limpar o banco de dados antes de adicionar dados de teste
            _context.Clientes.RemoveRange(_context.Clientes);
            _context.SaveChanges();

            // Seed the database with test data
            _context.Clientes.AddRange(
                new Cliente { Id = 1, Nome = "Cliente 1", Email = "cliente1@example.com", Telefone = "123456789" },
                new Cliente { Id = 2, Nome = "Cliente 2", Email = "cliente2@example.com", Telefone = "987654321" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithAListOfClientes()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Cliente>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithCliente()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Cliente>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var cliente = new Cliente { Id = 3, Nome = "Cliente Teste", Email = "emailteste@example.com", Telefone = "010111011" };

            // Act
            var result = await _controller.Create(cliente);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithCliente()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Cliente>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Cliente Editado", Email = "emailteste@example.com", Telefone = "010111011" };

            // Desanexar todas as entidades rastreadas
            foreach (var entity in _context.ChangeTracker.Entries().ToList())
            {
                entity.State = EntityState.Detached;
            }

            // Buscar a entidade do contexto
            var clienteToUpdate = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            if (clienteToUpdate != null)
            {
                // Atualizar os valores da entidade
                clienteToUpdate.Nome = cliente.Nome;
                clienteToUpdate.Email = cliente.Email;
                clienteToUpdate.Telefone = cliente.Telefone;

                // Anexar a entidade atualizada e definir seu estado como Modified
                _context.Attach(clienteToUpdate).State = EntityState.Modified;

                // Act
                var result = await _controller.Edit(1, clienteToUpdate);

                // Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
            else
            {
                // Tratar o caso onde clienteToUpdate é nulo
                Assert.True(false, "Cliente não encontrado para atualização.");
            }
        }


        [Fact]
        public async Task Delete_ReturnsViewResult_WithCliente()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Cliente>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult()
        {
            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}

using Cowork.Controllers;
using Cowork.Data;
using Cowork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cowork.Test
{
    public class SalaControllerTests
    {
        private readonly SalaController _controller;
        private readonly CoworkContext _context;

        public SalaControllerTests()
        {
            var options = new DbContextOptionsBuilder<CoworkContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CoworkContext(options);
            _controller = new SalaController(_context);

            // Limpar o banco de dados antes de adicionar dados de teste
            _context.Salas.RemoveRange(_context.Salas);
            _context.SaveChanges();

            // Seed the database with test data
            _context.Salas.AddRange(
                new Sala { Id = 1, Nome = "Sala 1" },
                new Sala { Id = 2, Nome = "Sala 2" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithAListOfSalas()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Sala>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithSala()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sala>(viewResult.Model);
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
            var sala = new Sala { Id = 3, Nome = "Sala Teste" };

            // Act
            var result = await _controller.Create(sala);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithSala()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sala>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var sala = new Sala { Id = 1, Nome = "Sala Editada" };

            // Desanexar todas as entidades rastreadas
            foreach (var entity in _context.ChangeTracker.Entries().ToList())
            {
                entity.State = EntityState.Detached;
            }

            // Buscar a entidade do contexto
            var salaToUpdate = await _context.Salas.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            if (salaToUpdate != null)
            {
                // Atualizar os valores da entidade
                salaToUpdate.Nome = sala.Nome;

                // Anexar a entidade atualizada e definir seu estado como Modified
                _context.Attach(salaToUpdate).State = EntityState.Modified;

                // Act
                var result = await _controller.Edit(1, salaToUpdate);

                // Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
            else
            {
                // Tratar o caso onde salaToUpdate é nulo
                Assert.True(false, "Sala não encontrada para atualização.");
            }
        }
        [Fact]
        public async Task Delete_ReturnsViewResult_WithSala()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sala>(viewResult.Model);
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

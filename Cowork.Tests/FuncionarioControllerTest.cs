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
    public class FuncionarioControllerTests
    {
        private readonly FuncionarioController _controller;
        private readonly CoworkContext _context;

        public FuncionarioControllerTests()
        {
            var options = new DbContextOptionsBuilder<CoworkContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CoworkContext(options);
            _controller = new FuncionarioController(_context);

            // Limpar o banco de dados antes de adicionar dados de teste
            _context.Funcionarios.RemoveRange(_context.Funcionarios);
            _context.SaveChanges();

            // Seed the database with test data
            _context.Funcionarios.AddRange(
                new Funcionario { Id = 1, Nome = "Funcionario 1", Cargo = "Cargo 1" },
                new Funcionario { Id = 2, Nome = "Funcionario 2", Cargo = "Cargo 2" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithAListOfFuncionarios()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Funcionario>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithFuncionario()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Funcionario>(viewResult.Model);
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
            var funcionario = new Funcionario { Id = 3, Nome = "Funcionario Teste", Cargo = "Cargo Teste" };

            // Act
            var result = await _controller.Create(funcionario);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithFuncionario()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Funcionario>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var funcionario = new Funcionario { Id = 1, Nome = "Funcionario Editado", Cargo = "Cargo Teste" };

            // Desanexar todas as entidades rastreadas
            foreach (var entity in _context.ChangeTracker.Entries().ToList())
            {
                entity.State = EntityState.Detached;
            }

            // Buscar a entidade do contexto
            var funcionarioToUpdate = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == 1);

            if (funcionarioToUpdate != null)
            {
                // Atualizar os valores da entidade
                funcionarioToUpdate.Nome = funcionario.Nome;
                funcionarioToUpdate.Cargo = funcionario.Cargo;

                // Anexar a entidade atualizada e definir seu estado como Modified
                _context.Attach(funcionarioToUpdate).State = EntityState.Modified;

                // Act
                var result = await _controller.Edit(1, funcionarioToUpdate);

                // Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
            else
            {
                // Tratar o caso onde funcionarioToUpdate é nulo
                Assert.True(false, "Funcionario não encontrado para atualização.");
            }
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithFuncionario()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Funcionario>(viewResult.Model);
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

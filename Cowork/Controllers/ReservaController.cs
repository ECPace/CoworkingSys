using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cowork.Data;
using Cowork.Models;

namespace Cowork.Controllers
{
    public class ReservaController : Controller
    {
        private readonly CoworkContext _context;

        public ReservaController(CoworkContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = _context.Reservas.Include(r => r.Cliente).Include(r => r.Sala);
            return View(await reservas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Sala)
                .Include(r => r.Funcionarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }
        public IActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nome");
            ViewBag.SalaId = new SelectList(_context.Salas, "Id", "Nome");
            ViewBag.FuncionariosIds = new MultiSelectList(_context.Funcionarios, "Id", "Nome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    reserva.Funcionarios = new List<Funcionario>();
                    foreach (var id in reserva.FuncionariosIds)
                    {
                        var funcionario = await _context.Funcionarios.FindAsync(id);
                        if (funcionario != null)
                        {
                            reserva.Funcionarios.Add(funcionario);
                        }
                    }

                    _context.Add(reserva);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao salvar a reserva: {ex.Message}");
                }
            }
            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nome");
            ViewBag.SalaId = new SelectList(_context.Salas, "Id", "Nome");
            ViewBag.FuncionariosIds = new MultiSelectList(_context.Funcionarios, "Id", "Nome");
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reserva = await _context.Reservas
                .Include(r => r.Funcionarios)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (reserva == null)
                return NotFound();

            reserva.FuncionariosIds = reserva.Funcionarios.Select(f => f.Id).ToList();

            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nome", reserva.ClienteId);
            ViewBag.SalaId = new SelectList(_context.Salas, "Id", "Nome", reserva.SalaId);
            ViewBag.FuncionariosIds = new MultiSelectList(_context.Funcionarios, "Id", "Nome", reserva.FuncionariosIds);
            return View(reserva);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var reservaToUpdate = await _context.Reservas
                        .Include(r => r.Funcionarios)
                        .FirstOrDefaultAsync(r => r.Id == id);

                    if (reservaToUpdate == null)
                        return NotFound();

                    reservaToUpdate.DataReserva = reserva.DataReserva;
                    reservaToUpdate.HorarioInicio = reserva.HorarioInicio;
                    reservaToUpdate.HorarioFim = reserva.HorarioFim;
                    reservaToUpdate.ClienteId = reserva.ClienteId;
                    reservaToUpdate.SalaId = reserva.SalaId;

                    // Atualizar funcionários
                    reservaToUpdate.Funcionarios.Clear();
                    foreach (var funcionarioId in reserva.FuncionariosIds)
                    {
                        var funcionario = await _context.Funcionarios.FindAsync(funcionarioId);
                        if (funcionario != null)
                        {
                            reservaToUpdate.Funcionarios.Add(funcionario);
                        }
                    }

                    _context.Update(reservaToUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Reservas.Any(e => e.Id == reserva.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nome", reserva.ClienteId);
            ViewBag.SalaId = new SelectList(_context.Salas, "Id", "Nome", reserva.SalaId);
            ViewBag.FuncionariosIds = new MultiSelectList(_context.Funcionarios, "Id", "Nome", reserva.FuncionariosIds);
            return View(reserva);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Reservas)
                .ThenInclude(r => r.Sala)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes
                                        .Include(c => c.Reservas)
                                        .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // Redireciona para a lista de clientes
        }
    }
}

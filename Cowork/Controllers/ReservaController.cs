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

        public IActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nome");
            ViewBag.SalaId = new SelectList(_context.Salas, "Id", "Nome");
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid) 
            { 
                try 
                {
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
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
                return NotFound();

            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nome");
            ViewBag.SalaId = new SelectList(_context.Salas, "Id", "Nome");
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
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Reservas.Any(e => e.Id == reserva.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reserva == null)
                return NotFound();

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

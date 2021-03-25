using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CargoTransportation.Data;
using CargoTransportation.Models;
using CargoTransportation.ViewModels;
using CargoTransportation.ViewModels.Filters;

namespace CargoTransportation.Controllers
{
    public class CargoesController : Controller
    {
        private readonly CargoTransportationContext _context;

        public CargoesController(CargoTransportationContext context)
        {
            _context = context;
        }

        // GET: Cargoes
        public async Task<IActionResult> Index(int? selectedTypeOfCargoId, int page = 1)
        {
            var pageSize = 10;
            var itemCount = _context.Cargos.Count();
            IQueryable<Cargo> cargos = _context.Cargos;

            if (selectedTypeOfCargoId.HasValue && selectedTypeOfCargoId.Value != 0)
            {
                cargos = cargos.Where(c => c.TypeOfCargoId == selectedTypeOfCargoId);
            }

            cargos = cargos.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.TypeOfCargo);

            return View(new CargoViewModel()
            {
                Cargos = await cargos.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                CargoFilter = new CargoFilter(selectedTypeOfCargoId, await _context.TypeOfCargos.ToListAsync())
                
            });
        }

        // GET: Cargoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.Include(c => c.TypeOfCargo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargoes/Create
        public IActionResult Create()
        {
            ViewData["TypeOfCargoId"] = new SelectList(_context.TypeOfCargos, "Id", "Name");
            return View();
        }

        // POST: Cargoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeOfCargoId,ShelfLife,Features")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfCargoId"] = new SelectList(_context.TypeOfCargos, "Id", "Name", cargo.TypeOfCargoId);
            return View(cargo);
        }

        // GET: Cargoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            ViewData["TypeOfCargoId"] = new SelectList(_context.TypeOfCargos, "Id", "Name", cargo.TypeOfCargoId);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeOfCargoId,ShelfLife,Features")] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfCargoId"] = new SelectList(_context.TypeOfCargos, "Id", "Name", cargo.TypeOfCargoId);
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.Include(c => c.TypeOfCargo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoExists(int id)
        {
            return _context.Cargos.Any(e => e.Id == id);
        }
    }
}

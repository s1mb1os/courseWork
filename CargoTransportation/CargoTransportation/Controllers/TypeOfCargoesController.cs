using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CargoTransportation.Data;
using CargoTransportation.Models;

namespace CargoTransportation.Controllers
{
    public class TypeOfCargoesController : Controller
    {
        private readonly CargoTransportationContext _context;

        public TypeOfCargoesController(CargoTransportationContext context)
        {
            _context = context;
        }

        // GET: TypeOfCargoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfCargos.ToListAsync());
        }

        // GET: TypeOfCargoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfCargo = await _context.TypeOfCargos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfCargo == null)
            {
                return NotFound();
            }

            return View(typeOfCargo);
        }

        // GET: TypeOfCargoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfCargoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] TypeOfCargo typeOfCargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfCargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfCargo);
        }

        // GET: TypeOfCargoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfCargo = await _context.TypeOfCargos.FindAsync(id);
            if (typeOfCargo == null)
            {
                return NotFound();
            }
            return View(typeOfCargo);
        }

        // POST: TypeOfCargoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] TypeOfCargo typeOfCargo)
        {
            if (id != typeOfCargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfCargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfCargoExists(typeOfCargo.Id))
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
            return View(typeOfCargo);
        }

        // GET: TypeOfCargoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfCargo = await _context.TypeOfCargos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfCargo == null)
            {
                return NotFound();
            }

            return View(typeOfCargo);
        }

        // POST: TypeOfCargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfCargo = await _context.TypeOfCargos.FindAsync(id);
            _context.TypeOfCargos.Remove(typeOfCargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfCargoExists(int id)
        {
            return _context.TypeOfCargos.Any(e => e.Id == id);
        }
    }
}

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
    public class CarsController : Controller
    {
        private readonly CargoTransportationContext _context;

        public CarsController(CargoTransportationContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(DateTime? techStartDate, DateTime? techEndDate, int page = 1)
        {
            var pageSize = 10;
            var itemCount = _context.Cargos.Count();

            IQueryable<Car> cars = _context.Cars;

            if (techStartDate.HasValue)
            {
                cars = cars.Where(c => c.TechInspection >= techStartDate.Value);
            }

            if (techEndDate.HasValue)
            {
                cars = cars.Where(c => c.TechInspection <= techEndDate.Value);
            }

            cars = cars.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.CarBrand)
                .Include(c => c.Employee);

            return View(new CarViewModel()
            {
                Cars = await cars.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                CarFilter = new CarFilter(techStartDate, techEndDate)
            });
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNumber,VinNumber,EngineNumber,YearOfIssue,TechInspection,EmployeeId,CarBrandId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "Name", car.CarBrandId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", car.EmployeeId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "Name", car.CarBrandId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", car.EmployeeId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNumber,VinNumber,EngineNumber,YearOfIssue,TechInspection,EmployeeId,CarBrandId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "Name", car.CarBrandId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", car.EmployeeId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}

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
    public class FlightsController : Controller
    {
        private readonly CargoTransportationContext _context;

        public FlightsController(CargoTransportationContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index(bool flightsFromLastWeek, int page = 1)
        {
            var pageSize = 10;
            var itemCount = _context.Employees.Count();

            IQueryable<Flight> flights = _context.Flights;

            if (flightsFromLastWeek)
            {
                var lastWeekDate = DateTime.Now.AddDays(-7);
                flights = flights.Where(f => f.EndDate <= lastWeekDate);
            }

            flights = flights.Include(f => f.Car)
                .Include(f => f.Cargo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return View(new FlightViewModel()
            {
                Flights = await flights.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                FlightFilter = new FlightFilter(flightsFromLastWeek)
            });
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Car)
                .Include(f => f.Cargo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "RegNumber");
            ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "Name");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Customer,StartPoint,EndPoint,StartDate,EndDate,CarId,CargoId,Price,IsPayment,IsReturn")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "RegNumber", flight.CarId);
            ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "Name", flight.CargoId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "RegNumber", flight.CarId);
            ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "Name", flight.CargoId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Customer,StartPoint,EndPoint,StartDate,EndDate,CarId,CargoId,Price,IsPayment,IsReturn")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "RegNumber", flight.CarId);
            ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "Name", flight.CargoId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Car)
                .Include(f => f.Cargo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }
}

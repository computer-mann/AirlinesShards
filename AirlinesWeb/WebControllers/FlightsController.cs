using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlinesWeb.Models.DbContexts;
using System.Diagnostics;
using AirlinesWeb.Models.Tables;

namespace AirlinesWeb.WebControllers
{
    public class FlightsController : Controller
    {
        private readonly AirlinesContext _context;
        private readonly ILogger<FlightsController> logger;

        public FlightsController(AirlinesContext context,ILogger<FlightsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            
            var count = await _context.Flights.AsNoTracking().CountAsync();
            //var airlinesContext = _context.Flights.Include(f => f.AircraftCodeNavigation).Include(f => f.ArrivalAirportNavigation).Include(f => f.DepartureAirportNavigation);
            return View(count);
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.AircraftCodeNavigation)
                .Include(f => f.ArrivalAirportNavigation)
                .Include(f => f.DepartureAirportNavigation)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["AircraftCode"] = new SelectList(_context.AircraftsData, "AircraftCode", "AircraftCode");
            ViewData["ArrivalAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode");
            ViewData["DepartureAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,FlightNo,ScheduledDeparture,ScheduledArrival,DepartureAirport,ArrivalAirport,Status,AircraftCode,ActualDeparture,ActualArrival")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AircraftCode"] = new SelectList(_context.AircraftsData, "AircraftCode", "AircraftCode", flight.AircraftCode);
            ViewData["ArrivalAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode", flight.ArrivalAirport);
            ViewData["DepartureAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode", flight.DepartureAirport);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AircraftCode"] = new SelectList(_context.AircraftsData, "AircraftCode", "AircraftCode", flight.AircraftCode);
            ViewData["ArrivalAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode", flight.ArrivalAirport);
            ViewData["DepartureAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode", flight.DepartureAirport);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,FlightNo,ScheduledDeparture,ScheduledArrival,DepartureAirport,ArrivalAirport,Status,AircraftCode,ActualDeparture,ActualArrival")] Flight flight)
        {
            if (id != flight.FlightId)
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
                    if (!FlightExists(flight.FlightId))
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
            ViewData["AircraftCode"] = new SelectList(_context.AircraftsData, "AircraftCode", "AircraftCode", flight.AircraftCode);
            ViewData["ArrivalAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode", flight.ArrivalAirport);
            ViewData["DepartureAirport"] = new SelectList(_context.AirportsData, "AirportCode", "AirportCode", flight.DepartureAirport);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.AircraftCodeNavigation)
                .Include(f => f.ArrivalAirportNavigation)
                .Include(f => f.DepartureAirportNavigation)
                .FirstOrDefaultAsync(m => m.FlightId == id);
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
            if (_context.Flights == null)
            {
                return Problem("Entity set 'AirlinesContext.Flights'  is null.");
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
          return (_context.Flights?.Any(e => e.FlightId == id)).GetValueOrDefault();
        }
    }
}

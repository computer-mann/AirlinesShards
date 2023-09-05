
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Tables;
using Infrastructure.Database;

namespace AirlinesWeb.Controllers
{
    public class AircraftsController : Controller
    {
        private readonly AirlinesDbContext _context;

        public AircraftsController(AirlinesDbContext context)
        {
            _context = context;
        }

        // GET: Aircrafts
        public async Task<IActionResult> Index()
        {
            return _context.AircraftsData != null ?
                        View(await _context.VwAircrafts.ToListAsync()) :
                        Problem("Entity set 'AirlinesContext.AircraftsData'  is null.");
        }

        // GET: Aircrafts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AircraftsData == null)
            {
                return NotFound();
            }

            var aircraftsData = await _context.AircraftsData
                .FirstOrDefaultAsync(m => m.AircraftCode == id);
            if (aircraftsData == null)
            {
                return NotFound();
            }

            return View(aircraftsData);
        }

        // GET: Aircrafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aircrafts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftCode,Model,Range")] AircraftsData aircraftsData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aircraftsData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraftsData);
        }

        // GET: Aircrafts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AircraftsData == null)
            {
                return NotFound();
            }

            var aircraftsData = await _context.AircraftsData.FindAsync(id);
            if (aircraftsData == null)
            {
                return NotFound();
            }
            return View(aircraftsData);
        }

        // POST: Aircrafts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AircraftCode,Model,Range")] AircraftsData aircraftsData)
        {
            if (id != aircraftsData.AircraftCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraftsData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftsDataExists(aircraftsData.AircraftCode))
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
            return View(aircraftsData);
        }

        // GET: Aircrafts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AircraftsData == null)
            {
                return NotFound();
            }

            var aircraftsData = await _context.AircraftsData
                .FirstOrDefaultAsync(m => m.AircraftCode == id);
            if (aircraftsData == null)
            {
                return NotFound();
            }

            return View(aircraftsData);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AircraftsData == null)
            {
                return Problem("Entity set 'AirlinesContext.AircraftsData'  is null.");
            }
            var aircraftsData = await _context.AircraftsData.FindAsync(id);
            if (aircraftsData != null)
            {
                _context.AircraftsData.Remove(aircraftsData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftsDataExists(string id)
        {
            return (_context.AircraftsData?.Any(e => e.AircraftCode == id)).GetValueOrDefault();
        }
    }
}

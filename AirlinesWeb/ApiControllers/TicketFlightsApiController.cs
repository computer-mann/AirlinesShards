using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlinesWeb.Models.DbContexts;
using System.Diagnostics;
using AirlinesWeb.Models.Tables;

namespace AirlinesWeb.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketFlightsApiController : ControllerBase
    {
        private readonly AirlinesContext _context;
        private readonly ILogger<TicketFlightsApiController> logger;

        public TicketFlightsApiController(AirlinesContext context,ILogger<TicketFlightsApiController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/TicketFlightsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketFlight>>> GetTicketFlights()
        {
            var watch = Stopwatch.StartNew();
            var counts = await _context.TicketFlights.CountAsync();
            logger.LogInformation("The count tickets request took: {counter}milliseconds", watch.ElapsedMilliseconds);
            logger.LogInformation("Ticket request time {counter}", DateTime.Now);
            return new JsonResult(new {count= counts});
        }

        // GET: api/TicketFlightsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketFlight>> GetTicketFlight(string id)
        {
          if (_context.TicketFlights == null)
          {
              return NotFound();
          }
            var ticketFlight = await _context.TicketFlights.FindAsync(id);

            if (ticketFlight == null)
            {
                return NotFound();
            }

            return ticketFlight;
        }

        // PUT: api/TicketFlightsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketFlight(string id, TicketFlight ticketFlight)
        {
            if (id != ticketFlight.TicketNo)
            {
                return BadRequest();
            }

            _context.Entry(ticketFlight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketFlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TicketFlightsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketFlight>> PostTicketFlight(TicketFlight ticketFlight)
        {
          if (_context.TicketFlights == null)
          {
              return Problem("Entity set 'AirlinesContext.TicketFlights'  is null.");
          }
            _context.TicketFlights.Add(ticketFlight);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketFlightExists(ticketFlight.TicketNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketFlight", new { id = ticketFlight.TicketNo }, ticketFlight);
        }

        // DELETE: api/TicketFlightsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketFlight(string id)
        {
            if (_context.TicketFlights == null)
            {
                return NotFound();
            }
            var ticketFlight = await _context.TicketFlights.FindAsync(id);
            if (ticketFlight == null)
            {
                return NotFound();
            }

            _context.TicketFlights.Remove(ticketFlight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketFlightExists(string id)
        {
            return (_context.TicketFlights?.Any(e => e.TicketNo == id)).GetValueOrDefault();
        }
    }
}

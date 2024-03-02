using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OutputCaching;
using Domain.Tables;
using Infrastructure.Database;
using AirlinesApi;

namespace AirlinesApi.Controllers
{
    [Route("/api/flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirlinesDbContext _context;
        private readonly ILogger<FlightsController> _logger;
        public FlightsController(AirlinesDbContext context,ILogger<FlightsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Flights
        [HttpGet]
        [OutputCache(Duration =100)]
        public async Task<ActionResult<IEnumerable<TicketFlight>>> GetTicketFlights()
        {
            
            var counts = await _context.TicketFlights.CountAsync();
            return new JsonResult(new {count= counts});
        }

        [HttpGet("fare")]
        [OutputCache(Duration =200)]
        public async Task<ActionResult> GetAvailableFareConditions()
        {
            string distinctSeatQuery = "select distinct fare_condition_id from ticket_flights limit 200";
            var res =await _context.Database.SqlQueryRaw<int>(distinctSeatQuery).ToListAsync();  
            return new JsonResult(new {seats= res });
        }

        // GET: api/FlightsApiController/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        // PUT: api/FlightsApiController/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        // POST: api/FlightsApiController
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

        // DELETE: api/FlightsApiController/5
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

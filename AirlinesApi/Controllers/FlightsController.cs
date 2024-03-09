using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OutputCaching;
using AirlinesApi.Database.DbContexts;
using AirlinesApi.Database.Models;
using Microsoft.AspNetCore.Authorization;

namespace AirlinesApi.Controllers
{
    [Route("/api/flights")]
    [ApiController]
    [Authorize]
    public class FlightsController : ControllerBase
    {
        private readonly AirlinesDbContext _context;
        private readonly ILogger<FlightsController> _logger;
        private string _userId => User.Identity.Name;
        public FlightsController(AirlinesDbContext context,ILogger<FlightsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: api/Flights
        [HttpGet]
        [OutputCache(Duration =1251000)]
        public async Task<ActionResult<IEnumerable<TicketFlight>>> GetAllFlightsForAuser(CancellationToken token)
        {
           var flightsForUser = await _context.Bookings.Where(n => n.PassengerId == _userId)
                                .OrderBy(e=>e.BookDate)
                                .Include(e => e.Tickets)
                                .AsNoTracking()
                                .ToListAsync(token);
            _logger.LogInformation("flightsforuser count is {count}", flightsForUser.Count);
            return Ok(flightsForUser);
        }

        [HttpGet("fare")]
        [OutputCache(PolicyName = "IgnoreAuthCache", Duration =112000)]
        [AllowAnonymous]
        public async Task<ActionResult> GetAvailableFareConditions()
        {
            string distinctSeatQuery = "select distinct fare_condition_id from ticket_flights limit 200";
            var res =await _context.Database.SqlQueryRaw<int>("select 1").ToListAsync();  
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

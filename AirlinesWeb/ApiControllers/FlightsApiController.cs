﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlinesWeb.Models.DbContexts;
using System.Diagnostics;
using AirlinesWeb.Models.Tables;
using Microsoft.AspNetCore.OutputCaching;

namespace AirlinesWeb.ApiControllers
{
    [Route("/api/flights")]
    [ApiController]
    public class FlightsApiController : ControllerBase
    {
        private readonly AirlinesContext _context;
        private readonly ILogger<FlightsApiController> logger;
        public const string DistinctSeatQuery = "select distinct fare_conditions from ticket_flights limit 200";
        public FlightsApiController(AirlinesContext context,ILogger<FlightsApiController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/FlightsApiController
        [HttpGet]
        [OutputCache(Duration =100)]
        public async Task<ActionResult<IEnumerable<TicketFlight>>> GetTicketFlights()
        {
            
            var counts = await _context.TicketFlights.CountAsync();
            return new JsonResult(new {count= counts});
        }

        [HttpGet]
        [Route("fare")]
        [OutputCache(Duration =200)]
        public async Task<ActionResult> GetAvailableFareConditions()
        {
            // var result = _context.Database.SqlQueryRaw<List<string>>(DistinctSeatQuery).ToList();
            var res = _context.Database.SqlQueryRaw<string>(DistinctSeatQuery).ToList();
           // var res=_context.TicketFlights.Select(e=>e.FareConditions).Distinct().ToList();
            return new JsonResult(new {seats= res });
        }

        // GET: api/FlightsApiController/5
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

        // PUT: api/FlightsApiController/5
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
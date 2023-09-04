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
using StackExchange.Redis;
using Redis.OM;
using NRedisStack.RedisStackCommands;

namespace AirlinesWeb.ApiControllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsApiController : ControllerBase
    {
        private readonly AirlinesContext _context;
        private readonly ILogger<Ticket> logger;
        private IDatabase _database;
        

        public TicketsApiController(AirlinesContext context,ILogger<Ticket> logger,IConnectionMultiplexer connectionMultiplexer)
        {
            
            _context = context;
            this.logger = logger;
            _database = connectionMultiplexer.GetDatabase(0);
        }

        // GET: api/Tickets
        [HttpGet]
        [ResponseCache(VaryByHeader ="User-Agent",Duration =100,Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetTickets()
        {
            
            var watch = Stopwatch.StartNew();
            var counts = await _context.Tickets.AsNoTracking().CountAsync();
            logger.LogInformation("The count tickets request took: {counter}ms", watch.ElapsedMilliseconds);
            logger.LogInformation("Ticket request time {counter}", DateTime.Now);
            return new JsonResult(new {ticketCount=counts});
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(string id)
        {
            
            logger.LogInformation("RedisCon: {id}",id);
          if (_context.Tickets == null)
          {           
            return NotFound();
          }
            var ticket = await _context.Tickets.FindAsync(id);
            
            if (ticket == null)
            {
                var obj = new
                {
                    art = "ariana111",
                    song = "pov"
                };
                var date = DateTime.UtcNow.ToString();
                if (await _database.JSON().SetAsync($"{date}", "$", obj, When.NotExists))
                {
                    await _database.KeyExpireAsync(date,TimeSpan.FromSeconds(40));
                    logger.LogInformation("success");
                }
                else { logger.LogWarning("cache failed"); }
                return NotFound(new {});
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(string id, Ticket ticket)
        {
            if (id != ticket.TicketNo)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'AirlinesContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketExists(ticket.TicketNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicket", new { id = ticket.TicketNo }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(string id)
        {
            return (_context.Tickets?.Any(e => e.TicketNo == id)).GetValueOrDefault();
        }
    }
}

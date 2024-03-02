using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using StackExchange.Redis;
using Redis.OM;
using NRedisStack.RedisStackCommands;
using Domain.Tables;
using Infrastructure.Database;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Identity;
using AirlinesApi;


namespace AirlinesApi.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AirlinesDbContext _context;
        private readonly UserManager<Traveller> userManager;
        private readonly ILogger<TicketsController> logger;
        private IDatabase _redisdatabase;


        public TicketsController(AirlinesDbContext context, ILogger<TicketsController> logger, IConnectionMultiplexer connectionMultiplexer, TravellerDbContext TravellerDbContext, UserManager<Traveller> userManager)
        {
            _context = context;
            this.logger = logger;
            _redisdatabase = connectionMultiplexer.GetDatabase(0);
            this.userManager = userManager;
        }

        // GET: api/Tickets/count
        [HttpGet]
       // [OutputCache(Duration =100)]
        [Route("count")]
        public async Task<IActionResult> GetTicketsCount()
        {
            
            
            var counts = await _context.Tickets.AsNoTracking().Where(e=>e.PassengerId != null ).CountAsync();
            return new JsonResult(new {ticketCount=counts});
        }
        // GET: api/Tickets/count
        [HttpGet]
        [OutputCache(Duration = 100)]
        public async Task<IActionResult> GetTickets()
        {

            var tickets = await _context.Tickets.AsNoTracking().OrderBy(e=>e.PassengerId).Take(100).ToListAsync();
            return Ok(tickets);
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
                if (await _redisdatabase.JSON().SetAsync($"{date}", "$", obj, When.NotExists))
                {
                    await _redisdatabase.KeyExpireAsync(date,TimeSpan.FromSeconds(40));
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

using AirlinesApi.Database.DbContexts;
using AirlinesApi.Database.Models;
using AirlinesApi.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AirlinesApi.Controllers
{
    [ApiController]
    [Route("/api/booking")]
    [Authorize]
    public class BookingController:ControllerBase
    {
        private readonly AirlinesDbContext _airlinesDbContext;
        private string _userId=>User.Identity.Name;
        public BookingController(AirlinesDbContext airlinesDbContext)
        {
            _airlinesDbContext = airlinesDbContext;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBookingsForAUser()
        {
            var bookings =await _airlinesDbContext.Tickets.Include(b=>b.BookRefNavigation).Where(e => e.PassengerId == _userId).ToListAsync();
            return Ok(bookings);
        }
       
    }
}

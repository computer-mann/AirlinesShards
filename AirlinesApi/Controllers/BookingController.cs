using AirlinesApi.Database.DbContexts;
using AirlinesApi.Database.Models;
using AirlinesApi.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
       // [OutputCache(PolicyName = "IgnoreAuthCache")]
        public async Task<ActionResult> GetAllBookingsForAUser([FromQuery]KeyPaging keyPaging)
        {
            
            var bookingsQuery = _airlinesDbContext.Bookings.OrderBy(e => e.BookDate).Include(b => b.Tickets)
                .Where(e => e.PassengerId == _userId);
            if (!string.IsNullOrEmpty(keyPaging.Offset))
            {
                bookingsQuery=bookingsQuery.Where(e=>e.BookRef.CompareTo(keyPaging.Offset)>0);
            }
            var bookingsForUser=await bookingsQuery.Take(keyPaging.Limit).ToListAsync();
            if(!bookingsForUser.Any())
            {
                return NotFound();
            }
            GetBookingsViewModel viewModel = new GetBookingsViewModel()
            {
                Previous = bookingsForUser.FirstOrDefault().BookRef,
                Next = bookingsForUser.LastOrDefault().BookRef,
                Bookings = bookingsForUser.AsEnumerable()
                .Select(best => new BookingsDto(best.BookRef, best.BookDate, best.TotalAmount))
            };

            return Ok(viewModel);
        }
       
    }
}

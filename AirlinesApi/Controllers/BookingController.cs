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
        [Produces<GetBookingsViewModel>]
        public async Task<ActionResult> GetAllBookingsForAUser([FromQuery]PaginationVm keyPaging)
        {
            if(!string.IsNullOrEmpty(keyPaging.Next) && !string.IsNullOrEmpty(keyPaging.Previous))
            {
                return BadRequest(new ProblemDetails()
                {
                    Title="Bad Request",
                    Detail="Please provide only one navigation query parameter"
                });
            }
            
            var bookingsQuery = _airlinesDbContext.Tickets.Include(b => b.BookRefNavigation).OrderBy(e => e.BookRefNavigation.BookRef)
                .Where(e => e.PassengerId == _userId);
            if (!string.IsNullOrEmpty(keyPaging.Next))
            {
                bookingsQuery=bookingsQuery.Where(e=>e.BookRef.CompareTo(keyPaging.Next)>0);
            }
            if (!string.IsNullOrEmpty(keyPaging.Previous))
            {
                bookingsQuery = bookingsQuery.Where(e => e.BookRef.CompareTo(keyPaging.Previous) < 0);
            }
            var bookingsForUser=await bookingsQuery.Take(keyPaging.Limit).ToListAsync();
            if(!bookingsForUser.Any())
            {
                return NotFound(new ProblemDetails()
                {
                    Detail="No item found with the given query params"
                });
            }
            GetBookingsViewModel viewModel = new GetBookingsViewModel()
            {
                Previous = bookingsForUser.FirstOrDefault()!.BookRef,
                Next = bookingsForUser.LastOrDefault()!.BookRef,
                Bookings = bookingsForUser.AsEnumerable()
                .Select(best => new BookingsDto(best.BookRef, best.BookRefNavigation.BookDate, best.BookRefNavigation.TotalAmount))
                .OrderByDescending(d=>d.BookDate)
            };

            return Ok(viewModel);
        }
       
    }
}

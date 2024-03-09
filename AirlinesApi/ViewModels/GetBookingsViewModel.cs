namespace AirlinesApi.ViewModels
{
    public class GetBookingsViewModel
    {
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<BookingsDto> Bookings { get; set; }=Enumerable.Empty<BookingsDto>();
    }
    public record BookingsDto(string BookRef,DateTime BookDate,decimal TotalAmount);
}

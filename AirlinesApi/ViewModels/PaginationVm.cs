using System.ComponentModel.DataAnnotations;

namespace AirlinesApi.ViewModels
{
    public class PaginationVm
    {
        public int Limit { get; set; } = 20;
        //if the offset is null or empty return from the first element in db
        
        public string? Next { get; set; }
        public string? Previous { get; set; }
    }
}

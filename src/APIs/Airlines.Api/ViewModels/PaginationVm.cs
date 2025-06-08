using System.ComponentModel.DataAnnotations;

namespace AirlinesApi.ViewModels
{
    public class PaginationVm
    {
        public int Limit { get; set; } = 20;
        
        //next and previous cant both not be null
        public string? Next { get; set; }
        public string? Previous { get; set; }
    }
}

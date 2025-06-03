using System.Text.Json.Serialization;

namespace AirlinesApi.ViewModels
{

    public class ArrivalAirpotViewmodel
    {
        [JsonPropertyName("en")]
        public string AirportNameEnglish { get; set; }
        public string ru { get; set; }
    }
}

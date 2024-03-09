namespace AirlinesApi.ViewModels
{
    public class KeyPaging
    {
        public int Limit { get; set; } = 20;
        //if the offset is null or empty return from the first element in db
        public string Offset { get; set; }
    }
}

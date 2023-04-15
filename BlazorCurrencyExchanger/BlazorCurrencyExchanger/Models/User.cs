namespace BlazorCurrencyExchanger.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string provider { get; set; }
        public bool confirmed { get; set; }
        public bool blocked { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}

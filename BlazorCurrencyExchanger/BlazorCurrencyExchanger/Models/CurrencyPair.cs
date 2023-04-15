using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorCurrencyExchanger.Models
{
    public class CurrencyPair
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public decimal ExchangeRate { get; set; }

        [Required]
        public int? userId { get; set; }

        [Required]
        public string? currencyA { get; set; }
        [Required]
        public string? currencyB { get; set; }
    }
}

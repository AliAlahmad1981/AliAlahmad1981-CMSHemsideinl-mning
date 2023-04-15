using System.ComponentModel.DataAnnotations;

namespace BlazorCurrencyExchanger.Models
{
    public class UserLogin
    {
        [Required]
        public string? identifier { get; set; }
        [Required]
        public string? password { get; set; }
    }
}

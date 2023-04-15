using System.ComponentModel.DataAnnotations;

namespace BlazorCurrencyExchanger.Models
{
    public class UserRegisterationData
    {
        [Required]
        public string? email { get; set; }
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoCurrencyExchanger.Services;

namespace UmbracoCurrencyExchanger.Controllers
{
    public class ConvertModel
    {
        public string from { get; set; }
        public string to { get; set; }
    }
    public class CurrencyController: UmbracoApiController
    {
		private CurrencyService _currencyService;
		public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
		}
        public async Task<decimal> Convert(ConvertModel data)
        {
            var val = await _currencyService.GetExchangeRateAsync(data.from, data.to);
            return val;
        }      
    }
}

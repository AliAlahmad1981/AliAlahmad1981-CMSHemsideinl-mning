using BlazorCurrencyExchanger.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorCurrencyExchanger.Services
{
    public class CurrencyService : ApiService
    {
        private const string APIKEY = "apikey=f8anUVQRcqa5EME30o43EbmV4d1hjiH8tsdstjjA";
        private readonly List<Currency> currencyList = new List<Currency> ();
        public CurrencyService(IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory, "CurrencyClient")
        {
        }
        public async ValueTask<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            if (currencyList.Count == 0)
            {
                JsonElement result = await ExecuteAsync<JsonElement>(async c =>
                    await c.GetAsync("currencies?" + APIKEY));
                foreach (JsonProperty currencyElement in result.GetProperty("data").EnumerateObject())
                {
                    currencyList.Add(new Currency()
                    {
                        Name = currencyElement.Value.GetProperty("name").GetString(),
                        Code = currencyElement.Value.GetProperty("code").GetString(),
                        Symbol = currencyElement.Value.GetProperty("symbol").GetString()
                    });
                }
            }
            return currencyList;
        }

        public async ValueTask<decimal> GetExchangeRateAsync(string fromCur, string toCur)
        {
            JsonElement result = await ExecuteAsync<JsonElement>(async c =>
               await c.GetAsync("latest?" + APIKEY + "&base_currency=" + fromCur + "&currencies=" + toCur));
            return result.GetProperty("data").GetProperty(toCur).GetDecimal();
        }
    }
}

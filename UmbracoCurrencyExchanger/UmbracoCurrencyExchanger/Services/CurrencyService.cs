using System.Text.Json;
using System.Text.Json.Nodes;

namespace UmbracoCurrencyExchanger.Services
{

	public class Currency
	{
		public string? Symbol { get; init; }
		public string? Name { get; init; }
		public string? Code { get; init; }
	}
	public class CurrencyService
	{
		private const string APIKEY = "apikey=f8anUVQRcqa5EME30o43EbmV4d1hjiH8tsdstjjA";
		private readonly HttpClient httpClient;
		private readonly List<Currency> currencyList;
		public CurrencyService()
		{
			currencyList = new List<Currency>();
			httpClient = new HttpClient()
			{
				BaseAddress = new Uri("https://api.freecurrencyapi.com/v1/")
			};
		}

		public async ValueTask<IEnumerable<Currency>> GetCurrenciesAsync()
		{
			if (currencyList.Count == 0)
			{
				JsonElement result = await httpClient.GetFromJsonAsync<JsonElement>("currencies?" + APIKEY);
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

		public async ValueTask<decimal> GetExchangeRateAsync(string fromCur , string toCur)
		{
            //https://api.freecurrencyapi.com/v1/latest
            JsonElement result = await httpClient
				.GetFromJsonAsync<JsonElement>
				("latest?" + APIKEY+ "&base_currency=" + fromCur + "&currencies=" + toCur);
			return result.GetProperty("data").GetProperty(toCur).GetDecimal();
        }
    }
}

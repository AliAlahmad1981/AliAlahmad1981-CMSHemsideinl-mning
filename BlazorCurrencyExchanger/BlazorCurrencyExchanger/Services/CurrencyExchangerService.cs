using BlazorCurrencyExchanger.Models;
using System.Net.Http.Json;
using System.Net.Security;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCurrencyExchanger.Services
{
    public class CurrencyExchangerService : ApiService
    {
        private string? token = string.Empty;
        public event EventHandler? AuthenticationChanged;
        public User? User { get; private set; }
        public CurrencyExchangerService(IHttpClientFactory httpClientFactory) : 
            base(httpClientFactory , "CurrencyExchangerClient")
        {
        }
        public async ValueTask<bool> LoginAsync(UserLogin data)
        {
            JsonElement result = await ExecuteAsync<JsonElement>(async (c) => await c.PostAsJsonAsync("auth/local", data));
            token = result.GetProperty("jwt").GetString();
            User = result.GetProperty("user").Deserialize<User>();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            AuthenticationChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public ValueTask LogoutAsync()
        {
            token = string.Empty;
            User = null;
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            AuthenticationChanged?.Invoke(this, EventArgs.Empty);
            return ValueTask.CompletedTask;
        }

        public async Task<bool> RegisterAsync(UserRegisterationData data)
        {
          return   await ExecuteAsync<bool>(async (c) => await c.PostAsJsonAsync("auth/local/register", data));
        }
         
        public async  ValueTask<List<CurrencyPair>> GetUserList()
        {
            List<CurrencyPair> currencyPairs = new List<CurrencyPair>();
            JsonElement result = await ExecuteAsync<JsonElement>(async (c) => 
            {
                return await c.GetAsync("currencypairs");
            });
            foreach (var item in result.GetProperty("data").EnumerateArray())
            {
                CurrencyPair pair = item.GetProperty("attributes").Deserialize<CurrencyPair>();
                pair.Id = item.GetProperty("id").GetInt32();
                currencyPairs.Add(pair);
            }
            return currencyPairs;
        }

        public async  ValueTask AddToListAsync(string from , string to)
        {
          JsonElement element =  await ExecuteAsync<JsonElement>(async (c) => {             
             return  await c.PostAsJsonAsync("currencypairs" , new { data =
                     new CurrencyPair()
                    {
                        userId = User.id,
                        currencyA = from,
                        currencyB = to
                    } });
          }) ;
        }

        public async ValueTask RemoveFromListAsync(int id)
        {
            JsonElement element = await ExecuteAsync<JsonElement>(async (c) => {
                return await c.DeleteAsync($"currencypairs/{id}");
            });
        }

    }
}

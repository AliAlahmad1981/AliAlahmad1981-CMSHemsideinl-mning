using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCurrencyExchanger.Services
{
    public abstract class ApiService
    {
        protected readonly HttpClient httpClient;
        public ApiService(IHttpClientFactory httpClientFactory ,string name)
        {
           httpClient= httpClientFactory.CreateClient(name);
        }

        protected async ValueTask<T> ExecuteAsync<T>(Func<HttpClient, ValueTask<HttpResponseMessage>> request)
        {
            try
            {
                HttpResponseMessage response = await request(httpClient);
                var result = await response.Content.ReadFromJsonAsync<T>();
                if (response.IsSuccessStatusCode && result != null)
                {
                    return result;
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(error);
                }
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }
}

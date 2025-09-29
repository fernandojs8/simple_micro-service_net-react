using Domain.simple_microservice.Settings;
using Domain_simple_microservice.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Service_simple_microservice.Interfaces;

namespace Service_simple_microservice.Services
{
    public class SalesService : ISalesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public SalesService(HttpClient httpClient, IOptions<AplicationSettings> settings)
        {
            this._httpClient = httpClient;
            _endpoint = settings.Value.FakeStoreProductsEndPoint;
        }
        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
            try
            {
                // Setup HTTP request
                using var response = await _httpClient.GetAsync(this._endpoint, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();

                // Perform HTTP request
                var resultJson = await response.Content.ReadAsStringAsync();

                // Transform the data
                var products = JsonConvert.DeserializeObject<List<FakeStoreProduct>>(resultJson);

                var sales = new List<Sale>();

                foreach (var item in products)
                {
                    var sale = new Sale()
                    {
                        Category = item.category,
                        Id = item.id,
                        Price = item.price,
                        Title = item.title,
                        Date = DateTime.UtcNow.AddDays(-item.id)
                    };

                    sales.Add(sale);
                }

                // Return the transformed data

                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

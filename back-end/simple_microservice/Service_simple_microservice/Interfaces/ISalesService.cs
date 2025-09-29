using Domain_simple_microservice.Models;

namespace Service_simple_microservice.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<Sale>?> GetSalesAsync();
    }
}

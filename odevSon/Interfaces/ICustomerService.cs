using odevSon.Models;

namespace odevSon.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
    }
}

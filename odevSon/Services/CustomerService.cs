using Microsoft.EntityFrameworkCore;
using odevSon.Interfaces;
using odevSon.Models;

namespace odevSon.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync() => await _customerRepository.GetAllAsync();

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        

    }
}

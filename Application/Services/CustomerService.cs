using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var res =await _customerRepository.GetAllAsync();
            return res;
        }

        public Task<Customer> GetByIdAsync(Guid id)
        {
            return _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            return await _customerRepository.CreateAsync(customer);
        }

        public Task UpdateAsync(Guid id, Customer customer)
        {
            return _customerRepository.UpdateAsync(id, customer);
        }

        public Task DeleteAsync(Guid id)
        {
            return _customerRepository.DeleteAsync(id);
        }
    }
}
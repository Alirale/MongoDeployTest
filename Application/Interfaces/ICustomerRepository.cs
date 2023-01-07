using Core.Entities;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(string id, Customer customer);
        Task DeleteAsync(string id);
    }
}
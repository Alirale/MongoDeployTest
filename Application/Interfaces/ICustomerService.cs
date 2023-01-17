using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(Guid id, Customer customer);
        Task DeleteAsync(Guid id);
    }
}
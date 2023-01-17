using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;
        public CustomerRepository(DbConfiguration settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Customer>(settings.CollectionName);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var list = await _collection.Find(x=>true).ToListAsync();
            return list;
        }
        public Task<Customer> GetByIdAsync(Guid id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _collection.InsertOneAsync(customer);
            return customer;
        }
        public Task UpdateAsync(Guid id, Customer customer)
        {
            return _collection.ReplaceOneAsync(c => c.Id == id, customer);
        }
        public Task DeleteAsync(Guid id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
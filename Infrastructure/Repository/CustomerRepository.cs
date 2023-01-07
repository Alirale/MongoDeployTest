using Application.Interfaces;
using Core.Entities;
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

        public Task<List<Customer>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }
        public Task<Customer> GetByIdAsync(string id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _collection.InsertOneAsync(customer).ConfigureAwait(false);
            return customer;
        }
        public Task UpdateAsync(string id, Customer customer)
        {
            return _collection.ReplaceOneAsync(c => c.Id == id, customer);
        }
        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
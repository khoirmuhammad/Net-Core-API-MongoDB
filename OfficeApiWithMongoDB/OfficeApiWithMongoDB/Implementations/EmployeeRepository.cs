using MongoDB.Driver;
using OfficeApiWithMongoDB.Abstractions;
using OfficeApiWithMongoDB.Models;

namespace OfficeApiWithMongoDB.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Employee> _employeeCollection;
        public EmployeeRepository() 
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = _mongoClient.GetDatabase("OfficeDB");
            _employeeCollection = _mongoDatabase.GetCollection<Employee>("Employees");
        }

        public async Task Delete(string id)
        {
            await _employeeCollection.DeleteOneAsync(d => d.Id.Equals(id));
        }

        public async Task<Employee> Get(string id)
        {
            var data = await _employeeCollection.Find(f => f.Id.Equals(id)).FirstOrDefaultAsync();
            return data;
        }

        public async Task Save(Employee employee)
        {
            var data = await this.Get(employee.Id);

            if (data == null) 
            {
                await _employeeCollection.InsertOneAsync(employee);
            }
            else
            {
                await _employeeCollection.ReplaceOneAsync(r => r.Id.Equals(employee.Id), employee);
            }

        }

        public async Task<IEnumerable<Employee>> Select()
        {
            var data = await _employeeCollection.Find(FilterDefinition<Employee>.Empty).ToListAsync();
            return data;
        }
    }
}

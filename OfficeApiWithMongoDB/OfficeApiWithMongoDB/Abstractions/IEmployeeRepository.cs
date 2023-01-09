using OfficeApiWithMongoDB.Models;

namespace OfficeApiWithMongoDB.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(string id);
        Task<IEnumerable<Employee>> Select();
        Task Save(Employee employee);
        Task Delete(string id);

    }
}

using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    void Update(Employee employee);
    void Delete(Employee employee);
}

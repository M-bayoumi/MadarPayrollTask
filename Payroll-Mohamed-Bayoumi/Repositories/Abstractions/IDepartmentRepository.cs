using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task AddAsync(Department department);
    void Update(Department department);
    void Delete(Department department);
}

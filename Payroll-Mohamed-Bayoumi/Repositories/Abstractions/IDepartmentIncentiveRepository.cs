using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface IDepartmentIncentiveRepository
{
    Task<IEnumerable<DepartmentIncentive>> GetAllAsync();
    Task<DepartmentIncentive?> GetByIdAsync(int id);
    Task AddAsync(DepartmentIncentive departmentIncentive);
    void Update(DepartmentIncentive departmentIncentive);
    void Delete(DepartmentIncentive departmentIncentive);
    bool IsDepartmentExist(DepartmentIncentive departmentIncentive);
}

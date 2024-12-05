using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface ISalaryRepository
{
    Task<IEnumerable<Salary>> GetAllAsync();
    Task<Salary?> GetByIdAsync(int id);
    Task AddAsync(Salary salary);
    void Update(Salary salary);
    void Delete(Salary salary);
    bool IsGradeExist(Salary salary);
}

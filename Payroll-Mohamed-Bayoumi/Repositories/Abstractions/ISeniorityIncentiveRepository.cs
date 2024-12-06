using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface ISeniorityIncentiveRepository
{
    Task<IEnumerable<SeniorityIncentive>> GetAllAsync();
    Task<SeniorityIncentive?> GetByIdAsync(int id);
    Task<SeniorityIncentive?> GetByYearsOfServiceAsync(int yearsOfService);
    Task AddAsync(SeniorityIncentive seniorityIncentive);
    void Update(SeniorityIncentive seniorityIncentive);
    void Delete(SeniorityIncentive seniorityIncentive);
    bool IsYearsOfServiceExist(SeniorityIncentive seniorityIncentive);
}

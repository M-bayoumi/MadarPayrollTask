using Payroll_Mohamed_Bayoumi.Enums;
using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface IAbsencePenaltyRepository
{
    Task<IEnumerable<AbsencePenalty>> GetAllAsync();
    Task<AbsencePenalty?> GetByIdAsync(int id);
    Task<AbsencePenalty?> GetByAbsenceDaysAsync(AbsenceDays absenceDays);
    Task AddAsync(AbsencePenalty absencePenalty);
    void Update(AbsencePenalty absencePenalty);
    void Delete(AbsencePenalty absencePenalty);
    bool IsAbsenceDaysExist(AbsencePenalty absencePenalty);

}

using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

public interface IAbsencePenaltyRepository
{
    Task<IEnumerable<AbsencePenalty>> GetAllAsync();
    Task<AbsencePenalty?> GetByIdAsync(int id);
    Task AddAsync(AbsencePenalty absencePenalty);
    void Update(AbsencePenalty absencePenalty);
    void Delete(AbsencePenalty absencePenalty);
    bool IsAbsenceDaysExist(AbsencePenalty absencePenalty);

}

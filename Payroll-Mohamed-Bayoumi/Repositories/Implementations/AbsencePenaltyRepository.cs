using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Enums;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class AbsencePenaltyRepository : IAbsencePenaltyRepository
{
    private readonly ApplicationDbContext _context;

    public AbsencePenaltyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AbsencePenalty>> GetAllAsync()
    {
        return await _context
            .AbsencePenalties
            .ToListAsync();
    }

    public async Task<AbsencePenalty?> GetByIdAsync(int id)
    {
        return await _context
            .AbsencePenalties
            .FindAsync(id);
    }
    public async Task<AbsencePenalty?> GetByAbsenceDaysAsync(AbsenceDays absenceDays)
    {
        return await _context
            .AbsencePenalties
            .FirstOrDefaultAsync(x => x.AbsenceDays == absenceDays);
    }

    public async Task AddAsync(AbsencePenalty absencePenalty)
    {
        await _context
            .AbsencePenalties
            .AddAsync(absencePenalty);
    }

    public void Update(AbsencePenalty absencePenalty)
    {
        _context
            .AbsencePenalties
            .Update(absencePenalty);
    }

    public void Delete(AbsencePenalty absencePenalty)
    {
        _context
            .AbsencePenalties
            .Remove(absencePenalty);
    }

    public bool IsAbsenceDaysExist(AbsencePenalty absencePenalty)
    {
        return _context
            .AbsencePenalties
            .Any(x => x.AbsenceDays == absencePenalty.AbsenceDays && x.Id != absencePenalty.Id);
    }
}

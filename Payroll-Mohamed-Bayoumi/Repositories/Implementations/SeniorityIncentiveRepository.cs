using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class SeniorityIncentiveRepository : ISeniorityIncentiveRepository
{
    private readonly ApplicationDbContext _context;

    public SeniorityIncentiveRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SeniorityIncentive>> GetAllAsync()
    {
        return await _context
            .SeniorityIncentives
            .ToListAsync();
    }

    public async Task<SeniorityIncentive?> GetByIdAsync(int id)
    {
        return await _context
            .SeniorityIncentives
            .FindAsync(id);
    }
    public async Task<SeniorityIncentive?> GetByYearsOfServiceAsync(int yearsOfService)
    {
        return await _context
            .SeniorityIncentives
            .OrderByDescending(x => x.YearsOfService)
            .FirstOrDefaultAsync(x => yearsOfService >= x.YearsOfService);
    }


    public async Task AddAsync(SeniorityIncentive seniorityIncentive)
    {
        await _context
            .SeniorityIncentives
            .AddAsync(seniorityIncentive);
    }

    public void Update(SeniorityIncentive seniorityIncentive)
    {
        _context
            .SeniorityIncentives
            .Update(seniorityIncentive);
    }
    public void Delete(SeniorityIncentive seniorityIncentive)
    {
        _context
            .SeniorityIncentives
            .Remove(seniorityIncentive);
    }

    public bool IsYearsOfServiceExist(SeniorityIncentive seniorityIncentive)
    {
        return _context
            .SeniorityIncentives
            .Any(x => x.YearsOfService == seniorityIncentive.YearsOfService && x.Id != seniorityIncentive.Id);
    }
}

using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class DepartmentIncentiveRepository : IDepartmentIncentiveRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentIncentiveRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DepartmentIncentive>> GetAllAsync()
    {
        return await _context
            .DepartmentIncentives
            .Include(x => x.Department)
            .ToListAsync();
    }

    public async Task<DepartmentIncentive?> GetByIdAsync(int id)
    {
        return await _context
            .DepartmentIncentives
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<DepartmentIncentive?> GetByDepartmentIdAsync(int departmentId)
    {
        return await _context
            .DepartmentIncentives
            .FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
    }


    public async Task AddAsync(DepartmentIncentive departmentIncentive)
    {
        await _context
            .DepartmentIncentives
            .AddAsync(departmentIncentive);
    }

    public void Update(DepartmentIncentive departmentIncentive)
    {
        _context
            .DepartmentIncentives
            .Update(departmentIncentive);
    }
    public void Delete(DepartmentIncentive departmentIncentive)
    {
        _context
            .DepartmentIncentives
            .Remove(departmentIncentive);
    }
    public bool IsDepartmentExist(DepartmentIncentive departmentIncentive)
    {
        return _context
            .DepartmentIncentives
            .Any(x => x.DepartmentId == departmentIncentive.DepartmentId && x.Id != departmentIncentive.Id);
    }

}

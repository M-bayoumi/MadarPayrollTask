using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Enums;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class SalaryRepository : ISalaryRepository
{
    private readonly ApplicationDbContext _context;

    public SalaryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Salary>> GetAllAsync()
    {
        return await _context
            .Salaries
            .ToListAsync();
    }

    public async Task<Salary?> GetByIdAsync(int id)
    {
        return await _context
            .Salaries
            .FindAsync(id);
    }

    public async Task AddAsync(Salary salary)
    {
        await _context
            .Salaries
            .AddAsync(salary);
    }

    public void Update(Salary salary)
    {
        _context
            .Salaries
            .Update(salary);
    }

    public void Delete(Salary salary)
    {
        _context
            .Salaries
            .Remove(salary);
    }

    public bool IsGradeExist(JobGrade jobGrade)
    {
        return _context.Salaries.Any(x => x.JobGrade == jobGrade);
    }
}

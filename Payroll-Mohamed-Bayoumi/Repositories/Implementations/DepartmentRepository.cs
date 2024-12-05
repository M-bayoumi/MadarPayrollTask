using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context
            .Departments
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context
            .Departments
            .FindAsync(id);
    }

    public async Task AddAsync(Department department)
    {
        await _context
            .Departments
            .AddAsync(department);
    }

    public void Update(Department department)
    {
        _context
            .Departments
            .Update(department);
    }

    public void Delete(Department department)
    {
        _context
            .Departments
            .Remove(department);
    }
}

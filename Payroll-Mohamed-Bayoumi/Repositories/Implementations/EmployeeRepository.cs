using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context
            .Employees
            .Include(x => x.Department)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
    {
        return await _context
            .Employees
            .Where(x => x.DepartmentId == departmentId)
            .Include(x => x.Department)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context
            .Employees
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context
            .Employees
            .AddAsync(employee);
    }

    public void Update(Employee employee)
    {
        _context
            .Employees
            .Update(employee);
    }

    public void Delete(Employee employee)
    {
        _context
            .Employees
            .Remove(employee);
    }
}

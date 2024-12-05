using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IEmployeeRepository EmployeeRepository { get; private set; }
    public IDepartmentRepository DepartmentRepository { get; private set; }

    public UnitOfWork(
        ApplicationDbContext context,
        IEmployeeRepository employeeRepo,
        IDepartmentRepository departmentRepo)
    {
        _context = context;
        EmployeeRepository = employeeRepo;
        DepartmentRepository = departmentRepo;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

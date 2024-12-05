using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IDepartmentRepository DepartmentRepository { get; private set; }
    public IEmployeeRepository EmployeeRepository { get; private set; }
    public ISalaryRepository SalaryRepository { get; private set; }

    public UnitOfWork(
        ApplicationDbContext context,
        IDepartmentRepository departmentRepo,
        IEmployeeRepository employeeRepo,
        ISalaryRepository salaryRepo)
    {
        _context = context;
        DepartmentRepository = departmentRepo;
        EmployeeRepository = employeeRepo;
        SalaryRepository = salaryRepo;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

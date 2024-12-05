using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.UnitOfWork;

public interface IUnitOfWork
{
    IEmployeeRepository EmployeeRepository { get; }
    IDepartmentRepository DepartmentRepository { get; }
    Task<int> CompleteAsync();
}

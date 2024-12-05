using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.UnitOfWork;

public interface IUnitOfWork
{
    IDepartmentRepository DepartmentRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    ISalaryRepository SalaryRepository { get; }
    Task<int> CompleteAsync();
}

using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.UnitOfWork;

public interface IUnitOfWork
{
    IDepartmentRepository DepartmentRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    ISalaryRepository SalaryRepository { get; }
    IDepartmentIncentiveRepository DepartmentIncentiveRepository { get; }
    ISeniorityIncentiveRepository SeniorityIncentiveRepository { get; }
    Task<int> CompleteAsync();
}

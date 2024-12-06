using Payroll_Mohamed_Bayoumi.Enums;

namespace Payroll_Mohamed_Bayoumi.Models;

public class SalaryReportViewModel
{
    public string EmployeeName { get; set; }
    public double BasicSalary { get; set; }
    public double DepartmentIncentive { get; set; }
    public double SeniorityIncentive { get; set; }
    public AbsenceDays AbsenceDays { get; set; }
    public double PenaltyPercentage { get; set; }
    public bool IsBonus { get; set; }
    public double NetSalary { get; set; }
    public Month Month { get; set; }
    public int Year { get; set; }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll_Mohamed_Bayoumi.Enums;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

public class ReportsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Employees(int? departmentId)
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
        ViewBag.Departments = new SelectList(departments, "Id", "Name");

        IEnumerable<Employee> employees;
        if (departmentId.HasValue)
            employees = await _unitOfWork.EmployeeRepository.GetByDepartmentIdAsync(departmentId.Value);
        else
            employees = await _unitOfWork.EmployeeRepository.GetAllAsync();

        return View(employees);
    }



    public async Task<IActionResult> Attendance(int? employeeId)
    {
        var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
        ViewBag.Employees = new SelectList(employees, "Id", "Name");

        IEnumerable<AttendanceRecord> attendanceRecords;
        if (employeeId.HasValue)
            attendanceRecords = await _unitOfWork.AttendanceRecordRepository.GetByEmployeeIdAsync(employeeId.Value);
        else
            attendanceRecords = await _unitOfWork.AttendanceRecordRepository.GetAllAsync();

        return View(attendanceRecords);
    }

    public async Task<IActionResult> Salaries(int? employeeId)
    {
        var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
        ViewBag.Employees = new SelectList(employees, "Id", "Name");

        if (employeeId.HasValue)
        {
            return await GetEmployeeSalaryReport(employeeId.Value);
        }
        else
        {
            return await GetAllEmployeeSalaryReports(employees);
        }
    }

    private async Task<IActionResult> GetEmployeeSalaryReport(int employeeId)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
        {
            ModelState.AddModelError("", "Employee not found.");
            return View();
        }

        var salaryReport = await CalculateSalaryReport(employee);

        if (!salaryReport.Any())
        {
            ModelState.AddModelError("", "No salary records found for this employee.");
            return View();
        }

        return View(salaryReport);
    }

    private async Task<List<SalaryReportViewModel>> CalculateSalaryReport(Employee employee)
    {
        var salary = await _unitOfWork.SalaryRepository.GetByJobGradeAsync(employee.JobGrade);
        var departmentIncentives = await _unitOfWork.DepartmentIncentiveRepository.GetByDepartmentIdAsync(employee.DepartmentId);
        var seniorityIncentives = await _unitOfWork.SeniorityIncentiveRepository.GetByYearsOfServiceAsync(DateTime.Now.Year - employee.HiringDate.Year);
        var attendanceRecords = await _unitOfWork.AttendanceRecordRepository.GetByEmployeeIdAsync(employee.Id);

        var salaryReports = new List<SalaryReportViewModel>();

        foreach (var attendanceRecord in attendanceRecords)
        {
            var salaryReport = new SalaryReportViewModel
            {
                EmployeeName = employee.Name,
                Month = attendanceRecord.Month,
                Year = attendanceRecord.Year
            };

            await CalculateSalaryDetails(salaryReport, salary, departmentIncentives, seniorityIncentives, attendanceRecord);

            salaryReports.Add(salaryReport);
        }

        return salaryReports;
    }

    private async Task CalculateSalaryDetails(SalaryReportViewModel salaryReport, Salary salary,
        DepartmentIncentive departmentIncentives, SeniorityIncentive seniorityIncentives, AttendanceRecord attendanceRecord)
    {
        if (salary != null)
        {
            salaryReport.BasicSalary = salary.Amount;
            salaryReport.NetSalary = salary.Amount;

            ApplyIncentives(salaryReport, departmentIncentives, seniorityIncentives);

            await ApplyAbsencePenalty(salaryReport, attendanceRecord.AbsenceDays, salary.Amount);
        }
    }

    private void ApplyIncentives(SalaryReportViewModel salaryReport, DepartmentIncentive departmentIncentives, SeniorityIncentive seniorityIncentives)
    {
        if (departmentIncentives != null)
        {
            salaryReport.DepartmentIncentive = departmentIncentives.IncentivePercentage;
            salaryReport.NetSalary += (departmentIncentives.IncentivePercentage / 100) * salaryReport.BasicSalary;
        }

        if (seniorityIncentives != null)
        {
            salaryReport.SeniorityIncentive = seniorityIncentives.IncentivePercentage;
            salaryReport.NetSalary += (seniorityIncentives.IncentivePercentage / 100) * salaryReport.BasicSalary;
        }
    }

    private async Task ApplyAbsencePenalty(SalaryReportViewModel salaryReport, AbsenceDays absenceDays, double basicSalary)
    {
        var absencePenalty = await _unitOfWork.AbsencePenaltyRepository.GetByAbsenceDaysAsync(absenceDays);
        if (absencePenalty != null)
        {
            salaryReport.AbsenceDays = absenceDays;
            salaryReport.PenaltyPercentage = absencePenalty.PenaltyPercentage;
            salaryReport.IsBonus = absencePenalty.IsBonus;

            if (absencePenalty.IsBonus)
                salaryReport.NetSalary += (absencePenalty.PenaltyPercentage / 100) * basicSalary;
            else
                salaryReport.NetSalary -= (absencePenalty.PenaltyPercentage / 100) * basicSalary;
        }
    }

    private async Task<IActionResult> GetAllEmployeeSalaryReports(IEnumerable<Employee> employees)
    {
        var allSalaries = new List<SalaryReportViewModel>();

        foreach (var employee in employees)
        {
            var salaryReports = await CalculateSalaryReport(employee);
            allSalaries.AddRange(salaryReports);
        }

        if (!allSalaries.Any())
        {
            ModelState.AddModelError("", "No salary records found for employees.");
            return View();
        }

        return View(allSalaries);
    }

}

using Payroll_Mohamed_Bayoumi.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Mohamed_Bayoumi.Models;
public class AttendanceRecord
{
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    [Required]
    public AbsenceDays AbsenceDays { get; set; }
    [Required]
    public Month Month { get; set; }
    [Required]
    public int Year { get; set; }
}
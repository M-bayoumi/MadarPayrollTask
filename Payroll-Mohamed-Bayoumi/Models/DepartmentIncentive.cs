using System.ComponentModel.DataAnnotations;

namespace Payroll_Mohamed_Bayoumi.Models;

public class DepartmentIncentive
{
    public int Id { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    [Range(0, 100, ErrorMessage = "نسبة الحافز يجب أن تكون بين 0 و 100")]
    [Required]
    public double IncentivePercentage { get; set; }
    public Department? Department { get; set; }
    public DepartmentIncentive()
    {

    }
}

using System.ComponentModel.DataAnnotations;

namespace Payroll_Mohamed_Bayoumi.Models;

public class SeniorityIncentive
{
    public int Id { get; set; }
    [Required]
    public int YearsOfService { get; set; }
    [Range(0, 100, ErrorMessage = "نسبة الحافز يجب أن تكون بين 0 و 100")]
    [Required]
    public double IncentivePercentage { get; set; }
    public SeniorityIncentive()
    {

    }
}

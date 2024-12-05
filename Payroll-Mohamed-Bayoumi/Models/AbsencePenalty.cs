using Payroll_Mohamed_Bayoumi.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Mohamed_Bayoumi.Models;

public class AbsencePenalty
{
    public int Id { get; set; }
    [Required]
    public AbsenceDays AbsenceDays { get; set; }
    [Range(0, 100, ErrorMessage = "نسبة الحافز يجب أن تكون بين 0 و 100")]
    [Required]
    public double PenaltyPercentage { get; set; }
    public bool IsBonus { get; set; }
}

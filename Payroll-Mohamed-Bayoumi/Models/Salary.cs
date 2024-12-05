using Payroll_Mohamed_Bayoumi.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Mohamed_Bayoumi.Models;

public class Salary
{
    public int Id { get; set; }
    [Required(ErrorMessage = "قيمة الراتب مطلوبة")]
    public double Amount { get; set; }
    [Required(ErrorMessage = "الدرجة الوظيفية مطلوبة")]
    public JobGrade JobGrade { get; set; }
    public Salary()
    {

    }
}

using Payroll_Mohamed_Bayoumi.Enums;
using System.ComponentModel.DataAnnotations;
namespace Payroll_Mohamed_Bayoumi.Models;

public class Employee
{
    public int Id { get; set; }
    [Required(ErrorMessage = "اسم الموظف مطلوب")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "رقم الهاتف مطلوب")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "العنوان مطلوب")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "الدرجة الوظيفية مطلوبة")]
    public JobGrade JobGrade { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    [Required(ErrorMessage = "تاريخ التعيين مطلوب")]
    [DataType(DataType.Date)]
    public DateTime HiringDate { get; set; }
    public Employee()
    {

    }
}

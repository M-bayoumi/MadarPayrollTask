using System.ComponentModel.DataAnnotations;
namespace Payroll_Mohamed_Bayoumi.Models;

public class Department
{
    public int Id { get; set; }
    [Required(ErrorMessage = "اسم القسم مطلوب")]
    public string Name { get; set; } = string.Empty;

    public List<Employee> Employees { get; set; } = new List<Employee>();
    public Department()
    {

    }
}

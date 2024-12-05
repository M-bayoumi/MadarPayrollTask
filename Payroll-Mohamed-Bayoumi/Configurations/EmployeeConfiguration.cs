using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Configurations;

internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {

        builder.HasKey(x => x.Id);

    }
}

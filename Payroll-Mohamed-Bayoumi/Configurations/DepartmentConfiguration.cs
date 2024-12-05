using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Configurations;

internal sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Configurations;

internal sealed class DepartmentIncentiveConfiguration : IEntityTypeConfiguration<DepartmentIncentive>
{
    public void Configure(EntityTypeBuilder<DepartmentIncentive> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Department)
            .WithOne(x => x.DepartmentIncentive)
            .HasForeignKey<DepartmentIncentive>(x => x.DepartmentId);
    }
}


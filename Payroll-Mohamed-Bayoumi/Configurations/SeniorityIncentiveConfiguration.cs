using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Configurations;

internal sealed class SeniorityIncentiveConfiguration : IEntityTypeConfiguration<SeniorityIncentive>
{
    public void Configure(EntityTypeBuilder<SeniorityIncentive> builder)
    {
        builder.HasKey(x => x.Id);
    }
}


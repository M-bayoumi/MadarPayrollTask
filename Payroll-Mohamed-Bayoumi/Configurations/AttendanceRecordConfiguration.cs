using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Configurations;

internal sealed class AttendanceRecordConfiguration : IEntityTypeConfiguration<AttendanceRecord>
{
    public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Employee)
            .WithMany(x => x.AttendanceRecords)
            .HasForeignKey(x => x.EmployeeId);
    }
}



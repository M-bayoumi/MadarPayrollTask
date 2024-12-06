using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Enums;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;

namespace Payroll_Mohamed_Bayoumi.Repositories.Implementations;

public class AttendanceRecordRepository : IAttendanceRecordRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AttendanceRecord>> GetAllAsync()
    {
        return await _context
                   .AttendanceRecords
                   .Include(x => x.Employee)
                   .ToListAsync();
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context
                   .AttendanceRecords
                   .Where(x => x.EmployeeId == employeeId)
                   .Include(x => x.Employee)
                   .ToListAsync();
    }
    public async Task<AttendanceRecord?> GetByDateAsync(int employeeId, Month month, int year)
    {
        return await _context
                   .AttendanceRecords
                   .Where(x => x.EmployeeId == employeeId && x.Month == month && x.Year == year)
                   .FirstOrDefaultAsync();
    }

    public async Task<AttendanceRecord?> GetByIdAsync(int id)
    {
        return await _context
                   .AttendanceRecords
                   .Include(x => x.Employee)
                   .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task AddAsync(AttendanceRecord attendanceRecord)
    {
        await _context
                .AttendanceRecords
                .AddAsync(attendanceRecord);
    }

    public void Update(AttendanceRecord attendanceRecord)
    {
        _context
            .AttendanceRecords
            .Update(attendanceRecord);
    }
    public void Delete(AttendanceRecord attendanceRecord)
    {
        _context
               .AttendanceRecords
               .Remove(attendanceRecord);
    }
    public bool IsAttendanceRecordExist(AttendanceRecord attendanceRecord)
    {
        return _context
                    .AttendanceRecords
                    .Any(x => x.EmployeeId == attendanceRecord.EmployeeId
                    && x.Month == attendanceRecord.Month
                    && x.Year == attendanceRecord.Year
                    && x.Id != attendanceRecord.Id);
    }


}

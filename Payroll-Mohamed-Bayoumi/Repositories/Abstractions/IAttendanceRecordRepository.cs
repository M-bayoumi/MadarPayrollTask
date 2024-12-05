using Payroll_Mohamed_Bayoumi.Models;

namespace Payroll_Mohamed_Bayoumi.Repositories.Abstractions;
public interface IAttendanceRecordRepository
{
    Task<IEnumerable<AttendanceRecord>> GetAllAsync();
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(int employeeId);
    Task<AttendanceRecord?> GetByIdAsync(int id);
    Task AddAsync(AttendanceRecord attendanceRecord);
    void Update(AttendanceRecord attendanceRecord);
    void Delete(AttendanceRecord attendanceRecord);
    bool IsAttendanceRecordExist(AttendanceRecord attendanceRecord);

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers
{
    public class AttendanceRecordController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var attendanceRecords = await _unitOfWork.AttendanceRecordRepository.GetAllAsync();
            return View(attendanceRecords);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var attendanceRecord = await _unitOfWork.AttendanceRecordRepository.GetByIdAsync(id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }
            return View(attendanceRecord);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttendanceRecord attendanceRecord)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var IsAttendanceRecordExist = _unitOfWork.AttendanceRecordRepository.IsAttendanceRecordExist(attendanceRecord);

                    if (IsAttendanceRecordExist)
                    {
                        var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
                        ViewBag.Employees = new SelectList(employees, "Id", "Name");
                        ModelState.AddModelError("EmployeeId", "This Month already exists for this employee.");
                        return View(attendanceRecord);
                    }
                    await _unitOfWork.AttendanceRecordRepository.AddAsync(attendanceRecord);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "حدث خطأ أثناء إضافة ايام الغياب لموظف: " + ex.Message);
                }
            }

            return View(attendanceRecord);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var attendanceRecord = await _unitOfWork.AttendanceRecordRepository.GetByIdAsync(id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            IEnumerable<Employee>? employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "Name", attendanceRecord.EmployeeId);
            return View(attendanceRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AttendanceRecord attendanceRecord)
        {
            if (id != attendanceRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var IsAttendanceRecordExist = _unitOfWork.AttendanceRecordRepository.IsAttendanceRecordExist(attendanceRecord);

                    if (IsAttendanceRecordExist)
                    {
                        var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
                        ViewBag.Employees = new SelectList(employees, "Id", "Name");
                        ModelState.AddModelError("EmployeeId", "This Month already exists for this employee.");
                        return View(attendanceRecord);
                    }
                    _unitOfWork.AttendanceRecordRepository.Update(attendanceRecord);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "حدث خطأ أثناء إضافة ايام الغياب لموظف: " + ex.Message);
                }
            }

            return View(attendanceRecord);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var attendanceRecord = await _unitOfWork.AttendanceRecordRepository.GetByIdAsync(id);
            if (attendanceRecord == null)
                return NotFound();

            return View(attendanceRecord);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var attendanceRecord = await _unitOfWork.AttendanceRecordRepository.GetByIdAsync(id);
                if (attendanceRecord == null)
                    return NotFound();

                _unitOfWork.AttendanceRecordRepository.Delete(attendanceRecord);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء إضافة ايام الغياب لموظف: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }

}

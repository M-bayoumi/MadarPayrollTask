using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.EmployeeRepository.AddAsync(employee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "حدث خطأ أثناء إضافة الموظف: " + ex.Message);
                }
            }

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.EmployeeRepository.Update(employee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "حدث خطأ أثناء تعديل بيانات الموظف: " + ex.Message);
                }
            }

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
                if (employee == null)
                    return NotFound();

                _unitOfWork.EmployeeRepository.Delete(employee);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء حذف الموظف: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }

}

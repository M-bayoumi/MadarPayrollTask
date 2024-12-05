using Microsoft.AspNetCore.Mvc;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers;

public class DepartmentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
        return View(departments);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء إضافة القسم: " + ex.Message);
            }
        }

        return View(department);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _unitOfWork.DepartmentRepository.Update(department);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء تعديل بيانات القسم: " + ex.Message);
            }
        }
        return View(department);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
        if (department == null)
            return NotFound();

        return View(department);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            _unitOfWork.DepartmentRepository.Delete(department);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "حدث خطأ أثناء حذف القسم: " + ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}


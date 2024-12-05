using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers;

public class DepartmentIncentiveController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentIncentiveController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var departmentIncentives = await _unitOfWork.DepartmentIncentiveRepository.GetAllAsync();
        return View(departmentIncentives);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var departmentIncentive = await _unitOfWork.DepartmentIncentiveRepository.GetByIdAsync(id);
        if (departmentIncentive == null)
        {
            return NotFound();
        }
        return View(departmentIncentive);
    }

    public async Task<IActionResult> Create()
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
        ViewBag.Departments = new SelectList(departments, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DepartmentIncentive departmentIncentive)
    {
        if (ModelState.IsValid)
        {
            var isDepartmentExist = _unitOfWork.DepartmentIncentiveRepository.IsDepartmentExist(departmentIncentive);

            if (isDepartmentExist)
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");
                ModelState.AddModelError("DepartmentId", "This Department already has an incentive.");
                return View(departmentIncentive);
            }

            try
            {
                await _unitOfWork.DepartmentIncentiveRepository.AddAsync(departmentIncentive);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while adding department incentive: " + ex.Message);
            }
        }

        return View(departmentIncentive);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var departmentIncentive = await _unitOfWork.DepartmentIncentiveRepository.GetByIdAsync(id);
        if (departmentIncentive == null)
        {
            return NotFound();
        }
        var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
        ViewBag.Departments = new SelectList(departments, "Id", "Name");
        return View(departmentIncentive);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DepartmentIncentive departmentIncentive)
    {
        if (id != departmentIncentive.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var isDepartmentExist = _unitOfWork.DepartmentIncentiveRepository.IsDepartmentExist(departmentIncentive);

            if (isDepartmentExist)
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");
                ModelState.AddModelError("DepartmentId", "This Department already has an incentive.");
                return View(departmentIncentive);
            }

            try
            {
                _unitOfWork.DepartmentIncentiveRepository.Update(departmentIncentive);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while editing department incentive: " + ex.Message);
            }
        }
        return View(departmentIncentive);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var departmentIncentive = await _unitOfWork.DepartmentIncentiveRepository.GetByIdAsync(id);
        if (departmentIncentive == null)
            return NotFound();

        return View(departmentIncentive);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var departmentIncentive = await _unitOfWork.DepartmentIncentiveRepository.GetByIdAsync(id);
            if (departmentIncentive == null)
                return NotFound();

            _unitOfWork.DepartmentIncentiveRepository.Delete(departmentIncentive);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error occurred while deleting department incentive: " + ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}

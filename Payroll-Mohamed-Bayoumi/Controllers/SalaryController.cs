using Microsoft.AspNetCore.Mvc;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers;

public class SalaryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public SalaryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var salaries = await _unitOfWork.SalaryRepository.GetAllAsync();
        return View(salaries);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var salary = await _unitOfWork.SalaryRepository.GetByIdAsync(id);
        if (salary == null)
        {
            return NotFound();
        }
        return View(salary);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Salary salary)
    {
        if (ModelState.IsValid)
        {
            var IsGradeExist = _unitOfWork.SalaryRepository.IsGradeExist(salary.JobGrade);

            if (IsGradeExist)
            {
                ModelState.AddModelError("JobGrade", "This Job Grade already exists.");
                return View(salary);
            }

            try
            {
                await _unitOfWork.SalaryRepository.AddAsync(salary);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء إضافة القسم: " + ex.Message);
            }
        }

        return View(salary);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var salary = await _unitOfWork.SalaryRepository.GetByIdAsync(id);
        if (salary == null)
        {
            return NotFound();
        }
        return View(salary);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Salary salary)
    {
        if (id != salary.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var IsGradeExist = _unitOfWork.SalaryRepository.IsGradeExist(salary.JobGrade);

            if (IsGradeExist)
            {
                ModelState.AddModelError("JobGrade", "This Job Grade already exists.");
                return View(salary);
            }

            try
            {
                _unitOfWork.SalaryRepository.Update(salary);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء تعديل بيانات القسم: " + ex.Message);
            }
        }
        return View(salary);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var salary = await _unitOfWork.SalaryRepository.GetByIdAsync(id);
        if (salary == null)
            return NotFound();

        return View(salary);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var salary = await _unitOfWork.SalaryRepository.GetByIdAsync(id);
            if (salary == null)
                return NotFound();

            _unitOfWork.SalaryRepository.Delete(salary);
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


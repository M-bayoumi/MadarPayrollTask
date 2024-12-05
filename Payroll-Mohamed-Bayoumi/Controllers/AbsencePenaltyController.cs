using Microsoft.AspNetCore.Mvc;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers;

public class AbsencePenaltyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public AbsencePenaltyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var absencePenalties = await _unitOfWork.AbsencePenaltyRepository.GetAllAsync();
        return View(absencePenalties);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var absencePenalty = await _unitOfWork.AbsencePenaltyRepository.GetByIdAsync(id);
        if (absencePenalty == null)
        {
            return NotFound();
        }
        return View(absencePenalty);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AbsencePenalty absencePenalty)
    {
        if (ModelState.IsValid)
        {
            var IsAbsenceDaysExist = _unitOfWork.AbsencePenaltyRepository.IsAbsenceDaysExist(absencePenalty);

            if (IsAbsenceDaysExist)
            {
                ModelState.AddModelError("AbsenceDays", "This Absence Days already exists.");
                return View(absencePenalty);
            }

            try
            {
                await _unitOfWork.AbsencePenaltyRepository.AddAsync(absencePenalty);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء إضافة خصم ايام الغياب: " + ex.Message);
            }
        }

        return View(absencePenalty);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var absencePenalty = await _unitOfWork.AbsencePenaltyRepository.GetByIdAsync(id);
        if (absencePenalty == null)
        {
            return NotFound();
        }
        return View(absencePenalty);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AbsencePenalty absencePenalty)
    {
        if (id != absencePenalty.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var IsAbsenceDaysExist = _unitOfWork.AbsencePenaltyRepository.IsAbsenceDaysExist(absencePenalty);

            if (IsAbsenceDaysExist)
            {
                ModelState.AddModelError("AbsenceDays", "This Absence Days already exists.");
                return View(absencePenalty);
            }

            try
            {
                _unitOfWork.AbsencePenaltyRepository.Update(absencePenalty);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء إضافة خصم ايام الغياب: " + ex.Message);
            }
        }
        return View(absencePenalty);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var absencePenalty = await _unitOfWork.AbsencePenaltyRepository.GetByIdAsync(id);
        if (absencePenalty == null)
            return NotFound();

        return View(absencePenalty);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var absencePenalty = await _unitOfWork.AbsencePenaltyRepository.GetByIdAsync(id);
            if (absencePenalty == null)
                return NotFound();

            _unitOfWork.AbsencePenaltyRepository.Delete(absencePenalty);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "حدث خطأ أثناء حذف خصم ايام الغياب: " + ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}

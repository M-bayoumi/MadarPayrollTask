using Microsoft.AspNetCore.Mvc;
using Payroll_Mohamed_Bayoumi.Models;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

namespace Payroll_Mohamed_Bayoumi.Controllers;

public class SeniorityIncentiveController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public SeniorityIncentiveController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var seniorityIncentives = await _unitOfWork.SeniorityIncentiveRepository.GetAllAsync();
        return View(seniorityIncentives);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var seniorityIncentive = await _unitOfWork.SeniorityIncentiveRepository.GetByIdAsync(id);
        if (seniorityIncentive == null)
        {
            return NotFound();
        }
        return View(seniorityIncentive);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SeniorityIncentive seniorityIncentive)
    {
        if (ModelState.IsValid)
        {
            var isYearsOfServiceExist = _unitOfWork.SeniorityIncentiveRepository.IsYearsOfServiceExist(seniorityIncentive.YearsOfService);

            if (isYearsOfServiceExist)
            {
                ModelState.AddModelError("YearsOfService", "This Years of Service incentive already exists.");
                return View(seniorityIncentive);
            }

            try
            {
                await _unitOfWork.SeniorityIncentiveRepository.AddAsync(seniorityIncentive);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while adding seniority incentive: " + ex.Message);
            }
        }
        return View(seniorityIncentive);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var seniorityIncentive = await _unitOfWork.SeniorityIncentiveRepository.GetByIdAsync(id);
        if (seniorityIncentive == null)
        {
            return NotFound();
        }
        return View(seniorityIncentive);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SeniorityIncentive seniorityIncentive)
    {
        if (id != seniorityIncentive.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var isYearsOfServiceExist = _unitOfWork.SeniorityIncentiveRepository.IsYearsOfServiceExist(seniorityIncentive.YearsOfService);

            if (isYearsOfServiceExist)
            {
                ModelState.AddModelError("YearsOfService", "This Years of Service incentive already exists.");
                return View(seniorityIncentive);
            }

            try
            {
                _unitOfWork.SeniorityIncentiveRepository.Update(seniorityIncentive);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while editing seniority incentive: " + ex.Message);
            }
        }
        return View(seniorityIncentive);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var seniorityIncentive = await _unitOfWork.SeniorityIncentiveRepository.GetByIdAsync(id);
        if (seniorityIncentive == null)
            return NotFound();

        return View(seniorityIncentive);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var seniorityIncentive = await _unitOfWork.SeniorityIncentiveRepository.GetByIdAsync(id);
            if (seniorityIncentive == null)
                return NotFound();

            _unitOfWork.SeniorityIncentiveRepository.Delete(seniorityIncentive);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error occurred while deleting seniority incentive: " + ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
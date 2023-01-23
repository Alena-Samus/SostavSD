using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Models;
using SostavSD.Interfaces;

namespace SostavSD.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IContractService _ContractService;
        public ContractsController(IContractService contractService)
        {
            _ContractService = contractService;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
                        return View();
        }

        //// GET: Contracts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Contract == null)
        //    {
        //        return NotFound();
        //    }

        //    var contract = await _context.Contract
        //        .FirstOrDefaultAsync(m => m.ContractID == id);
        //    if (contract == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contract);
        //}

        //// GET: Contracts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Contracts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ContractID,ProjectName,Index,Order,ContractNumber,ContractDate,ContractDateEndOfWork,City")] ContractModel contract)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contract);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contract);
        //}

        //// GET: Contracts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Contract == null)
        //    {
        //        return NotFound();
        //    }

        //    var contract = await _context.Contract.FindAsync(id);
        //    if (contract == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(contract);
        //}

        //// POST: Contracts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ContractID,ProjectName,Index,Order,ContractNumber,ContractDate,ContractDateEndOfWork,City")] ContractModel contract)
        //{
        //    if (id != contract.ContractID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contract);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContractExists(contract.ContractID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contract);
        //}

        //// GET: Contracts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Contract == null)
        //    {
        //        return NotFound();
        //    }

        //    var contract = await _context.Contract
        //        .FirstOrDefaultAsync(m => m.ContractID == id);
        //    if (contract == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contract);
        //}

        //// POST: Contracts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Contract == null)
        //    {
        //        return Problem("Entity set 'SostavSDContext.Contract'  is null.");
        //    }
        //    var contract = await _context.Contract.FindAsync(id);
        //    if (contract != null)
        //    {
        //        _context.Contract.Remove(contract);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContractExists(int id)
        //{
        //  return (_context.Contract?.Any(e => e.ContractID == id)).GetValueOrDefault();
        //}
    }
}

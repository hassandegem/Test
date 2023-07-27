using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleCRMApp.Data;
using SaleCRMApp.Models;

namespace SaleCRMApp.Controllers
{
    public class Leads : Controller
    {
        private readonly ApplicationDBContext _context;

        public Leads(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Leads
        public async Task<IActionResult> Index()
        {
              return _context.SaleLead != null ? 
                          View(await _context.SaleLead.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.SaleLead'  is null.");
        }

        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SaleLead == null)
            {
                return NotFound();
            }

            var saleLeadEntity = await _context.SaleLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleLeadEntity == null)
            {
                return NotFound();
            }

            return View(saleLeadEntity);
        }

        // GET: Leads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] SaleLeadEntity saleLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleLeadEntity);
        }

        // GET: Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SaleLead == null)
            {
                return NotFound();
            }

            var saleLeadEntity = await _context.SaleLead.FindAsync(id);
            if (saleLeadEntity == null)
            {
                return NotFound();
            }
            return View(saleLeadEntity);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] SaleLeadEntity saleLeadEntity)
        {
            if (id != saleLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleLeadEntityExists(saleLeadEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saleLeadEntity);
        }

        // GET: Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SaleLead == null)
            {
                return NotFound();
            }

            var saleLeadEntity = await _context.SaleLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleLeadEntity == null)
            {
                return NotFound();
            }

            return View(saleLeadEntity);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SaleLead == null)
            {
                return Problem("Entity set 'ApplicationDBContext.SaleLead'  is null.");
            }
            var saleLeadEntity = await _context.SaleLead.FindAsync(id);
            if (saleLeadEntity != null)
            {
                _context.SaleLead.Remove(saleLeadEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleLeadEntityExists(int id)
        {
          return (_context.SaleLead?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

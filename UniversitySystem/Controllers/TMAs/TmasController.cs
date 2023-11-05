using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Context;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers.TMAs
{
    public class TmasController : Controller
    {
        private readonly UniversityDbContext _context;

        public TmasController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: Tmas
        public async Task<IActionResult> Index()
        {
            return _context.Tmas != null ?
                        View(await _context.Tmas.ToListAsync()) :
                        Problem("Entity set 'UniversityDbContext.Tmas'  is null.");
        }

        // GET: Tmas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Tmas == null)
            {
                return NotFound();
            }

            var tma = await _context.Tmas
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (tma == null)
            {
                return NotFound();
            }

            return View(tma);
        }

        // GET: Tmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Tmaid,Tmaletter")] Tma tma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tma);
        }

        // GET: Tmas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Tmas == null)
            {
                return NotFound();
            }

            var tma = await _context.Tmas.FindAsync(id);
            if (tma == null)
            {
                return NotFound();
            }
            return View(tma);
        }

        // POST: Tmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseId,Tmaid,Tmaletter")] Tma tma)
        {
            if (id != tma.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmaExists(tma.CourseId))
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
            return View(tma);
        }

        // GET: Tmas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Tmas == null)
            {
                return NotFound();
            }

            var tma = await _context.Tmas
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (tma == null)
            {
                return NotFound();
            }

            return View(tma);
        }

        // POST: Tmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Tmas == null)
            {
                return Problem("Entity set 'UniversityDbContext.Tmas'  is null.");
            }
            var tma = await _context.Tmas.FindAsync(id);
            if (tma != null)
            {
                _context.Tmas.Remove(tma);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TmaExists(string id)
        {
            return (_context.Tmas?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}

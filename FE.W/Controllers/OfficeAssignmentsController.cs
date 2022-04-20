using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FE.W.Models;

namespace FE.W.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private readonly ContosoUniversity2Context _context;

        public OfficeAssignmentsController(ContosoUniversity2Context context)
        {
            _context = context;
        }

        // GET: OfficeAssignments
        public async Task<IActionResult> Index()
        {
            var contosoUniversity2Context = _context.OfficeAssignment.Include(o => o.Instructor);
            return View(await contosoUniversity2Context.ToListAsync());
        }

        // GET: OfficeAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeAssignment = await _context.OfficeAssignment
                .Include(o => o.Instructor)
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (officeAssignment == null)
            {
                return NotFound();
            }

            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Person, "Id", "Discriminator");
            return View();
        }

        // POST: OfficeAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorId,Location")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(officeAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Person, "Id", "Discriminator", officeAssignment.InstructorId);
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeAssignment = await _context.OfficeAssignment.FindAsync(id);
            if (officeAssignment == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Person, "Id", "Discriminator", officeAssignment.InstructorId);
            return View(officeAssignment);
        }

        // POST: OfficeAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,Location")] OfficeAssignment officeAssignment)
        {
            if (id != officeAssignment.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officeAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeAssignmentExists(officeAssignment.InstructorId))
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
            ViewData["InstructorId"] = new SelectList(_context.Person, "Id", "Discriminator", officeAssignment.InstructorId);
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeAssignment = await _context.OfficeAssignment
                .Include(o => o.Instructor)
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (officeAssignment == null)
            {
                return NotFound();
            }

            return View(officeAssignment);
        }

        // POST: OfficeAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officeAssignment = await _context.OfficeAssignment.FindAsync(id);
            _context.OfficeAssignment.Remove(officeAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficeAssignmentExists(int id)
        {
            return _context.OfficeAssignment.Any(e => e.InstructorId == id);
        }
    }
}

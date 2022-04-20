using FE.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FE.Models;

namespace FE.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IPersonServices personServices;
        private readonly IDepartmentServices departmentServices;

        public DepartmentController(IDepartmentServices departmentServices, IPersonServices personServices)
        {
            this.departmentServices = departmentServices;
            this.personServices = personServices;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            //var contosoUniversity2Context = _context.Department.Include(d => d.Instructor);
            return View(departmentServices.GetAllAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = departmentServices.GetOneByIdAsync((int)id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name,Budget,StartDate,InstructorId,RowVersion")] Department department)
        {
            if (ModelState.IsValid)
            {
               departmentServices.Insert(department);   
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", department.InstructorId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = departmentServices.GetOneById((int)id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", department.InstructorId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name,Budget,StartDate,InstructorId,RowVersion")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    departmentServices.Update(department);
                }
                catch (Exception ee)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            ViewData["InstructorId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", department.InstructorId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = departmentServices.GetOneByIdAsync((int)id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = departmentServices.GetOneById((int)id);
            departmentServices.Update(department);
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return (departmentServices.GetOneById((int)id) != null);    
        }
    }
}

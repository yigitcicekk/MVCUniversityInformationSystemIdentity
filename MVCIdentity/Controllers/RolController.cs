using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCIdentity.Data;
using MVCIdentity.Models;

namespace MVCIdentity.Controllers
{
    // Roller ile ilgili işlemlerin yapılabilmesi için RoleManager'a ihtiyacımız var.
    // RoleManager identity'den gelmektedir zaten.

    [Authorize(Roles = "Rektör")]
    public class RolController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolController(RoleManager<IdentityRole> roleManager) 
        {
            _roleManager = roleManager;
        }

        // GET: Rol
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        //GET: Rol/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        //// GET: Rol/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Rol/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] IdentityRole rol)
        {
            if (ModelState.IsValid)
            {
                rol.ConcurrencyStamp = Guid.NewGuid().ToString();   
                await _roleManager.CreateAsync(rol);
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        //// GET: Rol/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);    
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        //// POST: Rol/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ConcurrencyStamp")] IdentityRole rol)
        {
            if (id != rol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(rol);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolExists(rol.Id))
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
            return View(rol);
        }

        //// GET: Rol/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        //// POST: Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rol = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (rol != null)
            {
                await _roleManager.DeleteAsync(rol);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RolExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id); 
        }
    }
}

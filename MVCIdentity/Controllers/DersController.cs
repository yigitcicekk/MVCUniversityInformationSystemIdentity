using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCIdentity.Data;
using MVCIdentity.Models;

namespace MVCIdentity.Controllers
{
    [Authorize(Roles = "Rektör,Ogrenci Isleri")]
    public class DersController : Controller
    {
        private readonly MVCIdentityContext _context;

        public DersController(MVCIdentityContext context)
        {
            _context = context;
        }

        // GET: Ders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ders.ToListAsync());
        }

        // GET: Ders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // GET: Ders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kod,Ad,Kredi")] Ders ders)
        {
            // OgrenciDersler olan listeyi modelstate'den kaldıralım ki validasyona takılmasın.
            ModelState.Remove("OgrenciDersler");
            if (ModelState.IsValid)
            {
                _context.Add(ders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

        // GET: Ders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders.FindAsync(id);
            if (ders == null)
            {
                return NotFound();
            }
            return View(ders);
        }

        // POST: Ders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kod,Ad,Kredi")] Ders ders)
        {
            if (id != ders.Id)
            {
                return NotFound();
            }
            ModelState.Remove("OgrenciDersler");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DersExists(ders.Id))
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
            return View(ders);
        }

        // GET: Ders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // POST: Ders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ders = await _context.Ders.FindAsync(id);
            if (ders != null)
            {
                _context.Ders.Remove(ders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DersExists(int id)
        {
            return _context.Ders.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kinosal.Models;

namespace Кинозал__cr.det.upd.del._.Controllers
{
    public class KinoesController : Controller
    {
        private readonly KinoContext _context;

        public KinoesController(KinoContext context)
        {
            _context = context;
        }

        // GET: Kinoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kinos.ToListAsync());
        }

        // GET: Kinoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kino = await _context.Kinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kino == null)
            {
                return NotFound();
            }

            return View(kino);
        }

        // GET: Kinoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kinoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Director,Description,Poster")] Kino kino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kino);
        }

        // GET: Kinoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kino = await _context.Kinos.FindAsync(id);
            if (kino == null)
            {
                return NotFound();
            }
            return View(kino);
        }

        // POST: Kinoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Director,Description,Poster")] Kino kino)
        {
            if (id != kino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KinoExists(kino.Id))
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
            return View(kino);
        }

        // GET: Kinoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kino = await _context.Kinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kino == null)
            {
                return NotFound();
            }

            return View(kino);
        }

        // POST: Kinoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kino = await _context.Kinos.FindAsync(id);
            if (kino != null)
            {
                _context.Kinos.Remove(kino);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KinoExists(int id)
        {
            return _context.Kinos.Any(e => e.Id == id);
        }
    }
}

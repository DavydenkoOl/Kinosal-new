using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kinosal.Models;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace Кинозал__cr.det.upd.del._.Controllers
{
    public class KinoesController : Controller
    {
        private readonly KinoContext _context;

        // IWebHostEnvironment предоставляет информацию об окружении, в котором запущено приложение
        IWebHostEnvironment _appEnvironment;
        public KinoesController(KinoContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
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
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Director,Description,Poster")] Kino kino, IFormFile? uploadedFile)
        {
            var title = _context.Kinos.Select(m => m.Title).ToList();
            var genre = _context.Kinos.Select(m => m.Genre).ToList();
            var director = _context.Kinos.Select(m => m.Director).ToList();
            if (uploadedFile != null)
            {
                // Путь к папке Files
                string path = "/images/" + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку images в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                
                
                   
                
                kino.Poster = uploadedFile.FileName;
            }
            if(title.Contains(kino.Title) && genre.Contains(kino.Genre) && director.Contains(kino.Director))
            {
                ModelState.AddModelError("", "Внимание!!! Совпадение добавляемого фильма по полям 'Название','Жанр','Редиссёр'!!! Этот фильм уже внесёт в каталог!!");
            }
           
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Director,Description,Poster")] Kino kino, IFormFile? uploadedFile)
        {
            if (id != kino.Id)
            {
                return NotFound();
            }
            if (uploadedFile == null)
                kino.Poster = (from p in _context.Kinos where p.Id == kino.Id select p.Poster).FirstOrDefault();

            
            else
                kino.Poster = uploadedFile.FileName;

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

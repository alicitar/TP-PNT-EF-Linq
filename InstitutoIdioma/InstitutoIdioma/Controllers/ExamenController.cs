using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstitutoIdioma.Context;
using InstitutoIdioma.Models;

namespace InstitutoIdioma.Controllers
{
    public class ExamenController : Controller
    {
        private readonly InstitutoDatabaseContext _context;

        public ExamenController(InstitutoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Examen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Examenes.ToListAsync());
        }

        // GET: Examen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examenes.Include("Preguntas").FirstOrDefaultAsync(e => e.Id == id);

            if (examen == null)
            {
                return NotFound();
            }

            return View(examen);
        }

        // GET: Examen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Examen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Examen examen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examen);
        }

        // GET: Examen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examenes.Include("Preguntas").FirstOrDefaultAsync(e => e.Id == id); 
            if (examen == null)
            {
                return NotFound();
            }
            return View(examen);
        }

        // POST: Examen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Examen examen)
        {
            if (id != examen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenExists(examen.Id))
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
            return View(examen);
        }

        // GET: Examen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examenes.Include("Preguntas").FirstOrDefaultAsync(e => e.Id == id);
            if (examen == null)
            {
                return NotFound();
            }

            return View(examen);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examen = await _context.Examenes.FindAsync(id);
            _context.Examenes.Remove(examen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddPregunta(int id, string enunciado)
        {
            var examen = await _context.Examenes.FirstOrDefaultAsync(e => e.Id == id);
            examen.Preguntas.Add(new Pregunta { Enunciado = enunciado });
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = id });
        }

        private bool ExamenExists(int id)
        {
            return _context.Examenes.Any(e => e.Id == id);
        }
    }
}

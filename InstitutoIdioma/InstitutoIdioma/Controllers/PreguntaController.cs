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
    public class PreguntaController : Controller
    {
        private readonly InstitutoDatabaseContext _context;

        public PreguntaController(InstitutoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Pregunta
        public async Task<IActionResult> Index()
        {
            return View(await _context.preguntas.ToListAsync());
        }

        // GET: Pregunta/Details/5
        public async Task<IActionResult> Details(int? id, int examenId)
        {

            if (id == null || examenId == null)
            {
                return NotFound();
            }
            var examen = await _context.Examenes.FindAsync(examenId);
            var pregunta = await _context.preguntas.FindAsync(id);
            pregunta.Examen = examen;
            if (pregunta == null || examen == null)
            {
                return NotFound();
            }

            return View(pregunta);
        }


        // GET: Pregunta/Create
        public IActionResult Create(int examenId)
        {
            var examen = _context.Examenes.FirstOrDefault(e => e.Id == examenId);

            if (examen == null)
            {
                return NotFound();
            }

            Pregunta pregunta = new Pregunta();
            pregunta.Examen = examen;
            return View(pregunta);
        }

        // POST: Pregunta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Enunciado, Examen")] Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("AddPregunta", "Examen", new { id= pregunta.Examen.Id, enunciado = pregunta.Enunciado });
            }
            return View(pregunta);
        }

        // GET: Pregunta/Edit/5
        public async Task<IActionResult> Edit(int id, int examenId)
        {
            if (id == null || examenId == null)
            {
                return NotFound();
            }

            var examen = await _context.Examenes.FindAsync(examenId);
            var pregunta = await _context.preguntas.FindAsync(id);
            pregunta.Examen = examen;
            if (pregunta == null || examen == null)
            {
                return NotFound();
            }
            return View(pregunta);
        }

        // POST: Pregunta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Enunciado, Examen")] Pregunta pregunta)
        {
            if (id != pregunta.Id )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pregunta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntaExists(pregunta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Examen", new { id = pregunta.Examen.Id });
            }
            return View(pregunta);
        }

        // GET: Pregunta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.preguntas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return View(pregunta);
        }

        // POST: Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pregunta = await _context.preguntas.FindAsync(id);
            _context.preguntas.Remove(pregunta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreguntaExists(int id)
        {
            return _context.preguntas.Any(e => e.Id == id);
        }
    }
}

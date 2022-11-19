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
    public class OpcionController : Controller
    {
        private readonly InstitutoDatabaseContext _context;

        public OpcionController(InstitutoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Opcion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Opciones.ToListAsync());
        }

        // GET: Opcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcion = await _context.Opciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opcion == null)
            {
                return NotFound();
            }

            return View(opcion);
        }

        // GET: Opcion/Create
        public IActionResult Create(int preguntaId)
        {
            var pregunta = _context.Preguntas.FirstOrDefault(e => e.Id == preguntaId);

            if (pregunta == null)
            {
                return NotFound();
            }

            Opcion opcion = new Opcion();
            opcion.Pregunta = pregunta;
            return View(opcion);
        }

        // POST: Opcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto,EsCorrecta, Pregunta")] Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("AddOpcion", "Pregunta", new { id = opcion.Pregunta.Id, texto = opcion.Texto, esCorrecta = opcion.EsCorrecta });
            }
            return View(opcion);
        }

        // GET: Opcion/Edit/5
        public async Task<IActionResult> Edit(int? id, int preguntaId)
        {
            if (id == null || preguntaId == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Preguntas.Include(e => e.Examen).FirstOrDefaultAsync(e => e.Id == preguntaId);
            var opcion = await _context.Opciones.FindAsync(id);
            if (opcion == null || preguntaId == null)
            {
                return NotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto,EsCorrecta, Pregunta")] Opcion opcion)
        {
            if (id != opcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    opcion.Pregunta = await _context.Preguntas.Include(e => e.Examen).FirstOrDefaultAsync(e => e.Id == opcion.Pregunta.Id);

                    _context.Update(opcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcionExists(opcion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Pregunta", new { id = opcion.Pregunta.Id, examenId = opcion.Pregunta.Examen.Id });
            }
            return View(opcion);
        }

        // GET: Opcion/Delete/5
        public async Task<IActionResult> Delete(int? id, int preguntaId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pregunta = await _context.Preguntas.Include(e => e.Examen).FirstOrDefaultAsync(e => e.Id == preguntaId);
            var opcion = await _context.Opciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opcion == null)
            {
                return NotFound();
            }

            return View(opcion);
        }

        // POST: Opcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Opcion opcion)
        {

            opcion.Pregunta = await _context.Preguntas.Include(e => e.Examen).FirstOrDefaultAsync(e => e.Id == opcion.Pregunta.Id);
            _context.Opciones.Remove(opcion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Pregunta", new { id = opcion.Pregunta.Id, examenId = opcion.Pregunta.Examen.Id });
        }

        private bool OpcionExists(int id)
        {
            return _context.Opciones.Any(e => e.Id == id);
        }
    }
}

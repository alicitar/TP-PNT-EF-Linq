using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstitutoIdioma.Context;
using InstitutoIdioma.Models;
using Microsoft.AspNetCore.Http;

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
            List<Examen> examenes = await _context.Examenes.ToListAsync();
            examenes = examenes.Where(e => (int)e.Nivel == HttpContext.Session.GetInt32("Nivel")).ToList();
            return View(examenes);
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
        public async Task<IActionResult> Create([Bind("Id, Nombre, Nivel")] Examen examen)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nombre, Nivel")] Examen examen)
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

        // GET: Examen/RendirExamenMenu
        public async Task<IActionResult> RendirExamenMenu()
        {
            Usuario usuario = _context.Usuarios.Where(u => u.NombreUsuario == HttpContext.Session.GetString("Usuario")).SingleOrDefault();
            List<Examen> examenes = _context.Examenes.Include(e => e.Preguntas).Where(e => (int)e.Nivel == HttpContext.Session.GetInt32("Nivel") && e.Preguntas.Count > 0).ToList();
            List<UsuarioExamen> usuarioExamenesExistentes = _context.UsuarioExamen.Where(ue => ue.UsuarioId == usuario.Id).ToList();

            foreach (Examen examen in examenes)
            {
                if (!usuarioExamenesExistentes.Any(ue => ue.ExamenId == examen.Id))
                {
                    UsuarioExamen usuarioExamenNuevo = new UsuarioExamen();
                    usuarioExamenNuevo.Examen = examen;
                    usuarioExamenNuevo.Usuario = usuario;
                    usuarioExamenNuevo.EstaAprobado = false;
                    usuarioExamenesExistentes.Add(usuarioExamenNuevo);
                    _context.Add(usuarioExamenNuevo);
                }
            }
            _context.SaveChanges();

            List<UsuarioExamen> usuarioExamenes = _context.UsuarioExamen.Include(ue => ue.Usuario).Include(ue => ue.Examen).Where(ue => ue.UsuarioId == usuario.Id).ToList();

            return View(usuarioExamenes);
        }

        public async Task<IActionResult> RendirExamen(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examenes.Include(e => e.Preguntas).ThenInclude(p => p.Opciones).FirstOrDefaultAsync(e => e.Id == id);

            return View(examen);
        }

        [HttpPost]
        public async Task<IActionResult> RendirExamenResultado(Examen examen)
        {

            ExamenResultado examenResultado = new ExamenResultado();
            Examen examenDB = _context.Examenes.Include(e=> e.Preguntas).ThenInclude(p=> p.Opciones).FirstOrDefault(e => e.Id == examen.Id);


            examenResultado.CantidadPreguntas = examen.Preguntas.Count();

            foreach (Pregunta preguntaDB in examenDB.Preguntas)
            {
                Pregunta preguntaForm = examen.Preguntas.First(p => p.Id == preguntaDB.Id);

                examenResultado.RespuestasCorrectas += preguntaDB.Opciones.FirstOrDefault(o => o.Id == preguntaForm.OpcionSeleccionada).EsCorrecta ? 1 : 0;
            }

            // 60% de respuestas correctas para aprobar
            if (examenResultado.Aprobado)
            {
                Usuario usuario = _context.Usuarios.Where(u => u.NombreUsuario == HttpContext.Session.GetString("Usuario")).SingleOrDefault();
                UsuarioExamen usuarioExamen = _context.UsuarioExamen.FirstOrDefault(ue => ue.UsuarioId == usuario.Id && ue.ExamenId == examen.Id);
                usuarioExamen.EstaAprobado = true;

                _context.SaveChanges();
            }
            return await RendirExamenResultadoView(examenResultado);
        }

        [HttpPost]
        public async Task<IActionResult> RendirExamenResultadoView(ExamenResultado examenResultado)
        {
            return View(examenResultado);
        }
    }
}

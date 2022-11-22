﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstitutoIdioma.Context;
using InstitutoIdioma.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace InstitutoIdioma.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly InstitutoDatabaseContext _context;

        public UsuarioController(InstitutoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreUsuario,Contrasenia,Email,FechaNacimiento,Dni,Nombre,Apellido")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreUsuario,Contrasenia,Email,FechaNacimiento,Dni,Nombre,Apellido,TipoPerfil,Nivel")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
        public IActionResult RegistroForm()
        {
            return View();
        }
        public IActionResult InicioForm()
        {
            return View();
        }

        public IActionResult Login(String usuario, String contra)
        {
            //Validar usuario y contraseña contra la tabla correspondiente en la BD
            //Si está OK, asignar el valor a la variable de sesion
            Usuario usu = BuscarUsuario(usuario);

            if (usu == null)
            {
                ViewBag.Error = "El usuario es inexistente";
                return View("InicioForm");
            }
            else if (contra != usu.Contrasenia)
            {
                ViewBag.ErrorContraseña = "La contraseña es incorrecta, vuelva a intentarlo";
                return View("InicioForm");
            }
            else
            {
                HttpContext.Session.SetString("Usuario", usuario);
                HttpContext.Session.SetInt32("Perfil", (int)usu.TipoPerfil);
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Logout()
        {

            HttpContext.Session.SetString("Usuario", string.Empty);
            HttpContext.Session.SetInt32("Perfil", 0);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrarse(String usuario, String contraseña, String contraseñanueva, String email, DateTime fechan, String dni, String nombre, String apellido)
        {
            Usuario usu = BuscarUsuario(usuario);
            if (usu == null)
            {
                if (contraseña == contraseñanueva)
                {
                    Usuario nuevo = new Usuario(usuario, contraseña, email, fechan, dni, nombre, apellido);
                    _context.Add(nuevo);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Usuario", new { usuario = nuevo.NombreUsuario, contra = nuevo.Contrasenia });
                }
                else
                {
                    ViewBag.Error = "Las contraseñas no coinciden";
                    return View("RegistroForm");

                }
            }
            else
            {
                ViewBag.Error = "El usuario ya existe. Intente con otro";
                return View("RegistroForm");
            }
        }




        private Usuario BuscarUsuario(String usuario)
        {
            //El metodo Single() convierne la coleccion en una unica entidad
            return _context.Usuarios.Where(u => u.NombreUsuario == usuario).SingleOrDefault();
        }

        private List<SelectListItem> ObtenerTodosLosUsuarios(int modo)
        {
            List<SelectListItem> usuarios = new List<SelectListItem>();

            List<Usuario> usuariosDB = new List<Usuario>();
            switch (modo)
            {
                case 1: // Perfiles
                    usuariosDB = _context.Usuarios.Where(u => (int)u.TipoPerfil < (int)TipoPerfil.ADMINISTRADOR).ToList();
                    break;
                case 2: // Niveles
                    
                    usuariosDB = _context.Usuarios.Where(u => (int)u.TipoPerfil < HttpContext.Session.GetInt32("Perfil")).ToList();
                    break;
            }


            foreach (Usuario usuario in usuariosDB)
            {
                SelectListItem item = new SelectListItem();
                item.Value = usuario.Id.ToString();
                switch (modo)
                {
                    case 1: // Perfiles
                        item.Text = string.Format("{0} ({1} {2}) - {3}", usuario.NombreUsuario, usuario.Nombre, usuario.Apellido, EnumExtensions.GetDescription(usuario.TipoPerfil));
                        break;
                    case 2: // Niveles
                        item.Text = string.Format("{0} ({1} {2}, {3}) - {4}", usuario.NombreUsuario, usuario.Nombre, usuario.Apellido, EnumExtensions.GetDescription(usuario.TipoPerfil), EnumExtensions.GetDescription(usuario.Nivel));
                        break;
                }
                usuarios.Add(item);
            }

            return usuarios;
        }

        public async Task<IActionResult> AdministrarPerfiles()
        {
            var model = new SelectorUsuario
            {
                Seleccionados = new[] { 0 },
                Usuarios = ObtenerTodosLosUsuarios(1)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AsignarPerfil([Bind("Seleccionados, Perfil")] SelectorUsuario usuariosPerfil)
        {

            await _context.Usuarios.Where(u => usuariosPerfil.Seleccionados.ToList().Contains(u.Id)).ForEachAsync(u => u.TipoPerfil = usuariosPerfil.Perfil);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AdministrarPerfiles));
        }

        public async Task<IActionResult> AdministrarNiveles()
        {
            var model = new SelectorUsuario
            {
                Seleccionados = new[] { 0 },
                Usuarios = ObtenerTodosLosUsuarios(2)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AsignarNivel([Bind("Seleccionados, NivelIdioma")] SelectorUsuario usuariosPerfil)
        {

            await _context.Usuarios.Where(u => usuariosPerfil.Seleccionados.ToList().Contains(u.Id)).ForEachAsync(u => u.Nivel = usuariosPerfil.NivelIdioma);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AdministrarNiveles));
        }

    }
}

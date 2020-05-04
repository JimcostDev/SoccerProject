using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectSoccerPoo.Models;

namespace ProjectSoccerPoo.Controllers
{
    public class JugadoresController : Controller
    {
        private ProjectSoccerPOOEntities db = new ProjectSoccerPOOEntities();

        // GET: Jugadores
        public async Task<ActionResult> Index()
        {
            var jugador = db.Jugador.Include(j => j.Equipo).Include(j => j.Posicion);
            return View(await jugador.ToListAsync());
        }

        // GET: Jugadores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = await db.Jugador.FindAsync(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // GET: Jugadores/Create
        public ActionResult Create()
        {
            ViewBag.equ_codigo = new SelectList(db.Equipo, "equ_codigo", "equ_nombre");
            ViewBag.pos_codigo = new SelectList(db.Posicion, "pos_codigo", "pos_descripcion");
            return View();
        }

        // POST: Jugadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "jug_codigo,equ_codigo,pos_codigo,jug_nombre,jug_primerApellido,jug_segundoApellido,jug_fechaNacimiento")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Jugador.Add(jugador);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.equ_codigo = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", jugador.equ_codigo);
            ViewBag.pos_codigo = new SelectList(db.Posicion, "pos_codigo", "pos_descripcion", jugador.pos_codigo);
            return View(jugador);
        }

        // GET: Jugadores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = await db.Jugador.FindAsync(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            ViewBag.equ_codigo = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", jugador.equ_codigo);
            ViewBag.pos_codigo = new SelectList(db.Posicion, "pos_codigo", "pos_descripcion", jugador.pos_codigo);
            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "jug_codigo,equ_codigo,pos_codigo,jug_nombre,jug_primerApellido,jug_segundoApellido,jug_fechaNacimiento")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jugador).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.equ_codigo = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", jugador.equ_codigo);
            ViewBag.pos_codigo = new SelectList(db.Posicion, "pos_codigo", "pos_descripcion", jugador.pos_codigo);
            return View(jugador);
        }

        // GET: Jugadores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = await db.Jugador.FindAsync(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Jugador jugador = await db.Jugador.FindAsync(id);
            db.Jugador.Remove(jugador);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

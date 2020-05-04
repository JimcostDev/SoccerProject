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
    public class PartidosController : Controller
    {
        private ProjectSoccerPOOEntities db = new ProjectSoccerPOOEntities();

        // GET: Partidos
        public async Task<ActionResult> Index()
        {
            var partido = db.Partido.Include(p => p.Equipo).Include(p => p.Equipo1).Include(p => p.Torneo);
            return View(await partido.ToListAsync());
        }

        // GET: Partidos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = await db.Partido.FindAsync(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        // GET: Partidos/Create
        public ActionResult Create()
        {
            ViewBag.equ_codigo_local = new SelectList(db.Equipo, "equ_codigo", "equ_nombre");
            ViewBag.equ_codigo_visitante = new SelectList(db.Equipo, "equ_codigo", "equ_nombre");
            ViewBag.tor_codigo = new SelectList(db.Torneo, "tor_codigo", "tor_nombre");
            return View();
        }

        // POST: Partidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "par_codigo,equ_codigo_visitante,equ_codigo_local,tor_codigo,par_fecha,par_horaInicio,par_horaFin,par_golesLocal,par_golesVisitante")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Partido.Add(partido);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.equ_codigo_local = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", partido.equ_codigo_local);
            ViewBag.equ_codigo_visitante = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", partido.equ_codigo_visitante);
            ViewBag.tor_codigo = new SelectList(db.Torneo, "tor_codigo", "tor_nombre", partido.tor_codigo);
            return View(partido);
        }

        // GET: Partidos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = await db.Partido.FindAsync(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            ViewBag.equ_codigo_local = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", partido.equ_codigo_local);
            ViewBag.equ_codigo_visitante = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", partido.equ_codigo_visitante);
            ViewBag.tor_codigo = new SelectList(db.Torneo, "tor_codigo", "tor_nombre", partido.tor_codigo);
            return View(partido);
        }

        // POST: Partidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "par_codigo,equ_codigo_visitante,equ_codigo_local,tor_codigo,par_fecha,par_horaInicio,par_horaFin,par_golesLocal,par_golesVisitante")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partido).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.equ_codigo_local = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", partido.equ_codigo_local);
            ViewBag.equ_codigo_visitante = new SelectList(db.Equipo, "equ_codigo", "equ_nombre", partido.equ_codigo_visitante);
            ViewBag.tor_codigo = new SelectList(db.Torneo, "tor_codigo", "tor_nombre", partido.tor_codigo);
            return View(partido);
        }

        // GET: Partidos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = await db.Partido.FindAsync(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Partido partido = await db.Partido.FindAsync(id);
            db.Partido.Remove(partido);
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

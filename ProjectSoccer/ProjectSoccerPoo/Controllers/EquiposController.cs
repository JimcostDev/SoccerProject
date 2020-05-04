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
    public class EquiposController : Controller
    {
        private ProjectSoccerPOOEntities db = new ProjectSoccerPOOEntities();

        // GET: Equipos
        public async Task<ActionResult> Index()
        {
            var equipo = db.Equipo.Include(e => e.Entrenador).Include(e => e.Liga);
            return View(await equipo.ToListAsync());
        }

        // GET: Equipos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = await db.Equipo.FindAsync(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.ent_codigo = new SelectList(db.Entrenador, "ent_codigo", "ent_primerApellido");
            ViewBag.lig_codigo = new SelectList(db.Liga, "lig_codigo", "lig_descripcion");
            return View();
        }

        // POST: Equipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "equ_codigo,equ_nombre,ent_codigo,lig_codigo")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipo.Add(equipo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ent_codigo = new SelectList(db.Entrenador, "ent_codigo", "ent_nombres", equipo.ent_codigo);
            ViewBag.lig_codigo = new SelectList(db.Liga, "lig_codigo", "lig_descripcion", equipo.lig_codigo);
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = await db.Equipo.FindAsync(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ent_codigo = new SelectList(db.Entrenador, "ent_codigo", "ent_primerApellido", equipo.ent_codigo);
            ViewBag.lig_codigo = new SelectList(db.Liga, "lig_codigo", "lig_descripcion", equipo.lig_codigo);
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "equ_codigo,equ_nombre,ent_codigo,lig_codigo")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ent_codigo = new SelectList(db.Entrenador, "ent_codigo", "ent_nombres", equipo.ent_codigo);
            ViewBag.lig_codigo = new SelectList(db.Liga, "lig_codigo", "lig_descripcion", equipo.lig_codigo);
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = await db.Equipo.FindAsync(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Equipo equipo = await db.Equipo.FindAsync(id);
            db.Equipo.Remove(equipo);
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

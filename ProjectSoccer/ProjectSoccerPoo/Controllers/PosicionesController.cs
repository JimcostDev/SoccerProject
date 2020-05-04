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
    public class PosicionesController : Controller
    {
        private ProjectSoccerPOOEntities db = new ProjectSoccerPOOEntities();

        // GET: Posiciones
        public async Task<ActionResult> Index()
        {
            return View(await db.Posicion.ToListAsync());
        }

        // GET: Posiciones/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posicion posicion = await db.Posicion.FindAsync(id);
            if (posicion == null)
            {
                return HttpNotFound();
            }
            return View(posicion);
        }

        // GET: Posiciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posiciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "pos_codigo,pos_descripcion")] Posicion posicion)
        {
            if (ModelState.IsValid)
            {
                db.Posicion.Add(posicion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(posicion);
        }

        // GET: Posiciones/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posicion posicion = await db.Posicion.FindAsync(id);
            if (posicion == null)
            {
                return HttpNotFound();
            }
            return View(posicion);
        }

        // POST: Posiciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "pos_codigo,pos_descripcion")] Posicion posicion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posicion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(posicion);
        }

        // GET: Posiciones/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posicion posicion = await db.Posicion.FindAsync(id);
            if (posicion == null)
            {
                return HttpNotFound();
            }
            return View(posicion);
        }

        // POST: Posiciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Posicion posicion = await db.Posicion.FindAsync(id);
            db.Posicion.Remove(posicion);
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

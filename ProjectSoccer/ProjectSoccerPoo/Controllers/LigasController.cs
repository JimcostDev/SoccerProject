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
    public class LigasController : Controller
    {
        private ProjectSoccerPOOEntities db = new ProjectSoccerPOOEntities();

        // GET: Ligas
        public async Task<ActionResult> Index()
        {
            return View(await db.Liga.ToListAsync());
        }

        // GET: Ligas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liga liga = await db.Liga.FindAsync(id);
            if (liga == null)
            {
                return HttpNotFound();
            }
            return View(liga);
        }

        // GET: Ligas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ligas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "lig_codigo,lig_descripcion")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                db.Liga.Add(liga);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(liga);
        }

        // GET: Ligas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liga liga = await db.Liga.FindAsync(id);
            if (liga == null)
            {
                return HttpNotFound();
            }
            return View(liga);
        }

        // POST: Ligas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "lig_codigo,lig_descripcion")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liga).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(liga);
        }

        // GET: Ligas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liga liga = await db.Liga.FindAsync(id);
            if (liga == null)
            {
                return HttpNotFound();
            }
            return View(liga);
        }

        // POST: Ligas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Liga liga = await db.Liga.FindAsync(id);
            db.Liga.Remove(liga);
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

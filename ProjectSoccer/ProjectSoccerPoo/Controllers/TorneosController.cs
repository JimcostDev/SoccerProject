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
    public class TorneosController : Controller
    {
        private ProjectSoccerPOOEntities db = new ProjectSoccerPOOEntities();
        

        // GET: Torneos
        public async Task<ActionResult> Index()
        {
            return View(await db.Torneo.ToListAsync());
        }

        // GET: Torneos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = await db.Torneo.FindAsync(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // GET: Torneos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Torneos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "tor_codigo,tor_fechaInicio,tor_fechaFinal,tor_nombre")] Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                db.Torneo.Add(torneo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(torneo);
        }

        // GET: Torneos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = await db.Torneo.FindAsync(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // POST: Torneos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "tor_codigo,tor_fechaInicio,tor_fechaFinal,tor_nombre")] Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(torneo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(torneo);
        }

        // GET: Torneos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = await db.Torneo.FindAsync(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // POST: Torneos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Torneo torneo = await db.Torneo.FindAsync(id);
            db.Torneo.Remove(torneo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: Torneos/GetPositions/5
        [HttpGet]
        public ActionResult GetPositions()
        {
            //Ejecutar prodedimiento de almacenado
            var tablaPosiciones = db.ObtenerPosiciones();
            return View(tablaPosiciones);
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

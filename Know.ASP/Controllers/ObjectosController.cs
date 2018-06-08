using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Know.ASP;

namespace Know.ASP.Controllers
{
    public class ObjectosController : Controller
    {
        private dbConocimientoEntities db = new dbConocimientoEntities();

        // GET: Objectos
        public async Task<ActionResult> Index()
        {
            var objecto = db.Objecto.Include(o => o.Animado).Include(o => o.Color).Include(o => o.Genero);
            return View(await objecto.ToListAsync());
        }

        // GET: Objectos/Details/5
        public async Task<ActionResult> Details(string? id)
        {
            if (id.Equals(""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objecto objecto = await db.Objecto.FindAsync(id);
            if (objecto == null)
            {
                return HttpNotFound();
            }
            return View(objecto);
        }

        // GET: Objectos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Obj = new SelectList(db.Animado, "Id_Obj", "Estado");
            ViewBag.Id_Obj = new SelectList(db.Color, "Id_Obj", "Color1");
            ViewBag.Id_Obj = new SelectList(db.Genero, "Id_Obj", "Genero1");
            return View();
        }

        // POST: Objectos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Obj,Nombre,Definicion")] Objecto objecto)
        {
            if (ModelState.IsValid)
            {
                db.Objecto.Add(objecto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Obj = new SelectList(db.Animado, "Id_Obj", "Estado", objecto.Id_Obj);
            ViewBag.Id_Obj = new SelectList(db.Color, "Id_Obj", "Color1", objecto.Id_Obj);
            ViewBag.Id_Obj = new SelectList(db.Genero, "Id_Obj", "Genero1", objecto.Id_Obj);
            return View(objecto);
        }

        // GET: Objectos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objecto objecto = await db.Objecto.FindAsync(id);
            if (objecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Obj = new SelectList(db.Animado, "Id_Obj", "Estado", objecto.Id_Obj);
            ViewBag.Id_Obj = new SelectList(db.Color, "Id_Obj", "Color1", objecto.Id_Obj);
            ViewBag.Id_Obj = new SelectList(db.Genero, "Id_Obj", "Genero1", objecto.Id_Obj);
            return View(objecto);
        }

        // POST: Objectos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Obj,Nombre,Definicion")] Objecto objecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objecto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Obj = new SelectList(db.Animado, "Id_Obj", "Estado", objecto.Id_Obj);
            ViewBag.Id_Obj = new SelectList(db.Color, "Id_Obj", "Color1", objecto.Id_Obj);
            ViewBag.Id_Obj = new SelectList(db.Genero, "Id_Obj", "Genero1", objecto.Id_Obj);
            return View(objecto);
        }

        // GET: Objectos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objecto objecto = await db.Objecto.FindAsync(id);
            if (objecto == null)
            {
                return HttpNotFound();
            }
            return View(objecto);
        }

        // POST: Objectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Objecto objecto = await db.Objecto.FindAsync(id);
            db.Objecto.Remove(objecto);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionProyectoCS.Models.Datos;

namespace GestionProyectoCS.Controllers
{
    public class MARCAController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();

        // GET: MARCA
        public ActionResult Index()
        {
            var rEGISTRO_MARCA = db.REGISTRO_MARCA.Include(r => r.EMPLEADO);
            return View(rEGISTRO_MARCA.ToList());
        }

        // GET: MARCA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_MARCA rEGISTRO_MARCA = db.REGISTRO_MARCA.Find(id);
            if (rEGISTRO_MARCA == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRO_MARCA);
        }

        // GET: MARCA/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES");
            return View();
        }

        // POST: MARCA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_REGISTRO_MARCA,ID_EMPLEADO,FECHA,H_ENTE,H_SALE,H_ENTA,H_SALA,H_ENTS,H_SALS,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] REGISTRO_MARCA rEGISTRO_MARCA)
        {
            if (ModelState.IsValid)
            {
                db.REGISTRO_MARCA.Add(rEGISTRO_MARCA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES", rEGISTRO_MARCA.ID_EMPLEADO);
            return View(rEGISTRO_MARCA);
        }

        // GET: MARCA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_MARCA rEGISTRO_MARCA = db.REGISTRO_MARCA.Find(id);
            if (rEGISTRO_MARCA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES", rEGISTRO_MARCA.ID_EMPLEADO);
            return View(rEGISTRO_MARCA);
        }

        // POST: MARCA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_REGISTRO_MARCA,ID_EMPLEADO,FECHA,H_ENTE,H_SALE,H_ENTA,H_SALA,H_ENTS,H_SALS,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] REGISTRO_MARCA rEGISTRO_MARCA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEGISTRO_MARCA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES", rEGISTRO_MARCA.ID_EMPLEADO);
            return View(rEGISTRO_MARCA);
        }

        // GET: MARCA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_MARCA rEGISTRO_MARCA = db.REGISTRO_MARCA.Find(id);
            if (rEGISTRO_MARCA == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRO_MARCA);
        }

        // POST: MARCA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REGISTRO_MARCA rEGISTRO_MARCA = db.REGISTRO_MARCA.Find(id);
            db.REGISTRO_MARCA.Remove(rEGISTRO_MARCA);
            db.SaveChanges();
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

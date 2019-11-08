using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionProyectoCS.Models.Datos;
using Microsoft.AspNet.Identity;

namespace GestionProyectoCS.Controllers
{
    [AccessDeniedAuthorize(Roles = "Administrador")]
    public class RESPONSABLEController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();

        // GET: RESPONSABLE
        public ActionResult Index()
        {
            return View(db.RESPONSABLEs.ToList());
        }

        // GET: RESPONSABLE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESPONSABLE rESPONSABLE = db.RESPONSABLEs.Find(id);
            if (rESPONSABLE == null)
            {
                return HttpNotFound();
            }
            return View(rESPONSABLE);
        }

        // GET: RESPONSABLE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RESPONSABLE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_RESPONSABLE,DESCRIPCION,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] RESPONSABLE rESPONSABLE)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                rESPONSABLE.ACTIVO = true;
                rESPONSABLE.USUARIO_CREACION = usuario;
                rESPONSABLE.FECHA_CREACION = DateTime.Now;
                db.RESPONSABLEs.Add(rESPONSABLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rESPONSABLE);
        }

        // GET: RESPONSABLE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESPONSABLE rESPONSABLE = db.RESPONSABLEs.Find(id);
            if (rESPONSABLE == null)
            {
                return HttpNotFound();
            }
            return View(rESPONSABLE);
        }

        // POST: RESPONSABLE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_RESPONSABLE,DESCRIPCION,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] RESPONSABLE rESPONSABLE)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                var responsablebd = db.RESPONSABLEs.Find(rESPONSABLE.ID_RESPONSABLE);
                responsablebd.DESCRIPCION = rESPONSABLE.DESCRIPCION;

                rESPONSABLE.USUARIO_MODIFICACION = usuario;
                rESPONSABLE.FECHA_MODIFICACION = DateTime.Now;
                //db.Entry(rESPONSABLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rESPONSABLE);
        }

        // GET: RESPONSABLE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESPONSABLE rESPONSABLE = db.RESPONSABLEs.Find(id);
            if (rESPONSABLE == null)
            {
                return HttpNotFound();
            }
            return View(rESPONSABLE);
        }

        // POST: RESPONSABLE/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string usuario = User.Identity.GetUserName();
            RESPONSABLE rESPONSABLE = db.RESPONSABLEs.Find(id);
            rESPONSABLE.FECHA_MODIFICACION = DateTime.Now;
            rESPONSABLE.USUARIO_MODIFICACION = usuario;
            //db.RESPONSABLEs.Remove(rESPONSABLE);
            rESPONSABLE.ACTIVO = false;
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

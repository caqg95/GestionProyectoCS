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
    public class CARGOController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();

        // GET: CARGO
        public ActionResult Index()
        {
            return View(db.CARGOes.ToList());
        }

        // GET: CARGO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = db.CARGOes.Find(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // GET: CARGO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CARGO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CARGO,DESCRIPCION,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] CARGO cARGO)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                cARGO.USUARIO_CREACION = usuario;
                cARGO.FECHA_CREACION = DateTime.Now;
                cARGO.ACTIVO = true;
                db.CARGOes.Add(cARGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cARGO);
        }

        // GET: CARGO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = db.CARGOes.Find(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // POST: CARGO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CARGO,DESCRIPCION,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] CARGO cARGO)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                CARGO cargodb = db.CARGOes.Find(cARGO.ID_CARGO);
                cargodb.DESCRIPCION = cARGO.DESCRIPCION;
                cargodb.USUARIO_MODIFICACION = usuario;
                cargodb.FECHA_MODIFICACION = DateTime.Now;              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cARGO);
        }

        // GET: CARGO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = db.CARGOes.Find(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // POST: CARGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string usuario = User.Identity.GetUserName();
            CARGO cARGO = db.CARGOes.Find(id);
            cARGO.ACTIVO = false;
            cARGO.USUARIO_MODIFICACION = usuario;
            cARGO.FECHA_MODIFICACION = DateTime.Now;
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

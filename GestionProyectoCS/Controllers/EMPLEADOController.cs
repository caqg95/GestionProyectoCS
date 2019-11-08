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
    public class EMPLEADOController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();

        // GET: EMPLEADO
        public ActionResult Index()
        {
            var eMPLEADOes = db.EMPLEADOes.Include(e => e.CARGO);
            return View(eMPLEADOes.ToList());
        }

        // GET: EMPLEADO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADOes.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // GET: EMPLEADO/Create
        public ActionResult Create()
        {
            ViewBag.ID_CARGO = new SelectList(db.CARGOes, "ID_CARGO", "DESCRIPCION");
            return View();
        }

        // POST: EMPLEADO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_EMPLEADO,ID_CARGO,NOMBRES,APELLIDOS,IDENTIDAD,COLOR,TELEF1,TELEF2,EMAIL1,EMAIL2,ID_CALENDAR,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                eMPLEADO.ACTIVO = true;
                eMPLEADO.USUARIO_CREACION = usuario;
                eMPLEADO.FECHA_CREACION = DateTime.Now;
                db.EMPLEADOes.Add(eMPLEADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CARGO = new SelectList(db.CARGOes, "ID_CARGO", "DESCRIPCION", eMPLEADO.ID_CARGO);
            return View(eMPLEADO);
        }

        // GET: EMPLEADO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADOes.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CARGO = new SelectList(db.CARGOes, "ID_CARGO", "DESCRIPCION", eMPLEADO.ID_CARGO);
            return View(eMPLEADO);
        }

        // POST: EMPLEADO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_EMPLEADO,ID_CARGO,NOMBRES,APELLIDOS,IDENTIDAD,COLOR,TELEF1,TELEF2,EMAIL1,EMAIL2,ID_CALENDAR,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                EMPLEADO empleadodb = db.EMPLEADOes.Find(eMPLEADO.ID_EMPLEADO);
                empleadodb.NOMBRES = eMPLEADO.NOMBRES;
                empleadodb.APELLIDOS = eMPLEADO.APELLIDOS;
                empleadodb.IDENTIDAD = eMPLEADO.IDENTIDAD;
                empleadodb.COLOR = eMPLEADO.COLOR;
                empleadodb.TELEF1 = eMPLEADO.TELEF1;
                empleadodb.TELEF2 = eMPLEADO.TELEF2;
                empleadodb.EMAIL1 = eMPLEADO.EMAIL1;
                empleadodb.EMAIL2 = eMPLEADO.EMAIL2;
                empleadodb.ID_CALENDAR = eMPLEADO.ID_CALENDAR;

                eMPLEADO.USUARIO_MODIFICACION = usuario;
                eMPLEADO.FECHA_MODIFICACION = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CARGO = new SelectList(db.CARGOes, "ID_CARGO", "DESCRIPCION", eMPLEADO.ID_CARGO);
            return View(eMPLEADO);
        }

        // GET: EMPLEADO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADOes.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // POST: EMPLEADO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string usuario = User.Identity.GetUserName();
            EMPLEADO eMPLEADO = db.EMPLEADOes.Find(id);
            eMPLEADO.ACTIVO = false;
            eMPLEADO.FECHA_MODIFICACION = DateTime.Now;
            eMPLEADO.USUARIO_MODIFICACION = usuario;
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

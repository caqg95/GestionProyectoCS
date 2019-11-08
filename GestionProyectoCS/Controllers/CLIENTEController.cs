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
    public class CLIENTEController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();

        // GET: CLIENTE
        public ActionResult Index()
        {
            return View(db.CLIENTEs.ToList());
        }

        // GET: CLIENTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTEs.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // GET: CLIENTE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CLIENTE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CLIENTE,COD_CLIENTE,CLIENTE1,ACTIVO,DIRECCION,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                cLIENTE.ACTIVO = true;
                cLIENTE.USUARIO_CREACION = usuario;
                cLIENTE.FECHA_CREACION = DateTime.Now;
                db.CLIENTEs.Add(cLIENTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cLIENTE);
        }

        // GET: CLIENTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTEs.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // POST: CLIENTE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CLIENTE,COD_CLIENTE,CLIENTE1,ACTIVO,DIRECCION,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                CLIENTE clientedb = db.CLIENTEs.Find(cLIENTE.ID_CLIENTE);
                clientedb.COD_CLIENTE = cLIENTE.COD_CLIENTE;
                clientedb.CLIENTE1 = cLIENTE.CLIENTE1;
                clientedb.DIRECCION = cLIENTE.DIRECCION;
                clientedb.USUARIO_MODIFICACION = usuario;
                clientedb.FECHA_MODIFICACION = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLIENTE);
        }

        // GET: CLIENTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTEs.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // POST: CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string usuario = User.Identity.GetUserName();
            CLIENTE cLIENTE = db.CLIENTEs.Find(id);
            cLIENTE.FECHA_MODIFICACION = DateTime.Now;
            cLIENTE.USUARIO_MODIFICACION = usuario;
            cLIENTE.ACTIVO = false;
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

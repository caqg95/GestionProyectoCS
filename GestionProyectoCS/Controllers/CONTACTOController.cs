
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
    public class CONTACTOController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();

        // GET: CONTACTO
        public ActionResult Index()
        {
            var cONTACTOes = db.CONTACTOes.Include(c => c.CLIENTE).Include(c => c.RESPONSABLE);
            return View(cONTACTOes.ToList());
        }

        // GET: CONTACTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // GET: CONTACTO/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "CLIENTE1");
            ViewBag.ID_RESPONSABLE = new SelectList(db.RESPONSABLEs, "ID_RESPONSABLE", "DESCRIPCION");
            return View();
        }

        // POST: CONTACTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CONTACTO,NOMBRES,APELLIDOS,TELF1,TELF2,EXT,EMAIL1,EMAIL2,ACTIVO,ID_CLIENTE,ID_RESPONSABLE,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] CONTACTO cONTACTO)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                cONTACTO.ACTIVO = true;
                cONTACTO.FECHA_CREACION = DateTime.Now;
                cONTACTO.USUARIO_CREACION = usuario;
                db.CONTACTOes.Add(cONTACTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "COD_CLIENTE", cONTACTO.ID_CLIENTE);
            ViewBag.ID_RESPONSABLE = new SelectList(db.RESPONSABLEs, "ID_RESPONSABLE", "DESCRIPCION", cONTACTO.ID_RESPONSABLE);
            return View(cONTACTO);
        }

        // GET: CONTACTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "CLIENTE1", cONTACTO.ID_CLIENTE);
            ViewBag.ID_RESPONSABLE = new SelectList(db.RESPONSABLEs, "ID_RESPONSABLE", "DESCRIPCION", cONTACTO.ID_RESPONSABLE);
            return View(cONTACTO);
        }

        // POST: CONTACTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CONTACTO,NOMBRES,APELLIDOS,TELF1,TELF2,EXT,EMAIL1,EMAIL2,ACTIVO,ID_CLIENTE,ID_RESPONSABLE,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] CONTACTO cONTACTO)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                var contactobd = db.CONTACTOes.Find(cONTACTO.ID_CONTACTO);
                contactobd.NOMBRES = cONTACTO.NOMBRES;
                contactobd.APELLIDOS = cONTACTO.APELLIDOS;
                contactobd.TELF1 = cONTACTO.TELF1;
                contactobd.TELF2 = cONTACTO.TELF2;
                contactobd.EXT = cONTACTO.EXT;
                contactobd.EMAIL1 = cONTACTO.EMAIL1;
                contactobd.EMAIL2 = cONTACTO.EMAIL2;
                contactobd.ID_CLIENTE = cONTACTO.ID_CLIENTE;
                contactobd.ID_RESPONSABLE = cONTACTO.ID_RESPONSABLE;
                contactobd.USUARIO_MODIFICACION = usuario;
                contactobd.FECHA_MODIFICACION = DateTime.Now;
                //db.Entry(cONTACTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "COD_CLIENTE", cONTACTO.ID_CLIENTE);
            ViewBag.ID_RESPONSABLE = new SelectList(db.RESPONSABLEs, "ID_RESPONSABLE", "DESCRIPCION", cONTACTO.ID_RESPONSABLE);
            return View(cONTACTO);
        }

        // GET: CONTACTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // POST: CONTACTO/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string usuario = User.Identity.GetUserName();
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            cONTACTO.ACTIVO = false;
            cONTACTO.FECHA_MODIFICACION = DateTime.Now;
            cONTACTO.USUARIO_MODIFICACION = usuario;
            //db.CONTACTOes.Remove(cONTACTO);
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

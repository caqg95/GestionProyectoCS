using DevExpress.Web.Mvc;
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
    public class AgendaController : Controller
    {
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();
        rptAgendaClientecs report = new rptAgendaClientecs();
        // GET: Agenda
        public ActionResult Index()
        {
            var vISITAs = db.SpListadoVistasCS().ToList();
            return View(vISITAs.ToList());
        }
        public ActionResult VisitaEmpleado()
        {
            string usuario = User.Identity.GetUserName();
            var usuariodb = db.AspNetUsers.Where(x => x.UserName == usuario).FirstOrDefault();
            var vISITAs = db.SpListadoVistasCS().Where(x => x.ID_EMPLEADO == usuariodb.ID_EMPELADO).ToList();
            return View(vISITAs.ToList());
        }
        public ActionResult DetalleVisita(int id)
        {
            //string usuario = User.Identity.GetUserName();
            //var usuariodb = db.AspNetUsers.Where(x => x.UserName == usuario).FirstOrDefault();
            //var vISITAs = db.SpListadoVistasCS().Where(x => x.ID_EMPLEADO == usuariodb.ID_EMPELADO).ToList();
            return View();
        }

        public ActionResult Agenda()
        {
            var vISITAs = db.VISITAs.Include(v => v.CLIENTE).Include(v => v.EMPLEADO);
            return View(vISITAs.ToList());
        }
        //public ActionResult FormartoAgendaCliente(int? consulta)
        //{

        //    rptAgendaClientecs report = new rptAgendaClientecs();
        //    //report.RequestParameters = false;
        //    //report.Parameters[0].Value = consulta;
        //    report.Parameters[0].Visible = true;

        //    //tool.ShowPreviewDialog();
        //    //report.Parameters[1].Value = consulta;
        //    //report.Parameters[1].Visible = false;

        //    return View(report);
        //}
        public ActionResult Test()
        {
            //var vISITAs = db.VISITAs.Include(v => v.CLIENTE).Include(v => v.EMPLEADO);
            return View();
        }
        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VISITA vISITA = db.VISITAs.Find(id);
            if (vISITA == null)
            {
                return HttpNotFound();
            }
            return View(vISITA);
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "COD_CLIENTE");
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES");
            return View();
        }

        // POST: Agenda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_VISITA,ID_CLIENTE,ID_EMPLEADO,DESCRIPCION,ESTADO,FECHA_INICIO,FECHA_FIN,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] VISITA vISITA)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                vISITA.USUARIO_CREACION = usuario;
                vISITA.FECHA_CREACION = DateTime.Now;
                db.VISITAs.Add(vISITA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "COD_CLIENTE", vISITA.ID_CLIENTE);
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES", vISITA.ID_EMPLEADO);
            return View(vISITA);
        }

        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VISITA vISITA = db.VISITAs.Find(id);
            if (vISITA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "COD_CLIENTE", vISITA.ID_CLIENTE);
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES", vISITA.ID_EMPLEADO);
            return View(vISITA);
        }

        // POST: Agenda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_VISITA,ID_CLIENTE,ID_EMPLEADO,DESCRIPCION,ESTADO,FECHA_INICIO,FECHA_FIN,ACTIVO,USUARIO_CREACION,FECHA_CREACION,USUARIO_MODIFICACION,FECHA_MODIFICACION")] VISITA vISITA)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.GetUserName();
                vISITA.USUARIO_MODIFICACION = usuario;
                vISITA.FECHA_MODIFICACION = DateTime.Now;
                db.Entry(vISITA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTEs, "ID_CLIENTE", "COD_CLIENTE", vISITA.ID_CLIENTE);
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOes, "ID_EMPLEADO", "NOMBRES", vISITA.ID_EMPLEADO);
            return View(vISITA);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VISITA vISITA = db.VISITAs.Find(id);
            if (vISITA == null)
            {
                return HttpNotFound();
            }
            return View(vISITA);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            VISITA vISITA = db.VISITAs.Find(id);
            string usuario = User.Identity.GetUserName();
            vISITA.USUARIO_MODIFICACION = usuario;
            vISITA.FECHA_MODIFICACION = DateTime.Now;
            db.VISITAs.Remove(vISITA);
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
        public JsonResult GetEventsAll()
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true).ToList();
                    //var events = db.CITAS.ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                    //JsonResult eventsJSON = new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //return eventsJSON;
                }
                catch
                {
                    return null;
                }
            }
        }
        [HttpPost]
        public JsonResult GetEventsEmpleadoClienteEstado(int empleado, int cliente, int estado)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_EMPLEADO == empleado && c.ID_CLIENTE == cliente && c.ID_ESTADO == estado).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEventsEmpleado(int empleado)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_EMPLEADO == empleado).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEventsCliente(int cliente)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_CLIENTE == cliente).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEventsEstado(int estado)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_ESTADO == estado).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEventsEmpleadoCliente(int empleado, int cliente)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_EMPLEADO == empleado && c.ID_CLIENTE == cliente).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEventsClienteEstado(int cliente, int estado)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_CLIENTE == cliente && c.ID_ESTADO == estado).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEventsEmpleadoEstado(int empleado, int estado)
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var events = db.SpListadoVistasCS().Where(c => c.ACTIVO == true && c.ID_EMPLEADO == empleado && c.ID_ESTADO == estado).ToList();
                    return Json(events.Select(x => new
                    {
                        ID_VISITA = x.ID_VISITA,
                        ID_CLIENTE = x.ID_CLIENTE,
                        CLIENTE = x.CLIENTE,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        EMPLEADO = x.NOMBRES + " " + x.APELLIDOS,
                        ASUNTO = x.ASUNTO + "-" + x.NOMBRES + " " + x.APELLIDOS,
                        DESCRIPCION = x.DESCRIPCION,
                        ESTADOID = x.ID_ESTADO,
                        ESTADO = x.ESTADO,
                        Start = x.FECHA_INICIO,
                        End = x.FECHA_FIN,
                        COLOR = x.COLOR,
                        ID_CALENDAR = x.ID_CALENDAR,
                        ID_EVENT = x.ID_EVENT
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }
            }
        }
        public JsonResult GetEmpleados()
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var EMPLEADO = db.EMPLEADOes.Where(x => x.ACTIVO == true).Select(x => new
                    {
                        ID = x.ID_EMPLEADO,
                        NOMBRE = x.NOMBRES + " " + x.APELLIDOS
                    }).ToList();
                    return Json(EMPLEADO.Select(x => new
                    {
                        ID = x.ID,
                        NOMBRE = x.NOMBRE
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }

            }
        }
        public JsonResult GetClientes()
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var CLIENTE = db.CLIENTEs.Where(x => x.ACTIVO == true).Select(x => new
                    {
                        ID = x.ID_CLIENTE,
                        NOMBRE = x.CLIENTE1
                    }).ToList();
                    return Json(CLIENTE.Select(x => new
                    {
                        ID = x.ID,
                        NOMBRE = x.NOMBRE
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }

            }
        }
        public JsonResult GetEstados()
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    var ESTADO = db.ESTADOes.Where(x => x.ACTIVO == true).Select(x => new
                    {
                        ID = x.ID_ESTADO,
                        NOMBRE = x.DESCRIPCION
                    }).ToList();
                    return Json(ESTADO.Select(x => new
                    {
                        ID = x.ID,
                        NOMBRE = x.NOMBRE
                    }), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return null;
                }

            }
        }

        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                var v = dc.VISITAs.Where(a => a.ID_VISITA == eventID).FirstOrDefault();
                string usuario = User.Identity.GetUserName();
                if (v != null)
                {
                    v.ACTIVO = false;
                    v.USUARIO_MODIFICACION = usuario;
                    v.FECHA_MODIFICACION = DateTime.Now;
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult SaveEvent(VISITA e)
        {
            var status = false;
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                e.ACTIVO = true;
                string usuario = User.Identity.GetUserName();
                if (e.ID_VISITA > 0)
                {
                    //Update the event
                    var v = dc.VISITAs.Where(a => a.ID_VISITA == e.ID_VISITA).FirstOrDefault();
                    if (v != null)
                    {
                        string[] asunto = e.ASUNTO.Split('-');
                        v.ASUNTO = asunto[0];
                        v.ID_EMPLEADO = e.ID_EMPLEADO;
                        v.ID_CLIENTE = e.ID_CLIENTE;
                        v.ID_ESTADO = e.ID_ESTADO;
                        v.FECHA_INICIO = e.FECHA_INICIO;
                        v.FECHA_FIN = e.FECHA_FIN;
                        v.DESCRIPCION = e.DESCRIPCION;
                        v.USUARIO_MODIFICACION = usuario;
                        v.FECHA_MODIFICACION = DateTime.Now;
                        v.ID_EVENT = e.ID_EVENT;

                    }
                }
                else
                {
                    e.ID_ESTADO = 1;
                    e.ASUNTO = e.ASUNTO;
                    e.ACTIVO = true;
                    e.USUARIO_CREACION = usuario;
                    e.FECHA_CREACION = DateTime.Now;
                    dc.VISITAs.Add(e);
                }
                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult GetCalendarAddress(int cliente, int empleado)
        {
            var status = true;
            string address = string.Empty;
            string calendarid = string.Empty;
            string nombre = string.Empty;
            try
            {
                using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
                {
                    calendarid = db.EMPLEADOes.Find(empleado).ID_CALENDAR.ToString();
                    address = db.CLIENTEs.Find(cliente).DIRECCION.ToString();
                    nombre = db.CLIENTEs.Find(cliente).CLIENTE1.ToString();
                }
            }
            catch
            {
                status = true;
            }
            return new JsonResult { Data = new { status = status, address = address, calendarid = calendarid, cliente = nombre } };
        }
        public ActionResult FormartoAgendaCliente(int cliente, DateTime fi, DateTime ff)
        {
            rptAgendaClientecs report = new rptAgendaClientecs();
            report.RequestParameters = false;
            report.Parameters[0].Value = cliente;
            report.Parameters[0].Visible = false;

            report.Parameters[1].Value = fi;
            report.Parameters[1].Visible = false;

            report.Parameters[2].Value = ff;
            report.Parameters[2].Visible = false;

            return View(report);
        }
        public ActionResult FormartoAgendaEmpleado(int empleado, DateTime fi, DateTime ff)
        {
            rptAgendaEmpleadoCS report = new rptAgendaEmpleadoCS();
            report.RequestParameters = false;
            report.Parameters[0].Value = empleado;
            report.Parameters[0].Visible = false;

            report.Parameters[1].Value = fi;
            report.Parameters[1].Visible = false;

            report.Parameters[2].Value = ff;
            report.Parameters[2].Visible = false;

            return View(report);
        }
        public ActionResult AgendaCliente()
        {
            return View();
        }
        public ActionResult AgendaEmpleado()
        {
            return View();
        }
    }
}

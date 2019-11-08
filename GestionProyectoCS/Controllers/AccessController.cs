using GestionProyectoCS.Models;
using GestionProyectoCS.Models.Datos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GestionProyectoCS.Controllers
{
    public class AccessController : Controller
    {
        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Usuario O Contrasena no valida.";
                return View(model);
            }
            bool activite_user = false;
            //No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            var Users = SignInManager.UserManager.Users;
            foreach (var user in Users)
            {
                if (user.UserName == model.UserName)
                {
                    activite_user = user.isActive;
                    break;
                }
            }
            if (activite_user)
            {
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Usuario O Contrasena no valida.");
                        ViewBag.Message = "Usuario O Contrasena no valida.";
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario O Contrasena no valida.");
                ViewBag.Message = "Usuario O Contrasena no valida.";
                return View(model);
            }
            // return Redirect("/Home/Index");

        }
        public ActionResult Denied()
        {
            return View();
        }
        //
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Access");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public JsonResult GetUserDetail()
        {
            using (GESTIONP_CSEntities dc = new GESTIONP_CSEntities())
            {
                try
                {
                    string  usuario=User.Identity.GetUserName();
                    var usuariobd = dc.SpListadoUsuarioDetail().Where(x => x.UserName == usuario).FirstOrDefault();
                    
                    return Json(usuariobd, JsonRequestBehavior.AllowGet);
                    //JsonResult eventsJSON = new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //return eventsJSON;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
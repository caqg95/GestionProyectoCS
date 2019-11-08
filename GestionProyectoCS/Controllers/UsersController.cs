using GestionProyectoCS.IdentityExtensions;
using GestionProyectoCS.Models;
using GestionProyectoCS.Models.Datos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GestionProyectoCS.Controllers
{
    [AccessDeniedAuthorize(Roles = "Administrador")]
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private GESTIONP_CSEntities db = new GESTIONP_CSEntities();
        public AppUserManager UserManager { get; private set; }

        public UsersController() : this(new AppUserManager())
        {
        }
        public UsersController(AppUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        //
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
        //
        // GET: /Account/UserWithRol
        public async Task<ActionResult> Users()
        {
            List<UserWithRolViewModel> list = new List<UserWithRolViewModel>();
            var Users = SignInManager.UserManager.Users.ToList();
            foreach (var user in Users)
            {
                UserWithRolViewModel uservm = new UserWithRolViewModel();
                try
                {
                    var RolesForUser = await UserManager.GetRolesAsync(user.Id);
                    uservm.Role = RolesForUser[0].ToString();
                }
                catch
                {
                    uservm.Role = string.Empty;
                }
                uservm.Id = user.Id;
                uservm.UserName = user.UserName;
                uservm.Name = user.Name;
                uservm.isActive = user.isActive;
                list.Add(uservm);

            }
            return View(list);
        }
        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Name = model.Name, isActive = model.isActive };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                    // Enviar correo electrónico con este vínculo
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                    return RedirectToAction("Users", "Users");
                }
                AddErrors(result);
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
        //

        public async Task<ActionResult> EditUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            UserWithRolViewModel uservm = new UserWithRolViewModel();
            var RolesForUser = await UserManager.GetRolesAsync(user.Id);
            uservm.Id = user.Id;
            uservm.UserName = user.UserName;
            uservm.Name = user.Name;
            uservm.Role = RolesForUser[0].ToString();
            uservm.isActive = user.isActive;

            //List<SelectListItem> list = new List<SelectListItem>();
            //foreach (var role in RoleManager.Roles)
            //    list.Add(new SelectListItem() { Value = role.Id.ToString(), Text = role.Name.ToString() });
            //var roed= RoleManager.Roles.ToList();
            ViewBag.Role = db.AspNetRoles.Select(x => new { Value = x.Name, Text = x.Name }).ToList();
            //ViewBag.RoleName = new SelectList(RoleManager.Roles,"Id","Name");
            return View(uservm);
        }
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserWithRolViewModel model, string roles)
        {
            if (!ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.UserName = model.UserName;
                user.Name = model.Name;
                user.isActive = model.isActive;

                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var RolesForUser = await UserManager.GetRolesAsync(model.Id);
                    string RoleName = RolesForUser[0].ToString();
                    result = await UserManager.RemoveFromRoleAsync(user.Id, RoleName);
                    await UserManager.AddToRoleAsync(user.Id, roles);

                    if (model.Password != string.Empty)
                    {

                        await UserManager.RemovePasswordAsync(model.Id);
                        var reslut = await UserManager.AddPasswordAsync(model.Id, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Users", "Users");
                        }
                        AddErrors(result);
                        return View(model);

                    }
                    return RedirectToAction("Users", "Users");
                }
                else
                {
                    AddErrors(result);
                }

            }
            return View(model);
        }
        //
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }

}
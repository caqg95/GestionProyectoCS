
using GestionProyectoCS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace GestionProyectoCS.IdentityExtensions
{
    public class AppUserManager : UserManager<ApplicationUser>
    {
        public AppUserManager()
         : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
            PasswordValidator = new CustomizePasswordValidation(6);
        }
    }
}
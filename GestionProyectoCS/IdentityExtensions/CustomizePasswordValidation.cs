using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace GestionProyectoCS.IdentityExtensions
{
    public class CustomizePasswordValidation : IIdentityValidator<string>
    {

        public int LengthRequired { get; set; }

        public CustomizePasswordValidation(int length)
        {
            LengthRequired = length;
        }

        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (String.IsNullOrEmpty(item) || item.Length < LengthRequired)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("La contraseña debe terner una longitud minima de:{0}", LengthRequired)));
            }

            /*string PasswordPattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";

            if (!Regex.IsMatch(item, PasswordPattern))
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("The Password must have at least one numeric and one special character")));
            }
            */
            return Task.FromResult(IdentityResult.Success);

        }
    }
}
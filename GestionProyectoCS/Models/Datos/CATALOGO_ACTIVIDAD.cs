//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionProyectoCS.Models.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class CATALOGO_ACTIVIDAD
    {
        public int ID_ACTIVIDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public string TIPO { get; set; }
        public Nullable<int> ID_ACTIVIDAD_PADRE { get; set; }
        public Nullable<decimal> CANT_MIN_HORA { get; set; }
        public Nullable<decimal> CANT_MAX_HORA { get; set; }
        public string COMPLEJIDAD { get; set; }
        public bool ACTIVO { get; set; }
        public string USUARIO_CREACION { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public string USUARIO_MODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    }
}

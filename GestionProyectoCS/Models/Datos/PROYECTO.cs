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
    
    public partial class PROYECTO
    {
        public int ID_PROYECTO { get; set; }
        public int ID_CLIENTE { get; set; }
        public string DESCRIPCION { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public Nullable<decimal> LONGITUD { get; set; }
        public Nullable<decimal> LATITUD { get; set; }
        public Nullable<int> EMPLEADO_RESPONSABLE { get; set; }
        public string OBSERVACION { get; set; }
        public bool ACTIVO { get; set; }
        public int USUARIO_CREACION { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public Nullable<int> USUARIO_MODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
    }
}

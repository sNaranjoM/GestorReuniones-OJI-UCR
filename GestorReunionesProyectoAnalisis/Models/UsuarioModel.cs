using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAnalisis.Models
{
    public class UsuarioModel
    {
        
        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string Cedula { get; set; }

        public string Usuario { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public string Oficina { get; set; }

        public string TC_Nombre_Oficina { get; set; }

        public string Puesto { get; set; }

        public string TC_Nombre_Puesto { get; set; }

        public string Rol { get; set; }

        public string TC_Nombre_Rol { get; set; }

        public int Permisos { get; set; }

        public int IdUsuario { get; set; }

        public int Asistencia { get; set; }

    }
}

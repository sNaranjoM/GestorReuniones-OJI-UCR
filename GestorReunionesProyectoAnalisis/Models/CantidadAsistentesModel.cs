using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAnalisis.Models
{
    public class CantidadAsistentesModel
    {
        public string mes { get; set; }

        public string anno { get; set; }

        public string asistencia { get; set; }

        public string invitados { get; set; }

        public int TN_Usuarios_Asistieron { get; set; }

        public int TN_Usuarios_Faltaron { get; set; }

        public int Dinero { get; set; }

        public string Tiempo { get; set; }
    }
}

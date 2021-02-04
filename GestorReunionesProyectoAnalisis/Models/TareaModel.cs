using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAnalisis.Models
{
    public class TareaModel
    {
        public string TC_Nombre_Tarea { get; set; }

        public string TC_Descripcion_Tarea { get; set; }

        public string listaUsuarios { get; set; }

        public int TN_Id_Tarea { get; set; }

        public int TB_Estado { get; set; }

        public string TC_Acuerdo { get; set; }

        public string TC_Nombre_Usuario { get; set; }

        public string TC_Primer_Apellido { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAnalisis.Models
{
    public class TipoReunionModel
    {
        public string TC_Nombre_Tipo_Reunion { get; set; }

        public int TN_Id_Tipo_Reunion { get; set; }

        public int TN_Cantidad_Tipo_Reunion { get; set; }

        public int mes { get; set; }

        public int anno { get; set; }

    }
}

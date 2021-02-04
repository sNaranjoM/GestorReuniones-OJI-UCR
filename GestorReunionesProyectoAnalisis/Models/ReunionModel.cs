using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAnalisis.Models
{
    public class ReunionModel
    {
        public string TC_Nombre_Reunion { get; set; }

        public int TN_Id_Reunion { get; set; }

        public int TN_Id_Tipo_Reunion { get; set; }

        public string TC_Nombre_Tipo_Reunion { get; set; }

        public string TC_Descripcion { get; set; }

        public string TC_Comentario { get; set; }

        public string TC_Lugar { get; set; }

        public DateTime TC_Fecha_Reunion { get; set; }

        public string TC_Lista_Temas { get; set; }

        public string TC_Lista_Usuarios { get; set; }

        public string TC_Lista_Tareas { get; set; }

        public string TC_Lista_Archivos { get; set; }

        public string TC_Fecha_Inicio { get; set; }

        public string TC_Fecha_Final { get; set; }

        public string TC_Nombre_Usuario { get; set; }

        public string TC_Nombre_Tema { get; set; }

        public string TC_Acuerdo { get; set; }

        public string Estado { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAnalisis.Models
{
    public class OficinaModel
    {
        public string TC_Nombre { get; set; }

        public string TC_Codigo { get; set; }

        public int TN_Id_Circuito { get; set; }

        public DateTime TF_Inicio_Vigencia { get; set; }                                

        public int TN_Id_Oficina { get; set; }
    }
}

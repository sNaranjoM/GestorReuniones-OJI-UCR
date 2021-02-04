using GestorReunionesProyectoAnalisis.Data;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GestorReunionesProyectoAnalisis.Business
{
    public class BusinessArchivos
    {
        public IConfiguration Configuration { get; }

        public BusinessArchivos(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor
        public List<ArchivoModel> listarArchivos(string idElemento)
        {

            DataArchivo dataArchivo = new DataArchivo(Configuration);
            List<ArchivoModel> ListaArchivosReunion = new List<ArchivoModel>();

            ListaArchivosReunion = dataArchivo.listarArchivos(idElemento);
            return ListaArchivosReunion;
        }

        public bool EliminarArchivo(string TN_Id_Archivo)
        {

            DataArchivo dataArchivo = new DataArchivo(Configuration);

            return dataArchivo.EliminarArchivo(TN_Id_Archivo);
        }
    }
}

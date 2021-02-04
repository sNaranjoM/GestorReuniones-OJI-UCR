using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using GestorReunionesProyectoAnalisis.Business;

namespace GestorReunionesProyectoAnalisis.Controllers
{
    public class ArchivoController : Controller
    {

        private readonly IHostingEnvironment hostingEnvironment;

        public IConfiguration Configuration { get; }

        public ArchivoController(IConfiguration configuration, IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
            Configuration = configuration;
        } // constructor



        //Recibe una lista de tipo IFormFile, desde java script, esto para cargar los archivos selecionados una carpeta del proyecto
        public void AgregarArchivo(IList<IFormFile> files)
        {

            //Se asigna la ruta por defecto donde van a caer estos archivos, para esto se utiliza el id de la reunion que este creando o modificando
            //Con el fin que la carpeta de cada reunion tenga el nombre con su respectivo id
            string ruta = "files/Reuniones/TN_Id_Reunion_" + HttpContext.Session.GetString("IdReunion");
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, ruta);

            //valida si el archivo existe sino es asi entra al if y lo crea
            if (!Directory.Exists(uploads))
            {
                Console.WriteLine("Creando el directorio: {0}", uploads);
                //Crea el archivo
                DirectoryInfo di = Directory.CreateDirectory(uploads);
            }

            //guarda en la caperte todos los archivos que venian por parametro
            foreach (var arch in files)
            {

                var filePath = Path.Combine(uploads, arch.FileName);
                FileStream f = new FileStream(filePath, FileMode.Create);
                arch.CopyTo(f);
                f.Close();

            }

            //Limpia la variable en session de la reunion
            HttpContext.Session.Remove("IdReunion");
            //HttpContext.Session.SetString("IdReunion", null);

        }


        //Lista  archivos segun id de una reunion 
        public List<ArchivoModel> listarArchivos(string idElemento)
        {

            BusinessArchivos businessArchivos = new BusinessArchivos(Configuration);
            List<ArchivoModel> ListaArchivosReunion = new List<ArchivoModel>();

            //Lista los archivos 
            ListaArchivosReunion = businessArchivos.listarArchivos(idElemento);
            return ListaArchivosReunion;
        }

        //Elimina los archivos de una reunion en especifico
        public bool EliminarArchivo(string TN_Id_Archivo)
        {

            BusinessArchivos dataArchivo = new BusinessArchivos(Configuration);
            //eliminar los archivos y retorna true si lo hizo o false sino lo logro.
            return dataArchivo.EliminarArchivo(TN_Id_Archivo);
        }

    }
}

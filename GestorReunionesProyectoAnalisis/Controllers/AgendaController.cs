using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Business;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestorReunionesProyectoAplicada.Controllers
{
    public class AgendaController : Controller
    {
        public IConfiguration Configuration { get; }

        public AgendaController(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        public IActionResult Index()
        {
            return View();
        }

        //Carga la vista de la agenda personal
        public IActionResult ViewAgendaPersonal()
        {

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
            List<ReunionModel> listaReuniones = new List<ReunionModel>();           

            //Retorna todas las reuniones que tenga el usuario que este en linea
            listaReuniones = businessAgenda.getReuniones(HttpContext.Session.GetString("UsuarioLogin"));
            ViewBag.ListaReuniones = listaReuniones;
           
            return View("Calendario");
            
            
        }


        //Lista los asistentes que posee una reunion
        public IActionResult CargarAsistentesReunion(string IdReunionAsistentes)
        {

            BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
            List<UsuarioModel> ListaAsistentesReunion = new List<UsuarioModel>();
            //lista los asistentes
            ListaAsistentesReunion = businessAgenda.getAsistentesReunion(IdReunionAsistentes);

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            
            List<ReunionModel> listaReuniones = new List<ReunionModel>();

            //Lista nuevamente las reuniones para que se vean en el calendario
            listaReuniones = businessAgenda.getReuniones(HttpContext.Session.GetString("UsuarioLogin"));
           
            //valida si la lista es != null crea el ViewBag que lleva la lista de usuarios de lo contrario no lo crea y no mostraria ninguna lista en interfaz
            if (ListaAsistentesReunion != null)
            {
                ViewBag.ListaReuniones = listaReuniones;
                ViewBag.ListaUsuarios = ListaAsistentesReunion;
                return View("Calendario");
            }
            else
            {
                ViewBag.ListaReuniones = listaReuniones;
                return View("Calendario");
            }                           

        }





        //Lista de las tareas que posee una reunion
        public IActionResult CargarTareasReunion(string IdReunionTareas)
        {
           
            BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
            List<TareaModel> ListaTareasReunion = new List<TareaModel>();
            //lista de tareas
            ListaTareasReunion = businessAgenda.getTareasReunion(IdReunionTareas);

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            List<ReunionModel> listaReuniones = new List<ReunionModel>();

            //Lista las reuniones que tenga el usuario que esta en linea para ponerlas en el calendario
            listaReuniones = businessAgenda.getReuniones(HttpContext.Session.GetString("UsuarioLogin"));
            ViewBag.ListaReuniones = listaReuniones;

            //valida la existencia de la lista de tareas para retornarlas a interfaz
            if (ListaTareasReunion != null)
            {
                ViewBag.ListaTareas = ListaTareasReunion;
                return View("Calendario");
            }
            else
            {
                return View("Calendario");
            }

        }

        //Lista de los archivos que posee una reunion
        public IActionResult CargarArchivosReunion(string IdReunionArchivos)
        {    

            List<ArchivoModel> listaArchivos = new List<ArchivoModel>();
            BusinessArchivos businessArchivos = new BusinessArchivos(Configuration);
            //lista archivos
            listaArchivos = businessArchivos.listarArchivos(IdReunionArchivos);

            BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);            
            List<ReunionModel> listaReuniones = new List<ReunionModel>();

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            //Lista las reuniones que tenga el usuario que esta en linea para ponerlas en el calendario
            listaReuniones = businessAgenda.getReuniones(HttpContext.Session.GetString("UsuarioLogin"));
            ViewBag.ListaReuniones = listaReuniones;

            //valida la existencia de la lista de archivos para retornarlos a interfaz
            if (listaArchivos != null)
            {
                ViewBag.ListaArchivos = listaArchivos;
                return View("Calendario");
            }
            else
            {
                return View("Calendario");
            }

        }

    }
}

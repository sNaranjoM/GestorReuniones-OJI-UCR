using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestorReunionesProyectoAplicada.Controllers
{
    public class TareaController : Controller
    {

        public IConfiguration Configuration { get; }

        public TareaController(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        public IActionResult Index()
        {
            return View();
        }


        // Mostrar la vista de crear Tarea
        public IActionResult viewCrearTarea()
        {
            
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            //Se trae toda la lista de usuarios para luego poder crear la tarea en la interfaz
            listaUsuarios = businessUsuario.getListarUsuario();

            ViewBag.ListaUsuarios = listaUsuarios;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("CrearTareaView");
        }


        //Se encarga de mostrar todas la tareas que se han creado para poder eliminarlas o modificarlas
        public IActionResult viewBuscarTarea()
        {
            List<TareaModel> listaTareas= new List<TareaModel>();

            BusinessTarea businessTarea = new BusinessTarea(Configuration);
            //Lista las tareas de la base de datos
            listaTareas = businessTarea.getListarTarea();

            ViewBag.ListaTareas = listaTareas;
            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("BuscarTareasView");
        }


        //Trae la tarea de la base de datos que el usuarios seleciono
        public IActionResult viewModificarTarea(string idTarea)
        {

            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);

            //Lista los usuarios, por si se desea modificar los usuarios de esa tarea
            listaUsuarios = businessUsuario.getListarUsuario();

            ViewBag.ListaUsuarios = listaUsuarios;


            BusinessTarea businessTarea = new BusinessTarea(Configuration);
            TareaModel tareaModificar = new TareaModel();

            //Lista la tarea que el usuario solicito modificar atravez del id de la misma
            tareaModificar = businessTarea.getTareaModificar(idTarea);

            ViewBag.TareaModificar = tareaModificar;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("ModificarTareaView");
            //return new JsonResult(idTarea);
            //return new JsonResult(ViewBag.TareaModificar.TC_Nombre_Tarea);
        }


        //Eliminar una tarea segun su id
        public IActionResult EliminarTarea(string idTarea)
        {

            List<TareaModel> listaTareas = new List<TareaModel>();
            BusinessTarea businessTarea = new BusinessTarea(Configuration);

            //Elimina la tarea y retorna la lista de todas la tareas, de esta manera en interfaz se podra ver si efectivamente se borro
            listaTareas = businessTarea.EliminarTarea(idTarea);

            ViewBag.ListaTareas = listaTareas;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("BuscarTareasView");
           
        }
        

        //Se encarga de agregar la tarea a la base de datos
        [HttpPost]
        public IActionResult CrearTarea(TareaModel tareaModel)
        {
                  
            BusinessTarea businessTarea = new BusinessTarea(Configuration);
            //Se encarga de crear la tarea en caso de exito retorna un true y le notifica al usuario del exito de la operacion
            bool valido = businessTarea.CrearTarea(tareaModel);

            if (valido)
            {
                //return a java script
                return new JsonResult("Si Creo");
            }
            else
            {
                //return a java script
                return new JsonResult("No guardo");                
            }
            
        }


        //Modificar una tarea 
        [HttpPost]
        public IActionResult ModificarTarea(TareaModel tareaModel)
        {
            
            BusinessTarea businessTarea = new BusinessTarea(Configuration);

            //Si la tarea fue modificada retorna true y le notifica al usuario del exito de la operacion
            bool valido = businessTarea.ModificarTarea(tareaModel);

            if (valido)
            {
                //return a java script
                return new JsonResult("Si modifico");
            }
            else
            {
                //return a java script
                return new JsonResult("No guardo");                
            }
           
        }
    }
}

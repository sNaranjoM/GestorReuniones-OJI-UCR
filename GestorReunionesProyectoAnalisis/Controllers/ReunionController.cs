using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Business;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rotativa.AspNetCore;

namespace GestorReunionesProyectoAplicada.Controllers
{
    public class ReunionController : Controller
    {
        public IConfiguration Configuration { get; }

        public ReunionController(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        public IActionResult Index()
        {
            return View();
        }


        //Muestra las vista para crear la reunion
        public IActionResult viewCrearReunion()
        {
            List<TipoReunionModel> listaTiposReunion = new List<TipoReunionModel>();
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            List<TareaModel> listaTareas = new List<TareaModel>();

            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            BusinessTarea businessTarea = new BusinessTarea(Configuration);

            //Lista las tareas, usuarios y los tipos de reunion para poder crearla luego en interfaz
            listaTiposReunion = businessCatalogo.getListarTipoReunion();
            listaUsuarios = businessUsuario.getListarUsuario();
            listaTareas = businessTarea.getListarTarea();
            
            //Validar si las listas no son nulas sino no llevarlas a interfaz
            if (listaTiposReunion != null) {
                ViewBag.ListaTiposReunion = listaTiposReunion;
            }

            if (listaUsuarios != null)
            {
                ViewBag.ListaUsuarios = listaUsuarios;
            }

            if (listaTareas != null)
            {
                ViewBag.ListaTareas = listaTareas;
            }

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("CrearReunionView");
        }


        //Se encarga de crear la reunion en la base de datos
        [HttpPost]
        public IActionResult CrearReunion(ReunionModel reunionModel)
        {
                     
            //Se trae la variable en Session del id del usuario que esta en linea para asi poder saber quien creo la reunion
            var idUsuario = HttpContext.Session.GetString("UsuarioLogin");
            BusinessReunion businessReunion = new BusinessReunion(Configuration);

            int[] respuesta = { 0, 0 };

            //valida si ese usuario no se a desconectado
            if (idUsuario != null)
            {
                //Crea la reunion y retorna en respuesta [0] si se efectuo la creacion de la reunion 
                respuesta = businessReunion.CrearReunion(reunionModel, idUsuario);
            }
            else {
                respuesta[0] = 0;
            }

            
            //si se creo va a ser igual a 1 de lo contrario envia un mensaje que no pudo guardar la reunion
            if (respuesta[0] == 1)
            {
                string idReunion = "" + respuesta[1];

                //Crea una session con el id de la reunion que se creo
                HttpContext.Session.SetString("IdReunion", idReunion);

                return new JsonResult("Si Creo");
            }
            else
            {
                return new JsonResult("No guardo");
            }
            
        }


        //Eliminar una reunion
        public IActionResult EliminarReunion(string idReunion)
        {

            List<ReunionModel> listaReuniones = new List<ReunionModel>();
            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //elimina la reunion y retorna la lista de todas las reuniones para asi validar desde interfaz que la misma ya no exista
            listaReuniones = businessReunion.EliminarReunion(idReunion);

            ViewBag.ListaReuniones = listaReuniones;
            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("BuscaReunionView");
        }


        //Modificar una reunion
        [HttpPost]
        public IActionResult ModificarReunion(ReunionModel reunionModel)
        {
           
            BusinessReunion businessReunion = new BusinessReunion(Configuration);

            //Retorna true si se pudo modificar la reunion
            bool valido = businessReunion.ModificarReunion(reunionModel);
           
            if (valido)
            {
                string idReunion = "" + reunionModel.TN_Id_Reunion;
                //Crea una session con el id de la reunion modificada
                HttpContext.Session.SetString("IdReunion", idReunion);

                return new JsonResult("Si Creo");
            }
            else
            {
                return new JsonResult("No guardo");
            }
        
        }


        //Se encarga de mostrarle al usuario todas las reuniones que hay para eliminar y modificar
        public IActionResult viewBuscaReunion()
        {
            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            List<ReunionModel> listaReuniones = new List<ReunionModel>();

            //Lista todas las reuniones existentes de la base de datos
            listaReuniones = businessReunion.getListarReunion();

            ViewBag.ListaReuniones = listaReuniones;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("BuscaReunionView");
        }


        //Se encarga de retornar todas las reuniones que ya estan finalizandas
        public IActionResult viewMostrarReunion()
        {
            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            List<ReunionModel> listaReuniones = new List<ReunionModel>();

            //retorna las reuniones finalizadas
            listaReuniones = businessReunion.getListarReunionFinalizadas();
            ViewBag.ListaReuniones = listaReuniones;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("MostrarReunionView");
        }

        //Se encarga de mostrar el resumen de una reunion en especifico a travez de su id
        public IActionResult viewResumenReunion(string idReunion)
        {

            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            List<TareaModel> listaTareas = new List<TareaModel>();
            List<ArchivoModel> listaArchivos = new List<ArchivoModel>();

            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            BusinessTarea businessTarea = new BusinessTarea(Configuration);
            BusinessArchivos businessArchivos = new BusinessArchivos(Configuration);

            //Lista los usuarios, las tareas y los archivos de la reunion que solicito el usuario
            listaUsuarios = businessUsuario.getListarUsuarioReunion(idReunion);//Listar usuarios del idReunion
            listaTareas = businessTarea.getListarTareaReunion(idReunion);//Listar usuarios del idReunion
            listaArchivos = businessArchivos.listarArchivos(idReunion);

            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //Se crea un objeto de tipo ReunionModel para asi poder extraer toda las iformacion de esa reunion
            ReunionModel reunionesModel = new ReunionModel();
            reunionesModel = businessReunion.ResumenReunion(idReunion);


            ViewBag.ListaUsuarios = listaUsuarios;
            ViewBag.ListaTareas = listaTareas;
            ViewBag.ListaArchivos = listaArchivos;
            ViewBag.reunionesModel = reunionesModel;

            BusinessDashboard businessDashboard = new BusinessDashboard(Configuration);

            //Se crea un objeto para guardar dinero tiempo y asistentes
            CantidadAsistentesModel cantidadAsistentesModel = new CantidadAsistentesModel();

            //Traer la cantidad de personas que asistienron y las que no esta reunion
            cantidadAsistentesModel = businessDashboard.getAsistenciaReunionUnica(idReunion);
            //Traer dinero invertido
            int dinero = businessDashboard.getDineroReunionUnica(idReunion);
            cantidadAsistentesModel.Dinero = dinero;
            //Traer tiempo invertido
            cantidadAsistentesModel.Tiempo = businessDashboard.getDuracionReunionUnica(idReunion);
            ViewBag.datosDashboard = cantidadAsistentesModel;
            
            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("ResumenReunionView");
        }


        //Se encarga de traer los datos de una reunion que desea modificar el usuario
        public IActionResult ViewModificarReunion(string idReunion, string Estado)
        {

            List<TipoReunionModel> listaTiposReunion = new List<TipoReunionModel>();
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            List<TareaModel> listaTareas = new List<TareaModel>();
            List<ArchivoModel> listaArchivos = new List<ArchivoModel>();

            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            BusinessTarea businessTarea = new BusinessTarea(Configuration);
            BusinessArchivos businessArchivos = new BusinessArchivos(Configuration);

            // Lista los tipos de reunion, todos los usuarios , todas la tareas, y todos los archivos que tenga esa reunion por si el usuario desea modificarlos
            //que pueda hacerlo
            listaTiposReunion = businessCatalogo.getListarTipoReunion();
            listaUsuarios = businessUsuario.getListarUsuario();
            listaTareas = businessTarea.getListarTarea();
            listaArchivos = businessArchivos.listarArchivos(idReunion);

            ViewBag.ListaTiposReunion = listaTiposReunion;
            ViewBag.ListaUsuarios = listaUsuarios;
            ViewBag.ListaTareas = listaTareas;
            ViewBag.ListaArchivos = listaArchivos;

            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            ReunionModel reunionModificar = new ReunionModel();

            //Trae la reunion que el usuario solicito
            reunionModificar = businessReunion.getReunionModificar(idReunion);
            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            
            //Si la reunion ya finalizo la misma no se puede modificar
           
            if (Estado == "True")
            {
                //Para avisarle al usuario que no puede modificar una renunion que ya se finalizo
                ViewBag.ESTADO = "La reunion ya a sido finalizada no se pueden realizar modificaciones";
                reunionModificar = null;
            }

            if (reunionModificar != null)
            {
                //Retorna la reunion que se desea modificar
                ViewBag.ReunionModificar = reunionModificar;
                return View("ModificarReunionView");
            }
            else
            {
                //En caso que estuviera finalizada se retorna el error que se creo arriba y la lista de todas las reuniones que se pueden eliminar y modificar
                
                List<ReunionModel> listaReuniones = new List<ReunionModel>();                
                listaReuniones = businessReunion.getListarReunion();
                ViewBag.ListaReuniones = listaReuniones;                        

                return View("BuscaReunionView");
            }


        }


        //Se realiza una consulta a la base de datos con el fin validar si la reunion que seleciono el usuario ya esta disponible para ingresar,         
        [HttpPost]
        public IActionResult viewEjecucionReunion(string IdReunionAsistir)
        {

            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //Retornando un true sobre valido si puede entrar y retornando si pude entrar a javaScript
            //Para asi direccionar hacia su siguiente vista o bien quedar en la misma            
            bool valido = businessReunion.validarFechaReunion(IdReunionAsistir, HttpContext.Session.GetString("UsuarioLogin"));

            if (valido)
            {
                //Se crea una session sobre el id de la reunion que esta en ejecucion.
                HttpContext.Session.SetString("IdReunionIniciarEjecucion", IdReunionAsistir);
                return new JsonResult("Si puede entrar");
                
            }
            else {

                return new JsonResult("Reunion no disponible");
            }           

        }


        //Se encarga de traer todos los datos de la reunion a la que el usuario desea ingresar
        //En el metodo de arriba el usuario valida si puede ingresar si es asi desde java script lo lleva a este metodo y carga la reunion
        public IActionResult ViewReunionEnEjecucion()
        {

            //Trae el id de la reunion que escogio el usuario
            string IdReunionAsistir = HttpContext.Session.GetString("IdReunionIniciarEjecucion");
            BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
            List<TareaModel> ListaTareasReunion = new List<TareaModel>();
            //Trae las tareas de esa reunion
            ListaTareasReunion = businessAgenda.getTareasReunion(IdReunionAsistir);
            ViewBag.ListaTareas = ListaTareasReunion;

            List<UsuarioModel> ListaAsistentesReunion = new List<UsuarioModel>();
            //Trae la lista de usuarios de la reunion
            ListaAsistentesReunion = businessAgenda.getAsistentesReunion(IdReunionAsistir);
            ViewBag.ListaUsuarios = ListaAsistentesReunion;

            BusinessArchivos businessArchivos = new BusinessArchivos(Configuration);
            List<ArchivoModel> listaArchivos = new List<ArchivoModel>();
            //Trae la lista de archivos de la reunion
            listaArchivos = businessArchivos.listarArchivos(IdReunionAsistir);
            ViewBag.ListaArchivos = listaArchivos;

            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            ReunionModel reunionAsistir = new ReunionModel();

            //Trae los datos de la reunion a la cual desea asistir
            reunionAsistir = businessReunion.getReunionModificar(IdReunionAsistir);
            ViewBag.ReunionAsistir = reunionAsistir;

            List<TemasModel> listaTemas = new List<TemasModel>();
            //Trae la lista de temas de la reunion
            listaTemas = businessReunion.ListarTemasReunion(IdReunionAsistir);
            if (listaTemas != null) {
                ViewBag.ListaTemas = listaTemas;
            }
           
            string usuarioCreadorReunion = businessReunion.UsuarioCreadorReunion(IdReunionAsistir);
            

            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            
            //valida si la persona que esta ingresando es el creador de la reunion para darle permiso de terminarla.
            if (usuarioCreadorReunion == HttpContext.Session.GetString("UsuarioLogin")) {
                ViewBag.CreadorReunion = usuarioCreadorReunion;
                return View("EjecucionReunionView");
            }
            else {
                return View("EjecucionReunionUsuarioEstandar");
            }

        }


        //Retorna la informacion necesaria para realizar la creacion de la minuta en la interfaz
        [HttpPost]
        public IActionResult mostrarMinutaReunion(string idReunion)
        {

            //Se crea una lista de listas para poder llevar toda la informacion a travez del return new ViewAsPdf
            List<Object> listaDeListas = new List<object>();

            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            //lista de usuarios de la reunion
            listaUsuarios = businessUsuario.getUsuarioMinuta(idReunion);

            List<TareaModel> listaTareas = new List<TareaModel>();
            BusinessTarea businessTarea = new BusinessTarea(Configuration);
            //lista de tareas de la reunion
            listaTareas = businessTarea.getListarTareaMinuta(idReunion);

            ReunionModel infoReunion = new ReunionModel();
            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //retorna todos los datos de la reunion que se solicita atravez del id
            infoReunion = businessReunion.getReunionMinuta(idReunion);

            List<ReunionModel> listaTemas = new List<ReunionModel>();
            //lista de temas de la reunion
            listaTemas = businessReunion.getTemasMinuta(idReunion);

            listaDeListas.Add(listaUsuarios);
            listaDeListas.Add(listaTareas);
            listaDeListas.Add(infoReunion);
            listaDeListas.Add(listaTemas);

            //retorna la lista de listas
            return new ViewAsPdf("/Views/Reunion/MinutaReunion.cshtml", listaDeListas)
            {
            };

        }



        //Salvar los acuerdos de los temas solo lo puede hacer el que creo la reunion
        [HttpPost]
        public IActionResult AgregarAcuerdosTemas(string idTemas, string acuerdoTema)
        {

            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //Retorna True si el tema se salvo con exito de lo contrario se le retorna al usuario que no se salvo correctamente
            bool valido = businessReunion.AgregarAcuerdosTemas(idTemas,acuerdoTema);

            if (valido)
            {
                return new JsonResult("Inserto");
            }
            {
                return new JsonResult("No Inserto");
            }



        }


        //Salvar los acuerdos de las tareas solo lo puede hacer el que creo la reunion
        [HttpPost]
        public IActionResult AgregarAcuerdosTareas(string idTarea, string acuerdoTarea)
        {

            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //Retorna True si la tarea se salvo con exito de lo contrario se le retorna al usuario que no se salvo correctamente
            bool valido = businessReunion.AgregarAcuerdosTareas(idTarea, acuerdoTarea);

            if (valido) {
                return new JsonResult("Inserto");
            }
            {
                return new JsonResult("No Inserto");
            }           


        }


        //Se encarga de finalizar la ejecucion de la reunion
        [HttpPost]
        public IActionResult TerminarReunion(string IdReunionTerminada)
        {
            BusinessReunion businessReunion = new BusinessReunion(Configuration);
            //Retorna true si la reunion se pude terminar correctamente
            bool valido = businessReunion.TerminarReunion(IdReunionTerminada);
            if (valido)
            {
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

                BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
                List<ReunionModel> listaReuniones = new List<ReunionModel>();

                //Traer la lista de reuniones que tiene en la agenda el usuario que esta en linea.
                listaReuniones = businessAgenda.getReuniones(HttpContext.Session.GetString("UsuarioLogin"));
                ViewBag.ListaReuniones = listaReuniones;

                return View("../Agenda/Calendario");

            }
            else {

                //En caso de que falle
                string IdReunionAsistir = HttpContext.Session.GetString("IdReunionIniciarEjecucion");
                BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
                List<TareaModel> ListaTareasReunion = new List<TareaModel>();
                // trae la lista de tareas de la reunion
                ListaTareasReunion = businessAgenda.getTareasReunion(IdReunionAsistir);
                ViewBag.ListaTareas = ListaTareasReunion;

                List<UsuarioModel> ListaAsistentesReunion = new List<UsuarioModel>();
                // trae la lista de usuarios de la reunion
                ListaAsistentesReunion = businessAgenda.getAsistentesReunion(IdReunionAsistir);
                ViewBag.ListaUsuarios = ListaAsistentesReunion;

                BusinessArchivos businessArchivos = new BusinessArchivos(Configuration);
                List<ArchivoModel> listaArchivos = new List<ArchivoModel>();
                // trae la lista de archivos de la reunion
                listaArchivos = businessArchivos.listarArchivos(IdReunionAsistir);
                ViewBag.ListaArchivos = listaArchivos;
                
                ReunionModel reunionAsistir = new ReunionModel();
                //trae la reunion en la que esta asitiendo
                reunionAsistir = businessReunion.getReunionModificar(IdReunionAsistir);
                ViewBag.ReunionAsistir = reunionAsistir;

                List<TemasModel> listaTemas = new List<TemasModel>();
                // trae la lista de temas de la reunion
                listaTemas = businessReunion.ListarTemasReunion(IdReunionAsistir);
                if (listaTemas != null)
                {
                    ViewBag.ListaTemas = listaTemas;
                }


                string usuarioCreadorReunion = businessReunion.UsuarioCreadorReunion(IdReunionAsistir);
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                //Se valida si el usuario es el creador de la reunion
                if (usuarioCreadorReunion == HttpContext.Session.GetString("UsuarioLogin"))
                {
                    ViewBag.CreadorReunion = usuarioCreadorReunion;
                    return View("EjecucionReunionView");
                }
                else
                {
                    return View("EjecucionReunionUsuarioEstandar");
                }

            }

        }

    }
}

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
    public class CatalogoController : Controller
    {
        public IConfiguration Configuration { get; }

        public CatalogoController(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        
        public IActionResult VistaCrearCatalogos()
        {
            return View("CrearCatalogos");
        }


        //TIPOS DE REUNION DEL SISTEMA

        // Carga la vista de tipo de reunion, con una lista de los tipos que esten en la base de datos
        // y envia el rol que tiene este usuario

        public IActionResult viewTipoReunionCatalogo()
        {
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            List<TipoReunionModel> listaTiposReunion = new List<TipoReunionModel>();

            //retorna lista tipos de reunion
            listaTiposReunion = businessCatalogo.getListarTipoReunion();
            ViewBag.ListaReuniones = listaTiposReunion;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("TipoReunionCatalogoView");

        }


        //Crear tipo de reunion
        [HttpPost]
        public IActionResult CrearTipoReunion(TipoReunionModel reunionModel)
        {

            BusinessCatalogo bussinessCatalogo = new BusinessCatalogo(Configuration);
            bool valido = false;
            //Valida que el nombre no venga en blanco
            if (reunionModel.TC_Nombre_Tipo_Reunion !=null)
            {
                //Crear el tipo si el espacio es diferente de null
                valido = bussinessCatalogo.CrearTipoReunion(reunionModel);
            }
            else {
                valido = false;
            }
            
            //si logra guardar retorna true y le informa al usuario
            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }


            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            List<TipoReunionModel> listaTiposReunion = new List<TipoReunionModel>();

            //Lista los tipos de reunion que hay para que el usuario pueda verlos
            listaTiposReunion = businessCatalogo.getListarTipoReunion();
            ViewBag.ListaReuniones = listaTiposReunion;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("TipoReunionCatalogoView");
        }


        //ModificarTipoReunion
        [HttpPost]
        public IActionResult ModificarTipoReunion(TipoReunionModel reunionModel)
        {

            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            bool valido = businessCatalogo.ModificarTipoReunion(reunionModel);
            string mensaje = "";
            //Retorna true si logro modificar el tipo de reunion
            if (valido)
            {
                mensaje = "Si modifico";
            }
            else
            {
                mensaje = "No modifico";
            }

            return new JsonResult(mensaje);

        } // Modificar Ajax


        //EliminarTipoReunion
        [HttpPost]
        public IActionResult EliminarTipoReunion(TipoReunionModel reunionModel)
        {

            
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            bool valido = businessCatalogo.EliminarTipoReunion(reunionModel);
            string mensaje = "";

            //retorna true si logro eliminar el tipo de reunion
            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }


            List<TipoReunionModel> listaTiposReunion = new List<TipoReunionModel>();
            //Carga la lista de todos los tipos de reunion asi el usuario ve si la logro eliminar
            listaTiposReunion = businessCatalogo.getListarTipoReunion();
            ViewBag.ListaReuniones = listaTiposReunion;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("TipoReunionCatalogoView");


        } // Eliminar tipo reunion




        //OFICINAS DEL SISTEMA

        //Se encarga de traer los circuitos y levantar la view de oficina
        public IActionResult viewOfinaCatalogo()
        {
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            List<CircuitoModel> listaCircuitos = new List<CircuitoModel>();
            List<OficinaModel> listaOficina = new List<OficinaModel>();

            //Trae la lista de circuitos de la base
            listaCircuitos = businessCatalogo.getListarCatalogoCircuito();
            ViewBag.ListaCircuitos = listaCircuitos;
            
            //trae la lista de oficinas de la base
            listaOficina = businessCatalogo.getListarCatalogoOficina();
            ViewBag.ListaOficinas = listaOficina;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("OficinaCatalogoView");

        }



        //Crear oficina
        [HttpPost]
        public IActionResult CrearOficina(OficinaModel oficinaModel)
        {
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);


            bool valido = false;
            //valida que no vengan espacios en blanco
            if (oficinaModel.TC_Nombre != null && oficinaModel.TC_Codigo!=null && oficinaModel.TF_Inicio_Vigencia != null)
            {
                valido = businessCatalogo.CrearOficina(oficinaModel);
            }
            else
            {
                valido = false;
            }


            //Si es True se creon la oficina de lo contrario retorna false
            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }

            List<CircuitoModel> listaCircuitos = new List<CircuitoModel>();
            List<OficinaModel> listaOficina = new List<OficinaModel>();

            //Lista de circuitos
            listaCircuitos = businessCatalogo.getListarCatalogoCircuito();
            ViewBag.ListaCircuitos = listaCircuitos;

            // lista de oficinas
            listaOficina = businessCatalogo.getListarCatalogoOficina();
            ViewBag.ListaOficinas = listaOficina;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            return View("OficinaCatalogoView");

        }


        //Modificar Oficina
        [HttpPost]
        public IActionResult ModificarOficina(OficinaModel oficinaModel)
        {
        
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            //Retorna True si la reunion se logro modificar
            bool valido = businessCatalogo.ModificarOficina(oficinaModel);
            string mensaje = "";
            if (valido)
            {
                mensaje = "Si modifico";
            }
            else
            {
                mensaje = "No modifico";
            }

            return new JsonResult(mensaje);

        } // Modificar Ajax


        //Eliminar oficina
        [HttpPost]
        public IActionResult EliminarOficina(OficinaModel oficinaModel)
        {

            
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            //retorna true si la reunion se logro eliminar
            bool valido = businessCatalogo.EliminarOficina(oficinaModel);

            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }


            List<OficinaModel> listaOficinas = new List<OficinaModel>();
            List<CircuitoModel> listaCircuitos = new List<CircuitoModel>();

            //listar circuitos
            listaCircuitos = businessCatalogo.getListarCatalogoCircuito();
            ViewBag.ListaCircuitos = listaCircuitos;
            //listar oficinas
            listaOficinas = businessCatalogo.getListarCatalogoOficina();
            ViewBag.ListaOficinas = listaOficinas;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("OficinaCatalogoView");


        }


        //ROLES DEL SISTEMA
        //cargar la vista de roles
        public IActionResult viewRolSistemaCatalogo()
        {
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            List<RolModel> listarRoles = new List<RolModel>();
            List<PermisosModel> listarPermisos = new List<PermisosModel>();

            //trae la lista de permisos y de roles
            listarPermisos = businessCatalogo.getListarCatalogoPermisos();
            listarRoles = businessCatalogo.getListarCatalogoRol();
            ViewBag.ListaRoles = listarRoles;
            ViewBag.ListaPermisos = listarPermisos;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("RolSistemaCatalogoView");
            
        }

        //Crear Rol
        [HttpPost]
        public IActionResult CrearRol(RolModel rolModel)
        {
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);


            bool valido = false;
            //valida que no vengan espacios en blanco
            if (rolModel.TC_Nombre_Rol != null )
            {
                valido = businessCatalogo.CrearRol(rolModel);
            }
            else
            {
                valido = false;
            }


           //Si valido es true si se creo el rol de lo contrario se le indica al usuario atravez de el ViewBag
            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }


            //BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            List<RolModel> listarRoles = new List<RolModel>();
            List<PermisosModel> listarPermisos = new List<PermisosModel>();

            //Listar permisos y los Roles que ya se han creado
            listarPermisos = businessCatalogo.getListarCatalogoPermisos();
            listarRoles = businessCatalogo.getListarCatalogoRol();
            ViewBag.ListaRoles = listarRoles;
            ViewBag.ListaPermisos = listarPermisos;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("RolSistemaCatalogoView");
        }


        //Modificar Puesto
        [HttpPost]
        public IActionResult ModificarRol(RolModel rolModel)
        {

            
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            //valida si es true fue que se logro modificar del contrario le avisa al usuario
            bool valido = businessCatalogo.ModificarRol(rolModel);
            string mensaje = "";
            if (valido)
            {
                mensaje = "Si modifico";
            }
            else
            {
                mensaje = "No modifico";
            }

            return new JsonResult(mensaje);

        } // Modificar Ajax



        //Eliminar Roles de usuario
        [HttpPost]
        public IActionResult EliminarRol(RolModel rolModel)
        {

            
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            //Si valido es True fue que se elimino correctamente de lo contrario se notifica al usuario del error
            bool valido = businessCatalogo.EliminarRol(rolModel);

            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }

           
            List<RolModel> listarRoles = new List<RolModel>();
            List<PermisosModel> listarPermisos = new List<PermisosModel>();

            //Retorna lista de permisos y retorna la lista de roles que se han creado 
            listarPermisos = businessCatalogo.getListarCatalogoPermisos();
            listarRoles = businessCatalogo.getListarCatalogoRol();
            ViewBag.ListaRoles = listarRoles;
            ViewBag.ListaPermisos = listarPermisos;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("RolSistemaCatalogoView");

        }



        //PUESTOS DEL SISTEMA

        //Cargar la vista para crear modificar y eliminar puestos 
        public IActionResult viewPuestoUsuarioCatalogo()
        {

            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            List<PuestoModel> listaPuestos = new List<PuestoModel>();

            //Lista de puestos
            listaPuestos = businessCatalogo.getListarCatalogoPuesto();
            ViewBag.ListaPuestos = listaPuestos;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            return View("PuestoUsuarioCatalogoView");

        }


        //Crear Puesto
        [HttpPost]
        public IActionResult CrearPuesto(PuestoModel puestoModel)
        {
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);

            bool valido = false;
            
            //valida que no vengan espacios en blanco
            if (puestoModel.TC_Nombre_Puesto != null)
            {
                valido = businessCatalogo.CrearPuesto(puestoModel);
            }
            else
            {
                valido = false;
            }

           //Si es true fue se que creo con exito de lo contrario se le avisa al usuario del fallo
            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }


            List<PuestoModel> listaPuestos = new List<PuestoModel>();

            listaPuestos = businessCatalogo.getListarCatalogoPuesto();
            ViewBag.ListaPuestos = listaPuestos;

            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            return View("PuestoUsuarioCatalogoView");

        }


        //Modificar Puesto
        [HttpPost]
        public IActionResult ModificarPuesto(PuestoModel puestoModel)
        {

            //return new JsonResult(oficinaModel.TC_Nombre + "   "+ oficinaModel.TN_Id_Oficina + "   " + oficinaModel.TC_Codigo);
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            //valida si valido es True esq que si se modifco el puesto
            bool valido = businessCatalogo.ModificarPuesto(puestoModel);

            string mensaje = "";
            if (valido)
            {
                mensaje = "Si modifico";
            }
            else
            {
                mensaje = "No modifico";
            }

            return new JsonResult(mensaje);

        } // Modificar Ajax

       

       
        //Metodo encargado de llamar al business para elimnar el puesto
        [HttpPost]
        public IActionResult EliminarPuesto(PuestoModel puestoModel)
        {

            //return new JsonResult(reunionModel.TN_Id_Tipo_Reunion + "  Haolaaaaaaaa   " + reunionModel.TC_Nombre_Tipo_Reunion);
            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
            //valida si valido es True esq que si se elimino el puesto
            bool valido = businessCatalogo.EliminarPuesto(puestoModel);

            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {
                ViewBag.ERROR = "Error al guardar";
            }


            List<PuestoModel> listaPuestos = new List<PuestoModel>();

            listaPuestos = businessCatalogo.getListarCatalogoPuesto();
            ViewBag.ListaPuestos = listaPuestos;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

            return View("PuestoUsuarioCatalogoView");

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace GestorReunionesProyectoAplicada.Controllers
{
    public class UsuarioController : Controller
    {

        public IConfiguration Configuration { get; }

        public UsuarioController(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        //Abre la vista para crear un usuario, envia 3 listas con las oficinas, roles y puestos a la vista
        public IActionResult MuestraCrearUsuario()
        {

            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);
           
            List<PuestoModel> listaPuesto = new List<PuestoModel>();
            List<OficinaModel> listaOficina = new List<OficinaModel>();
            List<RolModel> listaRol = new List<RolModel>();
            
            listaPuesto = businessCatalogo.getListarCatalogoPuesto();
            listaOficina = businessCatalogo.getListarCatalogoOficina();
            listaRol = businessCatalogo.getListarCatalogoRol();

            ViewBag.ListaPuestos = listaPuesto;
            ViewBag.ListaRoles = listaRol;
            ViewBag.ListaOficinas = listaOficina;


            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("CrearUsuario");
        }


        //Se encarga de mostrar el login del usuario
        public IActionResult MuestraLoginUsuario()
        {

            //Se limpian las variables en session
            HttpContext.Session.SetString("UsuarioLogin", "");
            HttpContext.Session.SetString("UsuarioPermiso","");
            return View("LoginView");
        }


        //Valida si el usuario existe para que ingrese al sistema
        [HttpPost]
        public IActionResult LoginUsuario(LoginModel loginModel)
        {

            //Se crea un objeto usuario y se le asigna un valor desde la base de datos
            UsuarioModel usuarioModel = new UsuarioModel();
           
            BusinessUsuario bussinesUsuario = new BusinessUsuario(Configuration);
            usuarioModel = bussinesUsuario.LoginUsuario(loginModel);

            //Si es diferente de null es que existe por lo tanto pasa a la siguiente vista, en caso contrario regresa al login con un mensaje de error
            if (usuarioModel != null) {

                HttpContext.Session.SetString("UsuarioLogin", usuarioModel.Usuario);
                HttpContext.Session.SetString("UsuarioPermiso", usuarioModel.Permisos.ToString());
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");

                BusinessAgenda businessAgenda = new BusinessAgenda(Configuration);
                List<ReunionModel> listaReuniones = new List<ReunionModel>();

                //Se extraen las reuniones de las base para colocarlas en el calendario
                listaReuniones = businessAgenda.getReuniones(HttpContext.Session.GetString("UsuarioLogin"));

                ViewBag.ListaReuniones = listaReuniones;

             
                return View("/Views/Agenda/Calendario.cshtml");
                

            }
            else {
                String ERROR = " Error en las credenciales";
                ViewBag.ERROR = ERROR;
                return View("LoginView");                
            }

        } // Login Usuario



        //Se utiliza insertar usuario en bases de datos 
        [HttpPost]
        public IActionResult CrearUsuario(UsuarioModel usuarioModel)
        {

            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);

            bool valido = false;

            string contrasena = usuarioModel.Contrasena;
            int valoresNumericos = 0;
            int valoresString = 0;
            int valorMaximo = 0;

            //Se realiza un conteo de cuantos numero y letras trae la contraseña
            for (int i = 0; i < contrasena.Length; i++)
            {

                if (Char.IsNumber(contrasena[i]) == true)
                {
                    valoresNumericos++;
                }
                else
                {
                    valoresString++;
                }
            }

            valorMaximo = valoresNumericos + valoresString;

            //Se valida que cumpla con un minimo de 3 numeros y 4 letras y que no sea mayor a 16 caracteres
            if (valoresNumericos >= 3 && valoresString >= 4 && valorMaximo < 16)
            {
                
                //Se valida que los campos que se insertaron no vengan en blanco
                if (usuarioModel.Nombre != null && usuarioModel.PrimerApellido != null && usuarioModel.SegundoApellido != null && usuarioModel.Cedula != null && usuarioModel.Usuario != null && usuarioModel.Correo != null && usuarioModel.Contrasena != null)
                {
                    //Si el usuario se inserta con exito valido=True en caso contrario es False
                    valido = businessUsuario.CrearUsuario(usuarioModel);
                }
                else
                {
                    valido = false;
                }
            }
            else
            {
                ViewBag.ErrorContrasena = "La contraseña requiere un minimo de 3 numeros y 4 letras";
              
            }

            BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);

            List<PuestoModel> listaPuesto = new List<PuestoModel>();
            List<OficinaModel> listaOficina = new List<OficinaModel>();
            List<RolModel> listaRol = new List<RolModel>();

            //Se trae la informacion que requieren la interfaz para realizar la insercion de un usuario, en este caso lista de puestos, roles, oficinas
            listaPuesto = businessCatalogo.getListarCatalogoPuesto();
            listaOficina = businessCatalogo.getListarCatalogoOficina();
            listaRol = businessCatalogo.getListarCatalogoRol();

            ViewBag.ListaPuestos = listaPuesto;
            ViewBag.ListaRoles = listaRol;
            ViewBag.ListaOficinas = listaOficina;
            
            // valido = TRUE exito, de lo contrario no se inserto el usuario
            if (valido)
            {
                ViewBag.EXITO = "Exito al guardar";
            }
            else
            {                

                ViewBag.ERROR = "Error al guardar";
            }

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("CrearUsuario");
           
        } // Crear Usuario



        //Se realiza la eliminación de un usuario
        [HttpPost]
        public IActionResult EliminarUsuario(UsuarioModel usuarioModel)
        {

            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

            //Se retorna la lista de usuarios que esta en la base de datos, si consiguio ser borrado se vera reflejado en la interfaz
            listaUsuarios = businessUsuario.EliminarUsuario(usuarioModel);

            ViewBag.ListaUsuarios = listaUsuarios;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("ModificarContactoView");

        } // Eliminar Usuario
        

        //listar usuarios existentes en interfaz para eliminar y modificar
        public IActionResult viewListarUsuario()
        {
            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            //Se listan los usuarios de la base de datos
            listaUsuarios = businessUsuario.getListarUsuario();

            ViewBag.ListaUsuarios = listaUsuarios;

            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("ModificarContactoView");
        }


        //Carga las listas de Oficina, Roles, Puesto y Los datos del usuario los manda a interfaz para poder realiza la modificación
        [HttpPost]
        public IActionResult VistaModificarUsuario(UsuarioModel usuarioModel)
        {

            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);
            UsuarioModel usuarioModificar = new UsuarioModel();
            
            //Se trae de la base el usuario con el ID que solicito el usuario
            usuarioModificar = businessUsuario.getUsuarioModificar(usuarioModel);

            //Se valida si ese usuario existia sin no existe se retorna la vista anterior con todos los usuarios
            if (usuarioModificar != null)
            {
                BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);

                List<PuestoModel> listaPuesto = new List<PuestoModel>();
                List<OficinaModel> listaOficina = new List<OficinaModel>();
                List<RolModel> listaRol = new List<RolModel>();

                //Se listan Roles, Oficinas, Puestos para poder cargarlos en interfaz y que se realice la modificaion del usuario
                listaPuesto = businessCatalogo.getListarCatalogoPuesto();
                listaOficina = businessCatalogo.getListarCatalogoOficina();
                listaRol = businessCatalogo.getListarCatalogoRol();

                ViewBag.ListaPuestos = listaPuesto;
                ViewBag.ListaRoles = listaRol;
                ViewBag.ListaOficinas = listaOficina;
                ViewBag.UsuarioModificar = usuarioModificar;
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                return View("ModificarUsuario");
            }
            else
            {

                List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

                //Extraer lista de usuarios
                listaUsuarios = businessUsuario.getListarUsuario();

                ViewBag.ListaUsuarios = listaUsuarios;
                ViewBag.ERROR = "Error al guardar";
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                return View("ModificarContactoView");
            }
            
        }

        
        //Modificar Usuario en la base de datos
        [HttpPost]
        public IActionResult ModificarUsuario(UsuarioModel usuarioModel)
        {

            BusinessUsuario businessUsuario = new BusinessUsuario(Configuration);

            bool valido = false;

            string contrasena = usuarioModel.Contrasena;
            int valoresNumericos = 0;
            int valoresString = 0;
            int valorMaximo = 0;

            //Se realiza un conteo de cuantos numero y letras trae la contraseña
            for (int i = 0; i < contrasena.Length; i++)
            {

                if (Char.IsNumber(contrasena[i]) == true)
                {
                    valoresNumericos++;
                }
                else
                {
                    valoresString++;
                }
            }

            valorMaximo = valoresNumericos + valoresString;

            //Se valida que cumpla con un minimo de 3 numeros y 4 letras y que no sea mayor a 16 caracteres
            if (valoresNumericos >= 3 && valoresString >= 4 && valorMaximo < 16)
            {
                //Se valida si los campos vienen vacios
                if (usuarioModel.Nombre != null && usuarioModel.PrimerApellido != null && usuarioModel.SegundoApellido != null && usuarioModel.Cedula != null && usuarioModel.Usuario != null && usuarioModel.Correo != null && usuarioModel.Contrasena != null)
                {
                    valido = businessUsuario.ModificarUsuario(usuarioModel);
                }
                else
                {
                    valido = false;
                }
            }
            else
            {
                ViewBag.ErrorContrasena = "La contraseña requiere un minimo de 3 numeros y 4 letras";

            }


            //Si es true se retorna la lista de todos los usuarios por sin desea modificar algun otro
            if (valido)
            {
                List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
                listaUsuarios = businessUsuario.getListarUsuario();
                ViewBag.ListaUsuarios = listaUsuarios;
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                return View("ModificarContactoView");
            }else{

                //En caso de fallo
                BusinessCatalogo businessCatalogo = new BusinessCatalogo(Configuration);

                List<PuestoModel> listaPuesto = new List<PuestoModel>();
                List<OficinaModel> listaOficina = new List<OficinaModel>();
                List<RolModel> listaRol = new List<RolModel>();

                //Lista puesto, Rol, Catalogos para poder modificar nuevamente
                listaPuesto = businessCatalogo.getListarCatalogoPuesto();
                listaOficina = businessCatalogo.getListarCatalogoOficina();
                listaRol = businessCatalogo.getListarCatalogoRol();

                ViewBag.ListaPuestos = listaPuesto;
                ViewBag.ListaRoles = listaRol;
                ViewBag.ListaOficinas = listaOficina;
               
                UsuarioModel usuarioModificar = new UsuarioModel();

                //Trae el usuario que se habia solicitado anteriormente
                usuarioModificar = businessUsuario.getUsuarioModificar(usuarioModel);
                ViewBag.UsuarioModificar = usuarioModificar;

                //Se le crea un mensaje de error porque no se pudo modificar
                ViewBag.ERROR = "Error al guardar";
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                return View("ModificarUsuario");
            }          
            
        } // Modificar Usuario
        
        public IActionResult IniciarLogin()
        {
            HttpContext.Session.SetString("UsuarioPermiso", null);
            return View("LoginView");
        }

    }
}

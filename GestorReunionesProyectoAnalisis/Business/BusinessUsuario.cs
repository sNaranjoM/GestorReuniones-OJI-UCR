using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAplicada.Business
{
    public class BusinessUsuario
    {
        public IConfiguration Configuration { get; }

        public BusinessUsuario(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        public UsuarioModel LoginUsuario(LoginModel loginModel) {
           
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.LoginUsuario(loginModel);
            
        }

        public List<UsuarioModel> getUsuarioMinuta(string idReunion)
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.getUsuariosMinuta(idReunion);

        }


        public bool CrearUsuario(UsuarioModel usuarioModel)
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.CrearUsuario(usuarioModel);
         
        }

        public List<UsuarioModel> EliminarUsuario(UsuarioModel usuarioModel)
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.EliminarUsuario(usuarioModel);

        }

        //Listar Usuarios
        public List<UsuarioModel> getListarUsuario()
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.getListarUsuario();
        }
        
        //Trae los datos del usuario que se desea modificar
        public UsuarioModel getUsuarioModificar(UsuarioModel usuarioModel)
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.getUsuarioModificar(usuarioModel);
        }


        public bool ModificarUsuario(UsuarioModel usuarioModel)
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.ModificarUsuario(usuarioModel);

        }


        public List<UsuarioModel> getListarUsuarioReunion(string idReunion)
        {
            DataUsuario dataUsuario = new DataUsuario(Configuration);
            return dataUsuario.getListarUsuarioReunion(idReunion);
        }



    }
}

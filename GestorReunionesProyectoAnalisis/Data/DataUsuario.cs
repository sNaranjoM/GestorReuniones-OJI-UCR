using GestorReunionesProyectoAnalisis.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GestorReunionesProyectoAplicada.Data
{
    public class DataUsuario
    {
        public IConfiguration Configuration { get; }

        public DataUsuario(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        //Verificar si el usuario existe, en caso de que si se debe 
        public UsuarioModel LoginUsuario(LoginModel loginModel)
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Login_Usuario @TC_Usuario ='{loginModel.Nombre}', @TC_Contrasenna='{loginModel.Contrasena}'  ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read()) {

                        valido = Int32.Parse(productoReader["valido"].ToString());
                        //Si valido =1 se extraen los demas valores devueltos, en este caso se devuelve el nombre del usuario, el nombre del rol 
                        //y cual id de permiso posee el usuario
                        if (valido==1) {
                            usuarioModel.Usuario = productoReader["TC_Usuario"].ToString();
                            usuarioModel.Rol = productoReader["TC_Nombre_Rol"].ToString();
                            usuarioModel.Permisos = Int32.Parse(productoReader["TN_Id_Permiso"].ToString());
                        }
                       
                       
                    }
                    // se cierra la conexion a la base de datos
                    connection.Close();

                }
            }
            //si el valor de valido es 1 se hace el retun hacia el model de usuario si no retorna null
            if (valido == 1)
            {
                return usuarioModel;
            }
            else {
                return null;
            }

        }

        //Crear usuario
        public bool CrearUsuario(UsuarioModel usuarioModel)
        {
           
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Usuario @TC_Usuario ='{usuarioModel.Usuario}', @TC_Contrasenna ='{usuarioModel.Contrasena}', @TC_Identificacion ='{usuarioModel.Cedula}', @TC_Nombre_Usuario ='{usuarioModel.Nombre}', @TC_Primer_Apellido ='{usuarioModel.PrimerApellido}',@TC_Segundo_Apellido ='{usuarioModel.SegundoApellido}' ,@TC_Correo ='{usuarioModel.Correo}' ,@TN_Id_Puesto ='{usuarioModel.Puesto}', @TN_Id_Oficina ='{usuarioModel.Oficina}',  @TN_Id_Rol ='{usuarioModel.Rol}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //se extrae el valor de valido de la respuesta que se obtiene de la consulta
                    while (productoReader.Read())
                    {
                        valido = Int32.Parse(productoReader["valido"].ToString());
                    }
                    //Se cierra la conexion a la base de datos
                    connection.Close();

                }
            }
            //si valido=1 significa que se logro hacer el insert de usuario de ser diferente significa que hubo un error a la hora de guardar en la base de datos
            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //Eliminar Usuario
        public List<UsuarioModel> EliminarUsuario(UsuarioModel usuarioModel)
        {

            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Usuario @TN_Id_Usuario  ='{usuarioModel.IdUsuario}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //se extrae el valor de valido de la respuesta que se obtiene de la consulta
                    while (productoReader.Read())
                    {
                        valido = Int32.Parse(productoReader["valido"].ToString());
                    }
                    //Se cierra la conexion a la base de datos
                    connection.Close();

                }
            }

            //si valido es 1 significa que el usuario si se elimino y se retorna la lista de usuarios en ambos casos
            if (valido == 1)
            {
                return getListarUsuario();
            }
            else
            {
                return getListarUsuario();
            }
        }


        //Listar Usuarios para poder modificar y eliminar
        public List<UsuarioModel> getListarUsuario()
        {

            List<UsuarioModel> listaRetorno = new List<UsuarioModel>();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Usuario";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        UsuarioModel listaUsuarioTemp = new UsuarioModel();
                        listaUsuarioTemp.IdUsuario = Int32.Parse(productoReader["TN_Id_Usuario"].ToString());
                        listaUsuarioTemp.Nombre = productoReader["TC_Nombre_Usuario"].ToString();
                        listaUsuarioTemp.PrimerApellido = productoReader["TC_Primer_Apellido"].ToString();
                        listaUsuarioTemp.SegundoApellido = productoReader["TC_Segundo_Apellido"].ToString();
                        listaUsuarioTemp.Puesto = productoReader["TC_Nombre_Puesto"].ToString();
                        listaUsuarioTemp.Oficina = productoReader["TC_Nombre_Oficina"].ToString();
                        
                        listaRetorno.Add(listaUsuarioTemp);
                    } // while
                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }




        public List<UsuarioModel> getListarUsuarioReunion(string idReunion)
        {

            List<UsuarioModel> listaRetorno = new List<UsuarioModel>();
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Reunion_Usuarios @TN_Id_Reunion  ='{idReunion}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        UsuarioModel listaUsuarioTemp = new UsuarioModel();

                        valido = Int32.Parse(productoReader["valido"].ToString());
                        listaUsuarioTemp.IdUsuario = Int32.Parse(productoReader["TN_Id_Usuario"].ToString());
                        listaUsuarioTemp.Nombre = productoReader["TC_Nombre_Usuario"].ToString();
                        listaUsuarioTemp.PrimerApellido = productoReader["TC_Primer_Apellido"].ToString();
                        listaUsuarioTemp.Puesto = productoReader["TC_Nombre_Puesto"].ToString();
                        listaUsuarioTemp.Oficina = productoReader["TC_Nombre"].ToString();

                        listaRetorno.Add(listaUsuarioTemp);
                    } // while
                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }







        //retorna los datos del usuario que se desea modificar
        public UsuarioModel getUsuarioModificar(UsuarioModel usuarioModel)
        {
            int valido = 0;
            UsuarioModel usuarioTemp = new UsuarioModel();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Usuario_Unico @TN_Id_Usuario  ='{usuarioModel.IdUsuario}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {

                        valido = Int32.Parse(productoReader["valido"].ToString());
                        usuarioTemp.IdUsuario = Int32.Parse(productoReader["TN_Id_Usuario"].ToString());
                        usuarioTemp.Usuario = productoReader["TC_Usuario"].ToString();
                        usuarioTemp.Contrasena = productoReader["TC_Contrasenna"].ToString();
                        usuarioTemp.Cedula = productoReader["TC_Identificacion"].ToString();
                        usuarioTemp.Nombre = productoReader["TC_Nombre_Usuario"].ToString();
                        usuarioTemp.PrimerApellido = productoReader["TC_Primer_Apellido"].ToString();
                        usuarioTemp.SegundoApellido = productoReader["TC_Segundo_Apellido"].ToString();
                        usuarioTemp.Correo = productoReader["TC_Correo"].ToString();
                        usuarioTemp.Puesto = productoReader["TN_Id_Puesto"].ToString();
                        usuarioTemp.TC_Nombre_Puesto = productoReader["TC_Nombre_Puesto"].ToString();
                        usuarioTemp.Oficina = productoReader["TN_Id_Oficina"].ToString();
                        usuarioTemp.TC_Nombre_Oficina = productoReader["TC_Nombre"].ToString();
                        usuarioTemp.Rol = productoReader["TN_Id_Rol"].ToString();
                        usuarioTemp.TC_Nombre_Rol = productoReader["TC_Nombre_Rol"].ToString();

                        //listaRetorno.Add(listaUsuarioTemp);
                    } // while

                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }
            //si el valor de valido=1 se hace un return de un model de usuario para mostrar la informacion de el en interfaz, de no ser asi se retorna null para que indique 
            //que no se encontro ese usuario
            if (valido == 1)
            {
                return usuarioTemp;
            }
            else
            {
                return null;
            }

        }



        //Modificar usuario
        public bool ModificarUsuario(UsuarioModel usuarioModel)
        {

            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Usuario @TN_Id_Usuario ='{usuarioModel.IdUsuario}', @TC_Usuario ='{usuarioModel.Usuario}', @TC_Contrasenna ='{usuarioModel.Contrasena}', @TC_Identificacion ='{usuarioModel.Cedula}', @TC_Nombre_Usuario ='{usuarioModel.Nombre}', @TC_Primer_Apellido ='{usuarioModel.PrimerApellido}',@TC_Segundo_Apellido ='{usuarioModel.SegundoApellido}' ,@TC_Correo ='{usuarioModel.Correo}' ,@TN_Id_Puesto ='{usuarioModel.Puesto}', @TN_Id_Oficina ='{usuarioModel.Oficina}',  @TN_Id_Rol ='{usuarioModel.Rol}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        valido = Int32.Parse(productoReader["valido"].ToString());
                    }
                    //Se cierra la conexion a la base de datos
                    connection.Close();

                }
            }
            // si valido=1 indica que el update de realizo con exito
            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public List<UsuarioModel> getUsuariosMinuta(string idReunion)
        {

            int valido = 1;
            List<UsuarioModel> listaRetorno = new List<UsuarioModel>();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Usuarios_Minuta  @TN_Id_Reunion ='{idReunion}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        UsuarioModel listaUsuarioTemp = new UsuarioModel();
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        listaUsuarioTemp.Nombre = productoReader["TC_Nombre_Usuario"].ToString();
                        listaUsuarioTemp.PrimerApellido = productoReader["TC_Primer_Apellido"].ToString();
                        listaUsuarioTemp.SegundoApellido = productoReader["TC_Segundo_Apellido"].ToString();
                        listaUsuarioTemp.Puesto = productoReader["TC_Nombre_Puesto"].ToString();
                        listaUsuarioTemp.Asistencia = Int32.Parse(productoReader["TN_Asistencia"].ToString());
                        listaRetorno.Add(listaUsuarioTemp);
                    } // while
                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            // si valido=1 se retorna la lista de usuarios ligados a la reunion que se le desea realizar la minuta
            if (valido == 1)
            {
                return listaRetorno;
            }
            else
            {
                return null;
            }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using Microsoft.Extensions.Configuration;

namespace GestorReunionesProyectoAplicada.Data
{
    public class DataAgenda
    {
        public IConfiguration Configuration { get; }

        public DataAgenda(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        //Lista las reuniones que tiene un usuario
        public List<ReunionModel> getReunionesData(string usuario)
        {

            int valido = 0;
            List<ReunionModel> listaRetorno = new List<ReunionModel>();

            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            //se crea la conexion
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Agenda @TC_Usuario ='{usuario}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        ReunionModel listaReunionTemp = new ReunionModel();
                        //Si valido =1 se extraen los valores de esa reunion, sino fue que fallo la consulta o no tiene reuniones
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        if (valido==1) {
                            listaReunionTemp.TN_Id_Reunion = Int32.Parse(productoReader["TN_Id_Reunion"].ToString());
                            listaReunionTemp.TC_Nombre_Reunion = productoReader["TC_Nombre_Reunion"].ToString();
                            listaReunionTemp.TC_Descripcion = productoReader["TC_Descripcion"].ToString();
                            listaReunionTemp.TC_Comentario = productoReader["TC_Comentario"].ToString();
                            listaReunionTemp.TC_Lugar = productoReader["TC_Lugar"].ToString();
                            listaReunionTemp.TC_Fecha_Reunion = DateTime.Parse(productoReader["TC_Fecha_Inicio"].ToString());
                        }

                        listaRetorno.Add(listaReunionTemp);
                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaRetorno;
            }
            else
            {
                return null;
            }

        }


        //Retorna la lista de asistentes de una reunion especifica
        public List<UsuarioModel> getAsistentesReunion(string IdReunionAsistentes)
        {

            int valido = 0;
            List<UsuarioModel> listaRetorno = new List<UsuarioModel>();

            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            //se crea la conexion
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              
                //se escribe la consulta
              
                string sqlQuery = $"exec Sp_Listar_Reunion_Usuarios @TN_Id_Reunion ='{IdReunionAsistentes}'";
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
               
                        //Si valido =1 se extraen los valores de esa reunion, sino fue que fallo la consulta o no tiene asistentes
                     
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        if (valido == 1)
                        {                            
                            listaUsuarioTemp.IdUsuario = Int32.Parse(productoReader["TN_Id_Usuario"].ToString());
                            listaUsuarioTemp.Nombre = productoReader["TC_Nombre_Usuario"].ToString();
                            listaUsuarioTemp.PrimerApellido = productoReader["TC_Primer_Apellido"].ToString();
                            listaUsuarioTemp.Puesto = productoReader["TC_Nombre_Puesto"].ToString();
                            listaUsuarioTemp.Oficina = productoReader["TC_Nombre"].ToString();
                                                                      
                        }

                        listaRetorno.Add(listaUsuarioTemp);
                    } // while
                                   
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaRetorno;
            }
            else
            {
                return null;
            }

        }

        //se encarga de retornar las tareas que tenga una reunion
        public List<TareaModel> getTareasReunion(string IdReunionTareas)
        {

            int valido = 0;
            List<TareaModel> listaRetorno = new List<TareaModel>();
            //se crea la conexion
           
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                //se escribe la consulta               
                string sqlQuery = $"exec Sp_Listar_Reunion_Tareas @TN_Id_Reunion ='{IdReunionTareas}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                 
                    // Se abre y se ejecuta la consulta
                   
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta
                  
                    while (productoReader.Read())
                    {

                        TareaModel listaTareasTemp = new TareaModel();
                        //Si valido =1 se extraen los valores de esa reunion, sino fue que fallo la consulta o no tiene tareas
                        
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        if (valido == 1)
                        {
                            listaTareasTemp.TN_Id_Tarea = Int32.Parse(productoReader["TN_Id_Tarea"].ToString());
                            listaTareasTemp.TC_Nombre_Tarea = productoReader["TC_Nombre_Tarea"].ToString();
                            listaTareasTemp.TC_Descripcion_Tarea = productoReader["TC_Descripcion"].ToString();
                            listaTareasTemp.listaUsuarios = productoReader["TC_Usuarios"].ToString();
                        }

                        listaRetorno.Add(listaTareasTemp);
                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

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

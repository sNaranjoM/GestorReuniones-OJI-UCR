using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GestorReunionesProyectoAplicada.Data
{
    public class DataTarea
    {
        public IConfiguration Configuration { get; }

        public DataTarea(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        //Crear Tarea
        public bool CrearTarea(TareaModel tareaModel)
        {

            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Tarea @TC_Nombre_Tarea ='{tareaModel.TC_Nombre_Tarea}', @TC_Descripcion ='{tareaModel.TC_Descripcion_Tarea}' , @TC_Lista_Usuarios='{tareaModel.listaUsuarios}'";
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

            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Modificar Tarea
        public bool ModificarTarea(TareaModel tareaModel)
        {

            int valido = 0;


            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Tarea @TN_Id_Tarea ='{tareaModel.TN_Id_Tarea}' ,@TC_Nombre_Tarea ='{tareaModel.TC_Nombre_Tarea}', @TC_Descripcion ='{tareaModel.TC_Descripcion_Tarea}', @TC_Lista_Usuarios ='{tareaModel.listaUsuarios}'";
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

            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Eliminar Tarea
        public List<TareaModel> EliminarTarea(string idTarea)
        {

            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Tarea @TN_Id_Tarea  ='{idTarea}'";
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

            if (valido == 1)
            {
                return getListarTarea();
            }
            else
            {
                return getListarTarea();
            }
            
        }
        

        //Listar Tareas para poder modificar y eliminar
        public List<TareaModel> getListarTarea()
        {
            int valido = 0;
            List<TareaModel> listaRetorno = new List<TareaModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Tarea";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        TareaModel listaTareaTemp = new TareaModel();
                        valido = Int32.Parse(productoReader["valido"].ToString());

                        //Si valido =1 se extren todos los valores                        
                        if (valido == 1) {
                            listaTareaTemp.TN_Id_Tarea = Int32.Parse(productoReader["TN_Id_Tarea"].ToString());
                            listaTareaTemp.TC_Nombre_Tarea = productoReader["TC_Nombre_Tarea"].ToString();
                            listaTareaTemp.TC_Descripcion_Tarea = productoReader["TC_Descripcion"].ToString();
                        }

                        listaRetorno.Add(listaTareaTemp);
                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }

        //Listar las tareas de una reunion
        public List<TareaModel> getListarTareaReunion(string idReunion)
        {

            List<TareaModel> listaRetorno = new List<TareaModel>();
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Reunion_Tareas @TN_Id_Reunion  ='{idReunion}'";
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

                        //Si valido =1 se extren todos los valores
                        if (valido == 1) {
                            TareaModel listaTareaTemp = new TareaModel();
                            listaTareaTemp.TN_Id_Tarea = Int32.Parse(productoReader["TN_Id_Tarea"].ToString());
                            listaTareaTemp.TC_Nombre_Tarea = productoReader["TC_Nombre_Tarea"].ToString();
                            listaTareaTemp.TC_Descripcion_Tarea = productoReader["TC_Descripcion"].ToString();
                            listaTareaTemp.listaUsuarios = productoReader["TC_Usuarios"].ToString();
                            listaRetorno.Add(listaTareaTemp);
                        }
                       
                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }


            return listaRetorno;
        }


        //Retorna la tarea que se desea modificar
        public TareaModel getTareaModificar(string idTarea)
        {
            int valido = 0;
            TareaModel tareaTemp = new TareaModel();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Tarea_Unico @TN_Id_Tarea  ='{idTarea}'";
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

                        //Si valido =1 se extren todos los valores                        
                        if (valido == 1)
                        {
                            tareaTemp.TN_Id_Tarea = Int32.Parse(productoReader["TN_Id_Tarea"].ToString());
                            tareaTemp.TC_Nombre_Tarea = productoReader["TC_Nombre_Tarea"].ToString();
                            tareaTemp.TC_Descripcion_Tarea = productoReader["TC_Descripcion"].ToString();
                            tareaTemp.listaUsuarios = productoReader["TC_Lista_Usuarios"].ToString();
                        }



                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return tareaTemp;
            }
            else
            {
                return null;
            }

        }

        //Listar las tareas que tiene una reunion en especifico segun su id
        public List<TareaModel> getListarTareaMinuta(String idReunion)
        {
            int valido = 0;
            List<TareaModel> listaRetorno = new List<TareaModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Tareas_Minuta  @TN_Id_Reunion  ='{idReunion}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        TareaModel listaTareaTemp = new TareaModel();
                        valido = Int32.Parse(productoReader["valido"].ToString());

                        //Si valido =1 se extren todos los valores
                        if (valido == 1)
                        {
                            listaTareaTemp.TC_Nombre_Tarea = productoReader["TC_Nombre_Tarea"].ToString();
                            listaTareaTemp.TC_Descripcion_Tarea = productoReader["TC_Descripcion"].ToString();
                            listaTareaTemp.TB_Estado = Int32.Parse(productoReader["TB_Estado"].ToString());
                            listaTareaTemp.TC_Acuerdo = productoReader["TC_Acuerdo"].ToString();
                            listaTareaTemp.TC_Nombre_Usuario = productoReader["TC_Nombre_Usuario"].ToString();
                            listaTareaTemp.TC_Primer_Apellido = productoReader["TC_Primer_Apellido"].ToString();
                            listaRetorno.Add(listaTareaTemp);
                        }
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

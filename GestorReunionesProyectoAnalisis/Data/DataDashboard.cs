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
    public class DataDashboard
    {
        public IConfiguration Configuration { get; }

        public DataDashboard(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor



        //retorna la cantidad de reuniones que
        public string getCantidadReuniones(string fechaInicio, string fechaFinal)
        {

            string listaCantidadReuniones = "";
            int valido = 0;

            //se crea la conexion           
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta                               
                string sqlQuery = $"exec Sp_Cantidad_Reuniones @TC_Fecha_Inicio = '{fechaInicio}', @TC_Fecha_Fin = '{fechaFinal}'";
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
                        //Si valido = 1 se extrae una lista de cantidad de reuniones
                       
                        if (valido == 1) {
                            listaCantidadReuniones = productoReader["TN_Cantidad_Reuniones"].ToString();
                        }
                        
                    } // while
                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaCantidadReuniones;
            }
            else {
                return "No hay nada en ese mes";
            }
            
        }



        //retorna la cantidad de tiempo invertido en las reuniones
        public string getCantidadTiempoReuniones(string fechaInicio, string fechaFinal)
        {

            string listaCantidadTiempoReuniones = "";
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Cantidad_Tiempo_Reuniones @TC_Fecha_Inicio = '{fechaInicio}', @TC_Fecha_Fin = '{fechaFinal}'";
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

                        //Si valido =1 se extrae una lista de cantidad de Tiempo invertido en reuniones

                        if (valido == 1)
                        {
                            listaCantidadTiempoReuniones = productoReader["TN_Cantidad_Tiempo_Reuniones"].ToString();
                        }

                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaCantidadTiempoReuniones;
            }
            else
            {
                return "No hay nada en ese mes";
            }

        }


        //Retorna la cantiad de dinero de todas las reuniones en las fechas que se pasan por parametro
        public string getCantidadDineroReuniones(string fechaInicio, string fechaFinal)
        {

            string listaCantidadDineroReuniones = "";
            int valido = 0;
            //se crea la conexion            
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta               
                string sqlQuery = $"exec Sp_Cantidad_Dinero_Reuniones @TC_Fecha_Inicio = '{fechaInicio}', @TC_Fecha_Fin = '{fechaFinal}'";
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
                        //Si valido =1 se extrae la cantidad de dinero de todas las reuniones en esas fechas
                        if (valido == 1)
                        {
                            listaCantidadDineroReuniones = productoReader["TN_Cantidad_Dinero_Reuniones"].ToString();
                        }

                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaCantidadDineroReuniones;
            }
            else
            {
                return "No hay nada en ese mes";
            }

        }


 
        // Retorna una lista con la cantidad de asistentes que tuvieron las reuniones
        public List<CantidadAsistentesModel> getCantidadAsistentesReuniones(string fechaInicio, string fechaFinal)
        {
            List<CantidadAsistentesModel> listaCantidadAsistentesModel = new List<CantidadAsistentesModel>();
           
            int valido = 0;

            //se crea la conexion            
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta                
                string sqlQuery = $"exec Sp_Asistencia_Reuniones @TC_Fecha_Inicio = '{fechaInicio}', @TC_Fecha_Final = '{fechaFinal}'";
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
                            CantidadAsistentesModel cantidadAsistentesModel = new CantidadAsistentesModel();
                            cantidadAsistentesModel.mes = productoReader["mes"].ToString();
                            cantidadAsistentesModel.anno = productoReader["anno"].ToString();
                            cantidadAsistentesModel.asistencia = productoReader["asistencia"].ToString();
                            cantidadAsistentesModel.invitados = productoReader["invitados"].ToString();
                            listaCantidadAsistentesModel.Add(cantidadAsistentesModel);
                        }

                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaCantidadAsistentesModel;
            }
            else
            {
                return null;
            }

        }


        //Retorna una lista con todos los tipos de reuniones que se realizaran en las fechas que entran por parametro
        public List<TipoReunionModel> getListaTiposReuniones(string fechaInicio, string fechaFinal)
        {
            List<TipoReunionModel> listaTipoReunionesModel = new List<TipoReunionModel>();

            int valido = 0;
            //se crea la conexion            
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta                
                string sqlQuery = $"exec Sp_Nombre_Tipo_Reunion_Mes @TC_Fecha_Inicio = '{fechaInicio}', @TC_Fecha_Final = '{fechaFinal}'";
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
                
                            TipoReunionModel tiposDeReunion = new TipoReunionModel();

                            
                            tiposDeReunion.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();
                            tiposDeReunion.TN_Cantidad_Tipo_Reunion = Int32.Parse(productoReader["cantidad"].ToString());
                          
                            listaTipoReunionesModel.Add(tiposDeReunion);
                        }

                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return listaTipoReunionesModel;
            }
            else
            {
                return null;
            }

        }



        ///PARA UNICA REUNION


        //Retorna una lista con la cantidad de asistentes que tuvo una reunion
        public CantidadAsistentesModel getAsistenciaReunionUnica(string idReuninio)
        {

            int valido = 0;
            CantidadAsistentesModel reunion = new CantidadAsistentesModel();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Asistencia_Reunion_Unica @TN_Id_Reunion = '{idReuninio}'";
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
                            reunion.TN_Usuarios_Asistieron = Int32.Parse(productoReader["TN_Usuarios_Asistieron"].ToString());
                            reunion.TN_Usuarios_Faltaron = Int32.Parse(productoReader["TN_Usuarios_Faltaron"].ToString());
                        }

                    } // while
                     //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return reunion;
            }
            else
            {
                return null;
            }

        }

        //Retorna la cantiad de dinero de una unica reunion en las fechas que se pasan por parametro
        public int getDineroReunionUnica(string idReunion)
        {

            int valido = 0;
            int cantidadDinero = 0;

            string connectionString = Configuration["ConnectionStrings:DB_Connection"];

            //se crea la conexion
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Cantidad_Dinero_Por_Reunion @TN_Id_Reunion = '{idReunion}'";
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
                            cantidadDinero = Int32.Parse(productoReader["TN_Cantidad_Dinero_Reuniones"].ToString());
                        }

                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return cantidadDinero;
            }
            else
            {
                return 0;
            }

        }


        //retorna la cantidad de tiempo invertido de una unica reunion
        public string getDuracionReunionUnica(string idReunion)
        {
            int valido = 0;
            string duracionReunion = "";

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Tiempo_Por_Reunion @TN_Id_Reunion = '{idReunion}'";
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
                            duracionReunion = productoReader["duracion"].ToString();
                        }

                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return duracionReunion;
            }
            else
            {
                return "";
            }

        }

    }
}

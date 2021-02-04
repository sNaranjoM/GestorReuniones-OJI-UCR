using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using Microsoft.Extensions.Configuration;
namespace GestorReunionesProyectoAnalisis.Data
{
    public class DataArchivo
    {

        public IConfiguration Configuration { get; }

        public DataArchivo(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        //retorna la lista de archivos
        public List<ArchivoModel> listarArchivos(string idElemento)
        {
            List<ArchivoModel> ListaArchivosReunion = new List<ArchivoModel>();

            int valido = 0;

            //se crea la conexion            
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta                
                string sqlQuery = $"exec Sp_Listar_Archivo_Reunion @TN_Id_Reunion  ='{idElemento}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta                    
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta
                    
                    while (productoReader.Read())
                    {

                        ArchivoModel archivoReunion = new ArchivoModel();
                        //Si valido =1 se extraen los valores de esa reunion, sino fue que fallo la consulta o no tiene archivos                        
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        if (valido==1) {
                            archivoReunion.TN_Id_Archivo = productoReader["TN_Id_Archivo"].ToString();
                            archivoReunion.TC_Nombre_Archivo = productoReader["TC_Link"].ToString();
                            archivoReunion.TC_Link = "\\files\\Reuniones\\TN_Id_Reunion_" + idElemento + "\\" + productoReader["TC_Link"].ToString();                          
                        }

                        ListaArchivosReunion.Add(archivoReunion);

                    } // while
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            if (valido == 1)
            {
                return ListaArchivosReunion;
            }
            else
            {
                return null;
            }
        }


        //Eliminar los archivos de la base de datos
        public bool EliminarArchivo(string TN_Id_Archivo)
        {

            int valido = 0;

            //se crea la conexion           
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta             
                string sqlQuery = $"exec Sp_Delete_Archivo_Reunion_Unico @TN_Id_Archivo  ='{TN_Id_Archivo}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta                   
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();
                    //Se hace lectura de lo que nos retorno la consulta                    
                    while (productoReader.Read())
                    {
                        //Si valido =1 es que la consulta se realizo con exito
                        
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

    }
}

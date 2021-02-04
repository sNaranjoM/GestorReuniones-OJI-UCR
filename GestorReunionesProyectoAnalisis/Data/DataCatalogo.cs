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
    public class DataCatalogo
    {
        public IConfiguration Configuration { get; }

        public DataCatalogo(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor 

        //Crear Roles
        public bool CrearRol(RolModel rolModel)
        {

            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Rol @TC_Nombre_Rol ='{rolModel.TC_Nombre_Rol}', @TN_ID_Permiso ='{rolModel.TN_ID_Permiso}'";
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

        //Crear Puesto
        public bool CrearPuesto(PuestoModel puestoModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Puesto @TC_Nombre_Puesto='{puestoModel.TC_Nombre_Puesto}', @TN_Salario ='{puestoModel.TN_Salario}'";
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

        

        //Crear tipo de reunion
        public bool CrearTipoReunion(TipoReunionModel reunionModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Tipo_Reunion @TC_Nombre_Tipo_Reunion='{reunionModel.TC_Nombre_Tipo_Reunion}'";
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
        
        //Crear oficina
        public bool CrearOficina(OficinaModel oficinaModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Oficina @TC_Nombre='{oficinaModel.TC_Nombre}', @TC_Codigo='{oficinaModel.TC_Codigo}', @TN_Id_Circuito='{oficinaModel.TN_Id_Circuito}', @TF_Inicio_Vigencia='{oficinaModel.TF_Inicio_Vigencia}'";
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



        //Modificar tipo Reunion 
        public bool ModificarTipoReunion(TipoReunionModel reunionModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Tipo_Reunion @TN_Id_Tipo_Reunion='{reunionModel.TN_Id_Tipo_Reunion}', @TC_Nombre_Tipo_Reunion ='{reunionModel.TC_Nombre_Tipo_Reunion}'";
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


        //Eliminar Tipo Reunion
        public bool EliminarTipoReunion(TipoReunionModel reunionModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Tipo_Reunion @TN_Id_Tipo_Reunion='{reunionModel.TN_Id_Tipo_Reunion}'";
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


        //Modificar Oficina 
        public bool ModificarOficina(OficinaModel oficinaModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Oficina @TN_Id_Oficina='{oficinaModel.TN_Id_Oficina}', @TC_Nombre ='{oficinaModel.TC_Nombre}', @TC_Codigo ='{oficinaModel.TC_Codigo}'";
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


        //Eliminar Oficina
        public bool EliminarOficina(OficinaModel oficinaModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Oficina @TN_Id_Oficina='{oficinaModel.TN_Id_Oficina}'";
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


        //Modificar Puesto 
        public bool ModificarPuesto(PuestoModel puestoModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Puesto @TN_Id_Puesto='{puestoModel.TN_Id_Puesto}', @TC_Nombre_Puesto ='{puestoModel.TC_Nombre_Puesto}', @TN_Salario ='{puestoModel.TN_Salario}'";
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



        //Eliminar puesto
        public bool EliminarPuesto(PuestoModel puestoModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Puesto @TN_Id_Puesto='{puestoModel.TN_Id_Puesto}'";
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


        //Modificar Rol 
        public bool ModificarRol(RolModel rolModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Rol @TN_Id_Rol='{rolModel.TN_Id_Rol}', @TC_Nombre_Rol ='{rolModel.TC_Nombre_Rol}', @TN_ID_Permiso ='{rolModel.TN_ID_Permiso}'";
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



        //Eliminar Rol
        public bool EliminarRol(RolModel rolModel)
        {
            int valido = 0;

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Rol @TN_Id_Rol='{rolModel.TN_Id_Rol}'";
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






        //Listar circuitos
        public List<CircuitoModel> getListarCatalogoCircuito()
        {

            List<CircuitoModel> listaRetorno = new List<CircuitoModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Circuito";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        CircuitoModel listaCatalogoTemp = new CircuitoModel();
                        listaCatalogoTemp.TC_Des_Circuito = productoReader["TC_Des_Circuito"].ToString();
                        listaCatalogoTemp.TN_Id_Circuito = Int32.Parse(productoReader["TN_Id_Circuito"].ToString());
                        listaRetorno.Add(listaCatalogoTemp);
                    } // while
           
                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }

        //Listar permisos
        public List<PermisosModel> getListarCatalogoPermisos()
        {

            List<PermisosModel> listaRetorno = new List<PermisosModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Permiso";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        PermisosModel listaCatalogoTemp = new PermisosModel();        
                        
                        listaCatalogoTemp.TN_Id_Permiso = Int32.Parse(productoReader["TN_Id_Permiso"].ToString());
                        listaCatalogoTemp.TC_Nombre_Permiso = productoReader["TC_Nombre_Permiso"].ToString();

                        listaRetorno.Add(listaCatalogoTemp);
                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }


        //Listar puestos
        public List<PuestoModel> getListaCatalogoPuesto()
        {

            List<PuestoModel> listaRetorno = new List<PuestoModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Puesto";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        PuestoModel listaCatalogoTemp = new PuestoModel();
                        listaCatalogoTemp.TC_Nombre_Puesto = productoReader["TC_Nombre_Puesto"].ToString();
                        listaCatalogoTemp.TN_Id_Puesto = Int32.Parse(productoReader["TN_Id_Puesto"].ToString()); 
                        listaCatalogoTemp.TN_Salario = float.Parse(productoReader["TN_Salario"].ToString());


                        listaRetorno.Add(listaCatalogoTemp);
                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }

        //Listar oficinas
        public List<OficinaModel> getListaCatalogoOficina( )
        {

            List<OficinaModel> listaRetorno = new List<OficinaModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Oficina";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        OficinaModel listaCatalogoTemp = new OficinaModel();                        
                        listaCatalogoTemp.TC_Nombre = productoReader["TC_Nombre"].ToString();
                        listaCatalogoTemp.TN_Id_Oficina = Int32.Parse(productoReader["TN_Id_Oficina"].ToString());
                        listaCatalogoTemp.TC_Codigo = productoReader["TC_Codigo"].ToString();
                        listaRetorno.Add(listaCatalogoTemp);
                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }

        //Listar roles
        public List<RolModel> getListaCatalogoRol( )
        {

            List<RolModel> listaRetorno = new List<RolModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Rol";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        RolModel listaCatalogoTemp = new RolModel();
                        listaCatalogoTemp.TC_Nombre_Rol = productoReader["TC_Nombre_Rol"].ToString();
                        listaCatalogoTemp.TN_Id_Rol = Int32.Parse(productoReader["TN_Id_Rol"].ToString());
                        listaCatalogoTemp.TC_Nombre_Permiso = productoReader["TC_Nombre_Permiso"].ToString();
                        listaCatalogoTemp.TN_ID_Permiso = Int32.Parse(productoReader["TN_ID_Permiso"].ToString());

                        listaRetorno.Add(listaCatalogoTemp);
                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }


        //Se encargar de extraer los tipos de reunion, con su respectivo Id parea trabajarlo en su crud en su respectiva view
        public List<TipoReunionModel> getListarTipoReunion()
        {

            List<TipoReunionModel> listaRetorno = new List<TipoReunionModel>();

            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Tipo_Reunion";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        TipoReunionModel listaCatalogoTemp = new TipoReunionModel();
                        listaCatalogoTemp.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();
                        listaCatalogoTemp.TN_Id_Tipo_Reunion = Int32.Parse(productoReader["TN_Id_Tipo_Reunion"].ToString());
                        listaRetorno.Add(listaCatalogoTemp);
                    } // while

                    //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }

    }
}

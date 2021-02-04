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
    public class DataReunion
    {
        public IConfiguration Configuration { get; }

        public DataReunion(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        public int[] CrearReunion(ReunionModel reunionModel, string idUsuario)
        {

            int[] valido = { 0, 0 };
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Reunion @TC_Nombre_Reunion ='{reunionModel.TC_Nombre_Reunion}', @TN_Id_Tipo_Reunion  ='{reunionModel.TN_Id_Tipo_Reunion}' , " +
                     $"@TC_Descripcion ='{reunionModel.TC_Descripcion}' , @TC_Comentario ='{reunionModel.TC_Comentario}', @TC_Lugar ='{reunionModel.TC_Lugar}', @TC_Fecha_Inicio ='{reunionModel.TC_Fecha_Reunion}', " +
                     $"@TC_Lista_Usuarios ='{reunionModel.TC_Lista_Usuarios}', @TC_Lista_Temas ='{reunionModel.TC_Lista_Temas}', @TC_Lista_Tareas ='{reunionModel.TC_Lista_Tareas}', @TC_Lista_Archivos ='{reunionModel.TC_Lista_Archivos}', @TC_Usuario ='{idUsuario}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {

                        //Si valido =1 se extren todos los valores
                        valido[0] = Int32.Parse(productoReader["valido"].ToString());
                        if (valido[0] == 1)
                        {
                            valido[1] = Int32.Parse(productoReader["TN_Id_Reunion"].ToString());
                        }

                    }
                    //Se cierra la conexion a la base de datos
                    connection.Close();

                }
            }

            return valido;

        }

        //Modificar Tarea
        public bool ModificarReunion(ReunionModel reunionModel)
        {
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Update_Reunion @TN_Id_Reunion ='{reunionModel.TN_Id_Reunion}', @TC_Nombre_Reunion ='{reunionModel.TC_Nombre_Reunion}', @TN_Id_Tipo_Reunion  ='{reunionModel.TN_Id_Tipo_Reunion}" +
                   $"' , @TC_Descripcion ='{reunionModel.TC_Descripcion}' , @TC_Comentario ='{reunionModel.TC_Comentario}', @TC_Lugar ='{reunionModel.TC_Lugar}', @TC_Fecha_Inicio ='{reunionModel.TC_Fecha_Reunion}" +
                   $"', @TC_Lista_Usuarios ='{reunionModel.TC_Lista_Usuarios}', @TC_Lista_Temas ='{reunionModel.TC_Lista_Temas}', @TC_Lista_Tareas ='{reunionModel.TC_Lista_Tareas}', @TC_Lista_Archivos ='{reunionModel.TC_Lista_Archivos}'";
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

            //si valido=1 significa que se realizo con exito la actualizacion
            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TerminarReunion(string IdReunionTerminada)
        {
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Finalizar_Reunion @TN_Id_Reunion ='{IdReunionTerminada}'";
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

            //si valido=1 significa que se logro finalizar la reunion con exito
            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


            public List<TemasModel> ListarTemasReunion(string IdReunion)
        {
            int valido = 0;
            List<TemasModel> listaRetorno = new List<TemasModel>();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Reunion_Temas @TN_Id_Reunion ='{IdReunion}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
       
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    while (productoReader.Read())
                    {

                        //Se hace lectura de lo que nos retorno la consulta
                        TemasModel listaTemasReunionTemp = new TemasModel();
                        valido = Int32.Parse(productoReader["valido"].ToString());

                        //Si valido =1 se extren todos los valores
                        if (valido==1) {
                            listaTemasReunionTemp.TN_Id_Temas = Int32.Parse(productoReader["TN_Id_Temas"].ToString());
                            listaTemasReunionTemp.TC_Nombre_Tema = productoReader["TC_Nombre_Tema"].ToString();
                            listaRetorno.Add(listaTemasReunionTemp);
                        }
                    }
                    //Se cierra la conexion a la base de datos
                    connection.Close();

                }
            }

            //si valido=1 se retorna la lista conmpleta de temas en base a la reunio que se especifique
            if (valido == 1)
            {
                return listaRetorno;
            }
            else
            {
                return null;
            }
        }
        


        public bool validarFechaReunion(string IdReunionAsistir, string usuario)
        {
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Valida_Fecha_Reunion @TN_Id_Reunion ='{IdReunionAsistir}', @TC_Usuario = '{usuario}'";
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

            //si valido=1 significa que el usuario si puede ingresar a la reunion disgnada
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
        public List<ReunionModel> EliminarReunion(string idReunion)
        {

            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Delete_Reunion @TN_Id_Reunion  ='{idReunion}'";
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

            //si valido=1 significa que la reunion se borro con exito
            if (valido == 1)
            {
                return getListarReunion();
            }
            else
            {
                return getListarReunion();
            }

        }



        //Listar Reuniones para poder modificar y eliminar
        public List<ReunionModel> getListarReunion()
        {

            List<ReunionModel> listaRetorno = new List<ReunionModel>();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Reunion";
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
                        listaReunionTemp.TN_Id_Reunion = Int32.Parse(productoReader["TN_Id_Reunion"].ToString());
                        listaReunionTemp.TC_Nombre_Reunion = productoReader["TC_Nombre_Reunion"].ToString();
                        listaReunionTemp.TN_Id_Tipo_Reunion = Int32.Parse(productoReader["TN_Id_Tipo_Reunion"].ToString());                     
                        listaReunionTemp.TC_Descripcion = productoReader["TC_Descripcion"].ToString();
                        listaReunionTemp.TC_Comentario = productoReader["TC_Comentario"].ToString();
                        listaReunionTemp.TC_Lugar = productoReader["TC_Lugar"].ToString();
                        listaReunionTemp.TC_Fecha_Reunion = DateTime.Parse (productoReader["TC_Fecha_Inicio"].ToString());
                        listaReunionTemp.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();
                        listaReunionTemp.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();
                        listaReunionTemp.Estado = productoReader["TN_Finalizada"].ToString();

                        listaRetorno.Add(listaReunionTemp);
                    } // while

                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }



        //Listar Reuniones Finalizadas para poder ver historial
        public List<ReunionModel> getListarReunionFinalizadas()
        {

            List<ReunionModel> listaRetorno = new List<ReunionModel>();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Reunion_Finalizada";
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
                        listaReunionTemp.TN_Id_Reunion = Int32.Parse(productoReader["TN_Id_Reunion"].ToString());
                        listaReunionTemp.TC_Nombre_Reunion = productoReader["TC_Nombre_Reunion"].ToString();
                        listaReunionTemp.TN_Id_Tipo_Reunion = Int32.Parse(productoReader["TN_Id_Tipo_Reunion"].ToString());
                        listaReunionTemp.TC_Descripcion = productoReader["TC_Descripcion"].ToString();
                        listaReunionTemp.TC_Comentario = productoReader["TC_Comentario"].ToString();
                        listaReunionTemp.TC_Lugar = productoReader["TC_Lugar"].ToString();
                        listaReunionTemp.TC_Fecha_Reunion = DateTime.Parse(productoReader["TC_Fecha_Inicio"].ToString());
                        listaReunionTemp.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();

                        listaRetorno.Add(listaReunionTemp);
                    } // while

                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }

            return listaRetorno;
        }


        public ReunionModel getReunionModificar(string idReunion)
        {
            int valido = 0;
            ReunionModel listaReunionTemp = new ReunionModel();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Unico_Reunion @TN_Id_Reunion  ='{idReunion}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        //Si valido =1 se extren todos los valores
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        if (valido == 1) {
                            listaReunionTemp.TN_Id_Reunion = Int32.Parse(productoReader["TN_Id_Reunion"].ToString());
                            listaReunionTemp.TC_Nombre_Reunion = productoReader["TC_Nombre_Reunion"].ToString();
                            listaReunionTemp.TN_Id_Tipo_Reunion = Int32.Parse(productoReader["TN_Id_Tipo_Reunion"].ToString());
                            listaReunionTemp.TC_Descripcion = productoReader["TC_Descripcion"].ToString();
                            listaReunionTemp.TC_Comentario = productoReader["TC_Comentario"].ToString();
                            listaReunionTemp.TC_Lugar = productoReader["TC_Lugar"].ToString();
                            listaReunionTemp.TC_Fecha_Reunion = DateTime.Parse(productoReader["TC_Fecha_Inicio"].ToString());
                            listaReunionTemp.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();
                            listaReunionTemp.TC_Lista_Usuarios = productoReader["TC_Lista_Usuarios"].ToString();
                            listaReunionTemp.TC_Lista_Tareas = productoReader["TC_Lista_Tareas"].ToString();
                        }


                    } // while

                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }
            //si valido=1 significa que la reunion se encontro y se retornara un modelo con todos los datos de la reunion
            if (valido == 1)
            {
                return listaReunionTemp;
            }
            else
            {
                return null;
            }

        }



        public bool AgregarAcuerdosTareas(string idTarea, string acuerdoTarea)
        {
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Tarea_Acuerdo @TN_Id_Tarea ='{idTarea}', @TC_Acuerdo = '{acuerdoTarea}' ";
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
            //si valido=1 significa que se logro insertar el acuerdo en la tarea
            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool AgregarAcuerdosTemas(string idTemas, string acuerdoTema)
        {
            int valido = 0;
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Insert_Temas_Acuerdo @TN_Id_Temas ='{idTemas}', @TC_Acuerdo = '{acuerdoTema}' ";
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
            //si valido=1 significa que el acuerdo de los temas fueron insertados correctamente
            if (valido == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public string UsuarioCreadorReunion(string IdReunionAsistir)
        {
            string usuarioCreadorReunion = "";
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Validar_Host @TN_Id_Reunion = '{IdReunionAsistir}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Se abre y se ejecuta la consulta
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader productoReader = command.ExecuteReader();

                    //Se hace lectura de lo que nos retorno la consulta
                    while (productoReader.Read())
                    {
                        usuarioCreadorReunion = productoReader["TC_Usuario"].ToString();

                    }

                    //Se cierra la conexion a la base de datos
                    connection.Close();

                }
            }

            return usuarioCreadorReunion;

        }



        public ReunionModel getReunionMinuta(string idReunion)
        {
            int valido = 0;
            ReunionModel reunionTemp = new ReunionModel();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Unico_Reunion_Minuta @TN_Id_Reunion  ='{idReunion}'";
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
                        reunionTemp.TN_Id_Reunion = Int32.Parse(productoReader["TN_Id_Reunion"].ToString());
                        reunionTemp.TC_Nombre_Reunion = productoReader["TC_Nombre_Reunion"].ToString();
                        reunionTemp.TN_Id_Tipo_Reunion = Int32.Parse(productoReader["TN_Id_Tipo_Reunion"].ToString());
                        reunionTemp.TC_Descripcion = productoReader["TC_Descripcion"].ToString();
                        reunionTemp.TC_Comentario = productoReader["TC_Comentario"].ToString();
                        reunionTemp.TC_Fecha_Inicio = productoReader["TC_Fecha_Inicio"].ToString();
                        reunionTemp.TC_Fecha_Final = productoReader["TC_Fecha_Final"].ToString();
                        reunionTemp.TC_Nombre_Usuario = productoReader["TC_Nombre_Usuario"].ToString();
                        reunionTemp.TC_Nombre_Tipo_Reunion = productoReader["TC_Nombre_Tipo_Reunion"].ToString();

                    } // while

                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }
            //si valido=1 significa que la reunion se encontro y se prodece a retornar la infromacion de dicha reunion para agregarla a la minuta
            if (valido == 1)
            {
                return reunionTemp;
            }
            else
            {
                return null;
            }

        }




        public List<ReunionModel> getTemasMinuta(string idReunion)
        {
            int valido = 0;
            List<ReunionModel> listaRetorno = new List<ReunionModel>();
            //se crea la conexion
            string connectionString = Configuration["ConnectionStrings:DB_Connection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //se escribe la consulta
                string sqlQuery = $"exec Sp_Listar_Temas_Minuta @TN_Id_Reunion  ='{idReunion}'";
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
                        valido = Int32.Parse(productoReader["valido"].ToString());
                        listaReunionTemp.TC_Nombre_Tema = productoReader["TC_Nombre_Tema"].ToString();
                        listaReunionTemp.TC_Acuerdo = productoReader["TC_Acuerdo"].ToString();
                        listaRetorno.Add(listaReunionTemp);


                    } // while
                      //Se cierra la conexion a la base de datos
                    connection.Close();
                }
            }
            //si valido=1 significa que los temas ligados a la reunion se encontraron, por lo tanto se va a retornar para mosntrarlos en la minuta
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


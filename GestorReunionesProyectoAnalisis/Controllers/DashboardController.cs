using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestorReunionesProyectoAplicada.Controllers
{
    public class DashboardController : Controller
    {
        public IConfiguration Configuration { get; }

        public DashboardController(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        //Muestra la vista de Dashboard
        public IActionResult Index()
        {
            ViewBag.INVALIDO = null;
            //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
            ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
            return View("DashboardView");
        }


        //Se encarga de cargar todos los datos en la vista de Dashboard
        public IActionResult CargarDashboard(string fechaInicio, string fechaFinal)
        {
           
            string [] FechaInicioSplit = fechaInicio.Split("-");
            string [] FechaFinalSplit = fechaFinal.Split("-");           

            //Se valida que la fecha de inicio sea menor a la fecha final
            if (Int32.Parse(FechaInicioSplit[0].ToString()) <= Int32.Parse(FechaFinalSplit[0].ToString()) && Int32.Parse(FechaInicioSplit[1].ToString()) <= Int32.Parse(FechaFinalSplit[1].ToString()) && Int32.Parse(FechaInicioSplit[2].ToString()) < Int32.Parse(FechaFinalSplit[2].ToString()) || Int32.Parse(FechaInicioSplit[0].ToString()) < Int32.Parse(FechaFinalSplit[0].ToString()) || Int32.Parse(FechaInicioSplit[0].ToString()) == Int32.Parse(FechaFinalSplit[0].ToString())&& Int32.Parse(FechaInicioSplit[1].ToString()) < Int32.Parse(FechaFinalSplit[1].ToString())) 
            {

                DashboardModel dashboardModel = new DashboardModel();

                BusinessDashboard businessDashboard = new BusinessDashboard(Configuration);
                //Trae los datos segun el rango de fechas cantidad de reuniones tiempo invertido, dinero invertido, asistentes, tipos de reunion esto para todas las reuniones.

                string listaCantidadReuniones = businessDashboard.getCantidadReuniones(fechaInicio, fechaFinal);
                string listaTiempoReuniones = businessDashboard.getCantidadTiempoReuniones(fechaInicio, fechaFinal);
                string listaDineroReuniones = businessDashboard.getCantidadDineroReuniones(fechaInicio, fechaFinal);


                List<CantidadAsistentesModel> ListaCantidadAsistentesModel= new List<CantidadAsistentesModel>();
                List<TipoReunionModel> listaTipoReunionesModel = new List<TipoReunionModel>();
                ListaCantidadAsistentesModel = businessDashboard.getCantidadAsistentesReuniones(fechaInicio, fechaFinal);
                listaTipoReunionesModel = businessDashboard.getListaTiposReuniones(fechaInicio, fechaFinal);



                dashboardModel.TC_Nombre_Meses = ExtraerMeses(FechaInicioSplit[1].ToString(), FechaFinalSplit[1].ToString(), FechaInicioSplit[0].ToString(), FechaFinalSplit[0].ToString());

                //Añade la cantidad de asistentes separados por una , a un string para luego cargarlo en los graficos mas facil
                for (int i = 0; i< ListaCantidadAsistentesModel.Count() ; i++) {
                    dashboardModel.TC_Asistencia_Meses += ListaCantidadAsistentesModel[i].asistencia;

                    if (i == ListaCantidadAsistentesModel.Count()-1)
                    {
                        break;
                    }
                    else {
                        dashboardModel.TC_Asistencia_Meses += ",";
                    }
                }
                dashboardModel.TC_Cantidad_Reunion = listaCantidadReuniones;

                //Añade los tipos de reuniones separados por una , a un string para luego cargarlo en los graficos mas facil
                for (int i = 0; i < listaTipoReunionesModel.Count(); i++)
                {
                    dashboardModel.TC_Tipo_Reunion += listaTipoReunionesModel[i].TN_Cantidad_Tipo_Reunion;
                    dashboardModel.TC_Nombre_Reunion += listaTipoReunionesModel[i].TC_Nombre_Tipo_Reunion;

                    if (i == listaTipoReunionesModel.Count() - 1)
                    {
                        break;
                    }
                    else
                    {
                        dashboardModel.TC_Tipo_Reunion += ",";
                        dashboardModel.TC_Nombre_Reunion += ",";
                    }
                }
             
                dashboardModel.TC_Dinero_Invertido = listaDineroReuniones;
                dashboardModel.TC_Tiempo_Invertido = listaTiempoReuniones;

                ViewBag.ListaDashboard = dashboardModel;

                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                return View("DashboardView");

            }
            else {

                ViewBag.INVALIDO = "La fecha final es menor a la de inicio";
                //El ViewBag.PermisoUsuario se utiliza controlar el inicio de session de un usuario y su respectivos permisos
                ViewBag.PermisoUsuario = HttpContext.Session.GetString("UsuarioPermiso");
                return View("DashboardView");

            }
          
        }


        //Se encarga de agregar los meses en un rango de fechas para luego mostrar en los graficos
        public string ExtraerMeses(string mesInicio, string mesFinal, string annoInicio, string annoFin) {
            string meses = "";
            int mesInicioValidar = Int32.Parse(mesInicio.ToString());
            int mesFinValidar = Int32.Parse(mesFinal.ToString());
            int annoInicioValidar = Int32.Parse(annoInicio.ToString());
            int annoFinValidar = Int32.Parse(annoFin.ToString());

            bool valido = true;
            while (valido) {

                if (mesInicioValidar == 13 && annoInicioValidar < annoFinValidar)
                {
                    annoInicioValidar = annoInicioValidar + 1;
                    mesInicioValidar = 1;
                }


                switch (mesInicioValidar) {
                    case 1:
                        meses += "Enero";
                        break;
                    case 2:
                        meses += "Febrero";
                        break;
                    case 3:
                        meses += "Marzo";
                        break;
                    case 4:
                        meses += "Abril";
                        break;
                    case 5:
                        meses += "Mayo";
                        break;
                    case 6:
                        meses += "Junio";
                        break;
                    case 7:
                        meses += "Julio";
                        break;
                    case 8:
                        meses += "Agosto";
                        break;
                    case 9:
                        meses += "Septiembre";
                        break;
                    case 10:
                        meses += "Octubre";
                        break;
                    case 11:
                        meses += "Noviembre";
                        break;
                    case 12:
                        meses += "Dicembre";
                        break;
                    default:
                        meses = "";
                        break;                        
                }
               

                if (annoInicioValidar == annoFinValidar)
                {
                    if (mesInicioValidar < mesFinValidar)
                    {
                        meses += ",";
                        mesInicioValidar++;
                    }
                    else
                    {
                        valido = false;
                    }

                }
                else if(annoInicioValidar < annoFinValidar)
                {
                    meses += ",";
                    mesInicioValidar++;                  

                }
                

            }

            return meses;
        
        }

    }
}

using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAplicada.Business
{
    public class BusinessDashboard
    {
        public IConfiguration Configuration { get; }

        public BusinessDashboard(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        public string getCantidadReuniones(string fechaInicio, string fechaFinal)
        {
            DataDashboard dataTarea = new DataDashboard(Configuration);
            return dataTarea.getCantidadReuniones(fechaInicio, fechaFinal);
        }


        public string getCantidadTiempoReuniones(string fechaInicio, string fechaFinal)
        {
            DataDashboard dataTarea = new DataDashboard(Configuration);
            return dataTarea.getCantidadTiempoReuniones(fechaInicio, fechaFinal);
        }


        public string getCantidadDineroReuniones(string fechaInicio, string fechaFinal)
        {
            DataDashboard dataTarea = new DataDashboard(Configuration);
            return dataTarea.getCantidadDineroReuniones(fechaInicio, fechaFinal);
        }

        public List<CantidadAsistentesModel> getCantidadAsistentesReuniones(string fechaInicio, string fechaFinal)
        {
            DataDashboard dataTarea = new DataDashboard(Configuration);
            return dataTarea.getCantidadAsistentesReuniones(fechaInicio, fechaFinal);
        }

        public List<TipoReunionModel> getListaTiposReuniones(string fechaInicio, string fechaFinal)
        {
            DataDashboard dataTarea = new DataDashboard(Configuration);
            return dataTarea.getListaTiposReuniones(fechaInicio, fechaFinal);
        }
     
        public CantidadAsistentesModel getAsistenciaReunionUnica(string idReunion)
        {

            DataDashboard data = new DataDashboard(Configuration);
            return data.getAsistenciaReunionUnica(idReunion);
        }


        public int getDineroReunionUnica(string idReunion)
        {
            DataDashboard data = new DataDashboard(Configuration);
            return data.getDineroReunionUnica(idReunion);

        }

        public string getDuracionReunionUnica(string idReunion)
        {

            DataDashboard data = new DataDashboard(Configuration);
            return data.getDuracionReunionUnica(idReunion);
        }





        }
}

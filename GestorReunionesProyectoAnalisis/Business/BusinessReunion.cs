using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAplicada.Business
{
    public class BusinessReunion
    {
        public IConfiguration Configuration { get; }

        public BusinessReunion(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor


        public int[] CrearReunion(ReunionModel reunionModel, string idUsuario)
        {
            DataReunion dataTarea = new DataReunion(Configuration);
            return dataTarea.CrearReunion(reunionModel, idUsuario);

        }

        public bool TerminarReunion(string IdReunionTerminada) {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.TerminarReunion(IdReunionTerminada);
        }


        public bool ModificarReunion(ReunionModel reunionModel)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.ModificarReunion(reunionModel);

        }

        public bool validarFechaReunion(string IdReunionAsistir, string usuario)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.validarFechaReunion(IdReunionAsistir, usuario);

        }


        public List<ReunionModel> EliminarReunion(string idReunion)
        {
            DataReunion dataTarea = new DataReunion(Configuration);
            return dataTarea.EliminarReunion(idReunion);

        }

        public List<TemasModel> ListarTemasReunion(string idReunion)
        {
            DataReunion dataTarea = new DataReunion(Configuration);
            return dataTarea.ListarTemasReunion(idReunion);

        }
        

        public List<ReunionModel> getListarReunion()
        {
            DataReunion dataTarea = new DataReunion(Configuration);
            return dataTarea.getListarReunion();
        }

        public List<ReunionModel> getListarReunionFinalizadas()
        {
            DataReunion dataTarea = new DataReunion(Configuration);
            return dataTarea.getListarReunionFinalizadas();
        }

        public ReunionModel ResumenReunion(string idReunion)
        {
            DataReunion dataTarea = new DataReunion(Configuration);
            return dataTarea.getReunionModificar(idReunion);
        }


        public ReunionModel getReunionModificar(string idReunion)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.getReunionModificar(idReunion);
        }



        public bool AgregarAcuerdosTareas(string idTarea, string acuerdoTarea)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.AgregarAcuerdosTareas(idTarea, acuerdoTarea);

        }



        public bool AgregarAcuerdosTemas(string idTemas, string acuerdoTema)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.AgregarAcuerdosTemas(idTemas, acuerdoTema);

        }

        public string UsuarioCreadorReunion(string IdReunionAsistir) {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.UsuarioCreadorReunion(IdReunionAsistir);
        }


        public ReunionModel getReunionMinuta(string idReunion)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.getReunionMinuta(idReunion);
        }

        public List<ReunionModel> getTemasMinuta(string idReunion)
        {
            DataReunion dataReunion = new DataReunion(Configuration);
            return dataReunion.getTemasMinuta(idReunion);
        }



    }
}

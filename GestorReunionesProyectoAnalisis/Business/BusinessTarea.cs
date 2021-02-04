using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAplicada.Business
{
    public class BusinessTarea
    {
        public IConfiguration Configuration { get; }

        public BusinessTarea(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        public bool CrearTarea(TareaModel tareaModel)
        {
            DataTarea dataTarea = new DataTarea(Configuration);
            return dataTarea.CrearTarea(tareaModel);

        }

        public bool ModificarTarea(TareaModel tareaModel)
        {
            DataTarea dataTarea = new DataTarea(Configuration);
            return dataTarea.ModificarTarea(tareaModel);

        }

        public List<TareaModel> EliminarTarea(string idTarea)
        {
            DataTarea dataTarea = new DataTarea(Configuration);
            return dataTarea.EliminarTarea(idTarea);

        }

        public List<TareaModel> getListarTarea()
        {
            DataTarea dataTarea = new DataTarea(Configuration);
            return dataTarea.getListarTarea();
        }


        public List<TareaModel> getListarTareaReunion(string idReunion)
        {
            DataTarea dataTarea = new DataTarea(Configuration);
            return dataTarea.getListarTareaReunion(idReunion);
        }

        public List<TareaModel> getListarTareaMinuta(string idTarea)
        {
            DataTarea dataTarea = new DataTarea(Configuration);
            return dataTarea.getListarTareaMinuta(idTarea);
        }


        public TareaModel getTareaModificar(string idTarea)
        {
            DataTarea dataUsuario = new DataTarea(Configuration);
            return dataUsuario.getTareaModificar(idTarea);
        }

    }
}

using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAplicada.Business
{
    public class BusinessAgenda
    {
        public IConfiguration Configuration { get; }

        public BusinessAgenda(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor

        public List<ReunionModel> getReuniones(string usuario)
        {
            DataAgenda dataAgenda = new DataAgenda(Configuration);
            return dataAgenda.getReunionesData(usuario);

        }



        public List<UsuarioModel> getAsistentesReunion(string IdReunionAsistentes)
        {
            DataAgenda dataAgenda = new DataAgenda(Configuration);
            return dataAgenda.getAsistentesReunion(IdReunionAsistentes);

        }


        public List<TareaModel> getTareasReunion(string IdReunionTareas)
        {
            DataAgenda dataAgenda = new DataAgenda(Configuration);
            return dataAgenda.getTareasReunion(IdReunionTareas);

        }

    }
}

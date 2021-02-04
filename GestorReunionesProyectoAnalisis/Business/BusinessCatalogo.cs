using GestorReunionesProyectoAnalisis.Models;
using GestorReunionesProyectoAplicada.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestorReunionesProyectoAplicada.Business
{
    public class BusinessCatalogo
    {
        public IConfiguration Configuration { get; }

        public BusinessCatalogo(IConfiguration configuration)
        {
            Configuration = configuration;
        } // constructor
        
        //Crear Rol
        public bool CrearRol(RolModel rolModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.CrearRol(rolModel);
        }

        //Crear puesto
        public bool CrearPuesto(PuestoModel puestoModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.CrearPuesto(puestoModel);
        }

        //Crear Tipo de Reunion
        public bool CrearTipoReunion(TipoReunionModel reunionModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.CrearTipoReunion(reunionModel);
        }


        //Crear Oficina
        public bool CrearOficina(OficinaModel oficinaModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.CrearOficina(oficinaModel);
        }

        //Modificar Tipo Reunion
        public bool ModificarTipoReunion(TipoReunionModel reunionModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.ModificarTipoReunion(reunionModel);
        }


        //Eliminar Tipo Reunion
        public bool EliminarTipoReunion(TipoReunionModel reunionModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.EliminarTipoReunion(reunionModel);
        }

        //Modificar Oficina
        public bool ModificarOficina(OficinaModel oficinaModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.ModificarOficina(oficinaModel);
        }

        //Eliminar Oficina
        public bool EliminarOficina(OficinaModel oficinaModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.EliminarOficina(oficinaModel);
        }

        //Modificar Puesto
        public bool ModificarPuesto(PuestoModel puestoModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.ModificarPuesto(puestoModel);
        }

        //Eliminar Puesto
        public bool EliminarPuesto(PuestoModel puestoModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.EliminarPuesto(puestoModel);
        }

        //Modificar Rol
        public bool ModificarRol(RolModel rolModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.ModificarRol(rolModel);
        }

        //Eliminar Rol
        public bool EliminarRol(RolModel rolModel)
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.EliminarRol(rolModel);
        }

        //Listar Permisos
        public List<PermisosModel> getListarCatalogoPermisos()
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.getListarCatalogoPermisos();
        }


        //Listar Circuitos
        public List<CircuitoModel> getListarCatalogoCircuito()
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.getListarCatalogoCircuito();
        }

        //Listar Puestos
        public List<PuestoModel> getListarCatalogoPuesto() {

            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.getListaCatalogoPuesto();

        }

        //Listar Oficinas
        public List<OficinaModel> getListarCatalogoOficina()
        {

            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.getListaCatalogoOficina();

        }

        //Listar Roles
        public List<RolModel> getListarCatalogoRol()
        {

            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.getListaCatalogoRol();
            
        }

        //Listar Roles
        public List<TipoReunionModel> getListarTipoReunion()
        {
            DataCatalogo dataCatalogo = new DataCatalogo(Configuration);
            return dataCatalogo.getListarTipoReunion();

        }
        



    }
}

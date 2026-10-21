using System;
using System.Collections.Generic;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaHospedaje
    {
        public static void Agregar(Hospedaje pHospedaje)
        {
            try
            {
                PersistenciaHospedaje.Agregar(pHospedaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Modificar(Hospedaje pHospedaje)
        {
            try
            {
                PersistenciaHospedaje.Modificar(pHospedaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Eliminar(Hospedaje pHospedaje)
        {
            try
            {
                PersistenciaHospedaje.Eliminar(pHospedaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Hospedaje Buscar(string pCodigoInterno)
        {
            try
            {
                return PersistenciaHospedaje.Buscar(pCodigoInterno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Hospedaje> ListarHospedajes()
        {
            try
            {
                return PersistenciaHospedaje.ListarHospedajes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Hospedaje> ListarHospedajesPorEstado(string pCodigoEstado)
        {
            try
            {
                return PersistenciaHospedaje.ListarHospedajesPorEstado(pCodigoEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
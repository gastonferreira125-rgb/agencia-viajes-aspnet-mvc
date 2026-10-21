using System;
using System.Collections.Generic;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaEstado
    {
        public static void Agregar(Estado pEstado)
        {
            try
            {
                PersistenciaEstado.Agregar(pEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Modificar(Estado pEstado)
        {
            try
            {
                PersistenciaEstado.Modificar(pEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Eliminar(Estado pEstado)
        {
            try
            {
                PersistenciaEstado.Eliminar(pEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Estado Buscar(string pCodigoEstado)
        {
            try
            {
                return PersistenciaEstado.Buscar(pCodigoEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Estado> ListarEstados()
        {
            try
            {
                return PersistenciaEstado.ListarEstados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaServicio
    {
        public static void Agregar(Servicio pServicio)
        {
            try
            {
                PersistenciaServicio.Agregar(pServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Modificar(Servicio pServicio)
        {
            try
            {
                PersistenciaServicio.Modificar(pServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Eliminar(Servicio pServicio)
        {
            try
            {
                PersistenciaServicio.Eliminar(pServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Servicio Buscar(int pNumeroOmnibus, DateTime pFechaHoraPartida)
        {
            try
            {
                return PersistenciaServicio.Buscar(
                    pNumeroOmnibus,
                    pFechaHoraPartida
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Servicio> ListarServicios()
        {
            try
            {
                return PersistenciaServicio.ListarServicios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Servicio> ListarServiciosFuturos()
        {
            try
            {
                return PersistenciaServicio.ListarServiciosFuturos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
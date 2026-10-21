using System;
using System.Collections.Generic;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaPaqueteViaje
    {
        public static int Agregar(PaqueteViaje pPaqueteViaje)
        {
            try
            {
                return PersistenciaPaqueteViaje.Agregar(pPaqueteViaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PaqueteViaje Buscar(int pIdPaquete)
        {
            try
            {
                return PersistenciaPaqueteViaje.Buscar(pIdPaquete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PaqueteViaje> ListarPaquetesDisponibles()
        {
            try
            {
                return PersistenciaPaqueteViaje.ListarPaquetesDisponibles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PaqueteViaje> ListarPaquetesPorEstado(
            string pCodigoEstado)
        {
            try
            {
                return PersistenciaPaqueteViaje.ListarPaquetesPorEstado(
                    pCodigoEstado
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PaqueteViaje> ListarPaquetesPorServicio(
            int pNumeroOmnibus,
            DateTime pFechaHoraPartida)
        {
            try
            {
                return PersistenciaPaqueteViaje.ListarPaquetesPorServicio(
                    pNumeroOmnibus,
                    pFechaHoraPartida
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
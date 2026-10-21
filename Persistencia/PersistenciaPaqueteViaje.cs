using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaPaqueteViaje
    {
        public static int Agregar(PaqueteViaje pPaqueteViaje)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("AltaPaqueteViaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //faltaban cantdias, precioindiv, base doble y triple los agrego
            oComando.Parameters.AddWithValue("@Titulo", pPaqueteViaje.Titulo);
            oComando.Parameters.AddWithValue("@Descripcion", pPaqueteViaje.Descripcion);
            oComando.Parameters.AddWithValue("@CodigoEstado", pPaqueteViaje.Estado.CodigoEstado);
            oComando.Parameters.AddWithValue("@CantidadDias", pPaqueteViaje.CantidadDias);
            oComando.Parameters.AddWithValue("@PrecioIndividual", pPaqueteViaje.PrecioIndividual);
            oComando.Parameters.AddWithValue("@PrecioBaseDoble", pPaqueteViaje.PrecioDoble);
            oComando.Parameters.AddWithValue("@PrecioBaseTriple", pPaqueteViaje.PrecioTriple);
            oComando.Parameters.AddWithValue("@NumeroOmnibus", pPaqueteViaje.Servicio.NumeroOmnibus);
            oComando.Parameters.AddWithValue("@FechaHoraPartidaServicio", pPaqueteViaje.Servicio.FechaHoraPartida);
            oComando.Parameters.AddWithValue("@CodigoInternoHospedaje", pPaqueteViaje.Hospedaje.CodigoInterno);
            oComando.Parameters.AddWithValue("@CantidadNochesAlojamiento", pPaqueteViaje.CantidadNoches);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El Estado no existe.");

                if (retorno == -2)
                    throw new Exception("El Servicio no existe.");

                if (retorno == -3)
                    throw new Exception("El Hospedaje no existe.");

                if (retorno == -4)
                    throw new Exception("El Hospedaje no pertenece al Estado destino del paquete.");

                if (retorno == -5)
                    throw new Exception("Error al agregar el Paquete de Viaje.");

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }
        public static PaqueteViaje Buscar(int pIdPaquete)
        {
            PaqueteViaje oPaqueteViaje = null;

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("BuscarPaqueteViaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@IdPaquete", pIdPaquete);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    Estado oEstado = new Estado(
                        oReader["CodigoEstado"].ToString(),
                        oReader["NombreEstado"].ToString(),
                        oReader["Pais"].ToString()
                    );

                    Servicio oServicio = new Servicio(
                        Convert.ToInt32(oReader["NumeroOmnibus"]),
                        Convert.ToDateTime(oReader["FechaHoraPartida"]),
                        Convert.ToDateTime(oReader["FechaHoraLlegada"]),
                        Convert.ToDecimal(oReader["PrecioPasajeIndividual"])
                    );

                    Hospedaje oHospedaje = new Hospedaje(
                        oReader["CodigoInterno"].ToString(),
                        oReader["Nombre"].ToString(),
                        oReader["Direccion"].ToString(),
                        oReader["TipoHospedaje"].ToString(),
                        Convert.ToDecimal(oReader["PrecioPorNoche"]),
                        oEstado
                    );

                    oPaqueteViaje = new PaqueteViaje(
                        Convert.ToInt32(oReader["IdPaquete"]),
                        oReader["Titulo"].ToString(),
                        oReader["Descripcion"].ToString(),
                        Convert.ToInt32(oReader["CantidadDias"]),
                        Convert.ToDecimal(oReader["PrecioIndividual"]),
                        Convert.ToDecimal(oReader["PrecioBaseDoble"]),
                        Convert.ToDecimal(oReader["PrecioBaseTriple"]),
                        Convert.ToInt32(oReader["CantidadNochesAlojamiento"]),
                        oEstado,
                        oServicio,
                        oHospedaje
                    );
                }

                return oPaqueteViaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oReader != null)
                    oReader.Close();

                oConexion.Close();
            }
        }

        public static List<PaqueteViaje> ListarPaquetesDisponibles()
        {
            List<PaqueteViaje> lista = new List<PaqueteViaje>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarPaquetesDisponibles", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    PaqueteViaje oPaquete =
                        Buscar(Convert.ToInt32(oReader["IdPaquete"]));

                    if (oPaquete != null)
                        lista.Add(oPaquete);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oReader != null)
                    oReader.Close();

                oConexion.Close();
            }
        }

        public static List<PaqueteViaje> ListarPaquetesPorEstado(string pCodigoEstado)
        {
            List<PaqueteViaje> lista = new List<PaqueteViaje>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarPaquetesPorEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoEstado", pCodigoEstado);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    PaqueteViaje oPaquete =
                        Buscar(Convert.ToInt32(oReader["IdPaquete"]));

                    if (oPaquete != null)
                        lista.Add(oPaquete);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oReader != null)
                    oReader.Close();

                oConexion.Close();
            }
        }

        public static List<PaqueteViaje> ListarPaquetesPorServicio(
            int pNumeroOmnibus,
            DateTime pFechaHoraPartida)
        {
            List<PaqueteViaje> lista = new List<PaqueteViaje>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarPaquetesPorServicio", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@NumeroOmnibus", pNumeroOmnibus);
            oComando.Parameters.AddWithValue("@FechaHoraPartida", pFechaHoraPartida);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    PaqueteViaje oPaquete =
                        Buscar(Convert.ToInt32(oReader["IdPaquete"]));

                    if (oPaquete != null)
                        lista.Add(oPaquete);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oReader != null)
                    oReader.Close();

                oConexion.Close();
            }
        }
    }
}
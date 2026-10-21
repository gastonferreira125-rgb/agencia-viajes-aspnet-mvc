using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaServicio
    {
        public static void Agregar(Servicio pServicio)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("AltaServicio", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@NumeroOmnibus", pServicio.NumeroOmnibus);
            oComando.Parameters.AddWithValue("@FechaHoraPartida", pServicio.FechaHoraPartida);
            oComando.Parameters.AddWithValue("@FechaHoraLlegada", pServicio.FechaHoraLlegada);
            oComando.Parameters.AddWithValue("@PrecioPasajeIndividual", pServicio.PrecioPasajeIndividual);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El número de ómnibus debe estar entre 1 y 100.");

                if (retorno == -2)
                    throw new Exception("La fecha de llegada debe ser posterior a la fecha de partida.");

                if (retorno == -3)
                    throw new Exception("El Servicio ya existe.");

                if (retorno == -4)
                    throw new Exception("Error al agregar el Servicio.");
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

        public static void Modificar(Servicio pServicio)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ModificarServicio", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@NumeroOmnibus", pServicio.NumeroOmnibus);
            oComando.Parameters.AddWithValue("@FechaHoraPartida", pServicio.FechaHoraPartida);
            oComando.Parameters.AddWithValue("@FechaHoraLlegada", pServicio.FechaHoraLlegada);
            oComando.Parameters.AddWithValue("@PrecioPasajeIndividual", pServicio.PrecioPasajeIndividual);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El Servicio no existe.");

                if (retorno == -2)
                    throw new Exception("La fecha de llegada debe ser posterior a la fecha de partida.");

                if (retorno == -3)
                    throw new Exception("Error al modificar el Servicio.");
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

        public static void Eliminar(Servicio pServicio)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("EliminarServicio", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@NumeroOmnibus", pServicio.NumeroOmnibus);
            oComando.Parameters.AddWithValue("@FechaHoraPartida", pServicio.FechaHoraPartida);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El Servicio no existe.");

                if (retorno == -2)
                    throw new Exception("No se puede eliminar el Servicio porque tiene paquetes asociados.");

                if (retorno == -3)
                    throw new Exception("Error al eliminar el Servicio.");
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

        public static Servicio Buscar(int pNumeroOmnibus, DateTime pFechaHoraPartida)
        {
            Servicio oServicio = null;

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("BuscarServicio", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@NumeroOmnibus", pNumeroOmnibus);
            oComando.Parameters.AddWithValue("@FechaHoraPartida", pFechaHoraPartida);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    oServicio = new Servicio(Convert.ToInt32(oReader["NumeroOmnibus"]), Convert.ToDateTime(oReader["FechaHoraPartida"]),
                        Convert.ToDateTime(oReader["FechaHoraLlegada"]),Convert.ToDecimal(oReader["PrecioPasajeIndividual"]));
                }

                return oServicio;
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

        public static List<Servicio> ListarServicios()
        {
            List<Servicio> lista = new List<Servicio>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarServicios", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Servicio oServicio = new Servicio(Convert.ToInt32(oReader["NumeroOmnibus"]), Convert.ToDateTime(oReader["FechaHoraPartida"]),
                        Convert.ToDateTime(oReader["FechaHoraLlegada"]), Convert.ToDecimal(oReader["PrecioPasajeIndividual"]));

                    lista.Add(oServicio);
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

        public static List<Servicio> ListarServiciosFuturos()
        {
            List<Servicio> lista = new List<Servicio>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarServiciosFuturos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Servicio oServicio = new Servicio(Convert.ToInt32(oReader["NumeroOmnibus"]), Convert.ToDateTime(oReader["FechaHoraPartida"]),
                        Convert.ToDateTime(oReader["FechaHoraLlegada"]), Convert.ToDecimal(oReader["PrecioPasajeIndividual"]));

                    lista.Add(oServicio);
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
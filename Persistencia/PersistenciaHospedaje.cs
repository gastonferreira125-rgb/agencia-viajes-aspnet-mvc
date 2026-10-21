using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaHospedaje
    {
        public static void Agregar(Hospedaje pHospedaje)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("AltaHospedaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoInterno", pHospedaje.CodigoInterno);
            oComando.Parameters.AddWithValue("@Nombre", pHospedaje.Nombre);
            oComando.Parameters.AddWithValue("@Direccion", pHospedaje.Direccion);
            oComando.Parameters.AddWithValue("@TipoHospedaje", pHospedaje.TipoHospedaje);
            oComando.Parameters.AddWithValue("@PrecioPorNoche", pHospedaje.PrecioPorNoche);
            oComando.Parameters.AddWithValue("@CodigoEstado", pHospedaje.Estado.CodigoEstado);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El hospedaje ya existe.");

                if (retorno == -2)
                    throw new Exception("El Estado no existe.");

                if (retorno == -3)
                    throw new Exception("Tipo de hospedaje inválido.");

                if (retorno == -4)
                    throw new Exception("Error al agregar el Hospedaje.");
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

        public static void Modificar(Hospedaje pHospedaje)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ModificarHospedaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoInterno", pHospedaje.CodigoInterno);
            oComando.Parameters.AddWithValue("@Nombre", pHospedaje.Nombre);
            oComando.Parameters.AddWithValue("@Direccion", pHospedaje.Direccion);
            oComando.Parameters.AddWithValue("@TipoHospedaje", pHospedaje.TipoHospedaje);
            oComando.Parameters.AddWithValue("@PrecioPorNoche", pHospedaje.PrecioPorNoche);
            oComando.Parameters.AddWithValue("@CodigoEstado", pHospedaje.Estado.CodigoEstado);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El Hospedaje no existe.");

                if (retorno == -2)
                    throw new Exception("El Estado no existe.");

                if (retorno == -3)
                    throw new Exception("Tipo de hospedaje inválido.");

                if (retorno == -4)
                    throw new Exception("Error al modificar el Hospedaje.");
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

        public static void Eliminar(Hospedaje pHospedaje)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("EliminarHospedaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoInterno", pHospedaje.CodigoInterno);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El Hospedaje no existe.");

                if (retorno == -2)
                    throw new Exception("No se puede eliminar el Hospedaje porque tiene paquetes asociados.");

                if (retorno == -3)
                    throw new Exception("Error al eliminar el Hospedaje.");
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

        public static Hospedaje Buscar(string pCodigoInterno)
        {
            Hospedaje oHospedaje = null;

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("BuscarHospedaje", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoInterno", pCodigoInterno);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    Estado oEstado =
                        PersistenciaEstado.Buscar(oReader["CodigoEstado"].ToString());

                    oHospedaje = new Hospedaje(oReader["CodigoInterno"].ToString(), oReader["Nombre"].ToString(),
                        oReader["Direccion"].ToString(), oReader["TipoHospedaje"].ToString(), Convert.ToDecimal(oReader["PrecioPorNoche"]), oEstado);
                }

                return oHospedaje;
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

        public static List<Hospedaje> ListarHospedajes()
        {
            List<Hospedaje> lista = new List<Hospedaje>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarHospedajes", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Estado oEstado = PersistenciaEstado.Buscar(oReader["CodigoEstado"].ToString());

                    Hospedaje oHospedaje = new Hospedaje(oReader["CodigoInterno"].ToString(), oReader["Nombre"].ToString(),
                        oReader["Direccion"].ToString(), oReader["TipoHospedaje"].ToString(), Convert.ToDecimal(oReader["PrecioPorNoche"]), oEstado);

                    lista.Add(oHospedaje);
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

        public static List<Hospedaje> ListarHospedajesPorEstado(string pCodigoEstado)
        {
            List<Hospedaje> lista = new List<Hospedaje>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarHospedajesPorEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoEstado", pCodigoEstado);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Estado oEstado = PersistenciaEstado.Buscar(oReader["CodigoEstado"].ToString());

                    Hospedaje oHospedaje = new Hospedaje(oReader["CodigoInterno"].ToString(), oReader["Nombre"].ToString(),
                        oReader["Direccion"].ToString(), oReader["TipoHospedaje"].ToString(), Convert.ToDecimal(oReader["PrecioPorNoche"]), oEstado);

                    lista.Add(oHospedaje);
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
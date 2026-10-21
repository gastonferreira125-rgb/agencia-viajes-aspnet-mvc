using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaEstado
    {
        public static void Agregar(Estado pEstado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("AltaEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoEstado", pEstado.CodigoEstado);
            oComando.Parameters.AddWithValue("@NombreEstado", pEstado.Nombre);
            oComando.Parameters.AddWithValue("@Pais", pEstado.Pais);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(oRetorno.Value);

                if (retorno == -1)
                    throw new Exception("El Estado ya existe.");

                if (retorno == -2)
                    throw new Exception("Error al agregar el Estado.");
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

        public static void Modificar(Estado pEstado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ModificarEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoEstado", pEstado.CodigoEstado);
            oComando.Parameters.AddWithValue("@NombreEstado", pEstado.Nombre);
            oComando.Parameters.AddWithValue("@Pais", pEstado.Pais);

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
                    throw new Exception("Error al modificar el Estado.");
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

        public static void Eliminar(Estado pEstado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("EliminarEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoEstado", pEstado.CodigoEstado);

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
                    throw new Exception("No se puede eliminar el Estado porque tiene hospedajes asociados.");

                if (retorno == -3)
                    throw new Exception("No se puede eliminar el Estado porque tiene paquetes de viaje asociados.");

                if (retorno == -4)
                    throw new Exception("Error al eliminar el Estado.");
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

        public static Estado Buscar(string pCodigoEstado)
        {
            Estado oEstado = null;

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("BuscarEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@CodigoEstado", pCodigoEstado);

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    oEstado = new Estado(oReader["CodigoEstado"].ToString(), oReader["NombreEstado"].ToString(),oReader["Pais"].ToString());
                }

                return oEstado;
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

        public static List<Estado> ListarEstados()
        {
            List<Estado> lista = new List<Estado>();

            SqlConnection oConexion = new SqlConnection(Conexion.cnn);
            SqlCommand oComando = new SqlCommand("ListarEstados", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlDataReader oReader = null;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    Estado oEstado = new Estado(oReader["CodigoEstado"].ToString(), oReader["NombreEstado"].ToString(), oReader["Pais"].ToString());

                    lista.Add(oEstado);
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
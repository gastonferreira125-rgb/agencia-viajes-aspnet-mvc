using Persistencia;
using System.Configuration;

namespace Persistencia
{
    internal class Conexion
    {
        internal static string cnn =
            ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Estado
    {
        
        private string _codigoEstado;
        private string _nombre;
        private string _pais;

        public string CodigoEstado
        {
            get { return _codigoEstado; }
            set { _codigoEstado = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        public Estado() { }//por defecto

        public Estado(string pCodigoEstado, string pNombre, string pPais)//al completo le agrego la validacion que no la tenia
        {
            CodigoEstado = pCodigoEstado;
            Nombre = pNombre;
            Pais = pPais;


            //Validar();//asi si intentan crear un estado con datos invalidas la excepcion salta ahi mismo
            //en persiistenciaestado.buscar y listarestado como usan este contructor  para armar los objs que vienen de la bd validar se va a ejecutar tamb ahi 
            //siempre que se use new estado no solo en las altas nose si me explique?
        }

        
        public void Validar()
        {
            if (this.CodigoEstado == null || this.CodigoEstado.Trim().Length != 5)
                throw new Exception("Código Estado: debe tener exactamente 4 caracteres");

            if (this.Nombre == null || this.Nombre.Trim().Length < 1 || this.Nombre.Trim().Length > 50)
                throw new Exception("Nombre: largo entre 1 y 50 caracteres");

            if (this.Pais == null || this.Pais.Trim().Length < 1 || this.Pais.Trim().Length > 50)
                throw new Exception("País: largo entre 1 y 50 caracteres");
        }
    }
}
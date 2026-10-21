using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Hospedaje
    {
        //Atributos
        private string _codigoInterno;
        private string _nombre;
        private string _direccion;
        private string _tipoHospedaje;
        private decimal _precioPorNoche;

        // atributo de asociación
        private Estado _estado;

        //Propiedades
        public string CodigoInterno
        {
            get { return _codigoInterno; }
            set { _codigoInterno = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string TipoHospedaje
        {
            get { return _tipoHospedaje; }
            set { _tipoHospedaje = value; }
        }

        public decimal PrecioPorNoche
        {
            get { return _precioPorNoche; }
            set { _precioPorNoche = value; }
        }

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public Hospedaje() { }

        public Hospedaje(string pCodigoInterno, string pNombre,
                         string pDireccion, string pTipoHospedaje,
                         decimal pPrecioPorNoche, Estado pEstado)
        {
            CodigoInterno = pCodigoInterno;
            Nombre = pNombre;
            Direccion = pDireccion;
            TipoHospedaje = pTipoHospedaje;
            PrecioPorNoche = pPrecioPorNoche;
            Estado = pEstado;

            //Validar();
        }


        public void Validar()
        {
            if (this.CodigoInterno == null ||
                this.CodigoInterno.Trim().Length < 1 ||
                this.CodigoInterno.Trim().Length > 10)
                throw new Exception("Código interno: largo entre 1 y 10 caracteres");

            if (this.Nombre == null ||
                this.Nombre.Trim().Length < 1 ||
                this.Nombre.Trim().Length > 100)
                throw new Exception("Nombre: largo entre 1 y 100 caracteres");

            if (this.Direccion == null ||
                this.Direccion.Trim().Length < 1 ||
                this.Direccion.Trim().Length > 150)
                throw new Exception("Dirección: largo entre 1 y 150 caracteres");

            if (this.TipoHospedaje != "Hotel STD" &&
                this.TipoHospedaje != "Posada" &&
                this.TipoHospedaje != "All Inclusive")
                throw new Exception("Tipo de hospedaje inválido");

            if (this.PrecioPorNoche <= 0)
                throw new Exception("El precio por noche debe ser mayor a cero");

            if (this.Estado == null)
                throw new Exception("Se necesita un Estado");
        }

        public decimal CalcularCosto(int pCantidadNoches)
        {
            return this.PrecioPorNoche * pCantidadNoches;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class PaqueteViaje
    {
        private int _codigo;
        private string _titulo;
        private string _descripcion;
        private int _cantidadDias;
        private decimal _precioIndividual;
        private decimal _precioDoble;
        private decimal _precioTriple;
        private int _cantidadNoches;
        private Estado _estado;
        private Servicio _servicio;
        private Hospedaje _hospedaje;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int CantidadDias
        {
            get { return _cantidadDias; }
            set { _cantidadDias = value; }
        }

        public decimal PrecioIndividual
        {
            get { return _precioIndividual; }
            set { _precioIndividual = value; }
        }

        public decimal PrecioDoble
        {
            get { return _precioDoble; }
            set { _precioDoble = value; }
        }

        public decimal PrecioTriple
        {
            get { return _precioTriple; }
            set { _precioTriple = value; }
        }

        public int CantidadNoches
        {
            get { return _cantidadNoches; }
            set { _cantidadNoches = value; }
        }

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public Servicio Servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }

        public Hospedaje Hospedaje
        {
            get { return _hospedaje; }
            set { _hospedaje = value; }
        }


        public PaqueteViaje()
        {
        }


        // Constructor para crear un paquete nuevo
        public PaqueteViaje(
            string pTitulo,
            string pDescripcion,
            int pCantidadNoches,
            Estado pEstado,
            Servicio pServicio,
            Hospedaje pHospedaje)
        {
            Titulo = pTitulo;
            Descripcion = pDescripcion;
            CantidadNoches = pCantidadNoches;
            Estado = pEstado;
            Servicio = pServicio;
            Hospedaje = pHospedaje;

            CantidadDias = Servicio.CalcularDuracion();

            CalcularPrecios();
        }


        // Constructor para reconstruir un paquete que ya existe en la BD
        public PaqueteViaje(
            int pCodigo,
            string pTitulo,
            string pDescripcion,
            int pCantidadDias,
            decimal pPrecioIndividual,
            decimal pPrecioDoble,
            decimal pPrecioTriple,
            int pCantidadNoches,
            Estado pEstado,
            Servicio pServicio,
            Hospedaje pHospedaje)
        {
            Codigo = pCodigo;
            Titulo = pTitulo;
            Descripcion = pDescripcion;
            CantidadDias = pCantidadDias;
            PrecioIndividual = pPrecioIndividual;
            PrecioDoble = pPrecioDoble;
            PrecioTriple = pPrecioTriple;
            CantidadNoches = pCantidadNoches;
            Estado = pEstado;
            Servicio = pServicio;
            Hospedaje = pHospedaje;
        }


        private void CalcularPrecios()
        {
            decimal precioBase =
                Servicio.PrecioPasajeIndividual +
                Hospedaje.CalcularCosto(CantidadNoches);

            PrecioIndividual = precioBase * 1.35m;

            PrecioDoble = precioBase * 2 * 1.10m;

            PrecioTriple = precioBase * 3 * 1.10m;
        }


        public void Validar()
        {
            if (string.IsNullOrEmpty(Titulo) || Titulo.Length > 100)
                throw new Exception("El título es obligatorio y admite hasta 100 caracteres.");

            if (string.IsNullOrEmpty(Descripcion) || Descripcion.Length > 300)
                throw new Exception("La descripción es obligatoria y admite hasta 300 caracteres.");

            if (Estado == null)
                throw new Exception("Debe seleccionar un Estado.");

            if (Servicio == null)
                throw new Exception("Debe seleccionar un Servicio.");

            if (Hospedaje == null)
                throw new Exception("Debe seleccionar un Hospedaje.");

            if (CantidadNoches <= 0)
                throw new Exception("La cantidad de noches debe ser mayor a cero.");

            if (Hospedaje.Estado == null ||
                Hospedaje.Estado.CodigoEstado != Estado.CodigoEstado)
            {
                throw new Exception(
                    "El Hospedaje debe pertenecer al mismo Estado de destino."
                );
            }

            if (CantidadNoches >= Servicio.CalcularDuracion())
            {
                throw new Exception(
                    "La cantidad de noches debe ser menor a la duración del viaje."
                );
            }
        }
    }
}
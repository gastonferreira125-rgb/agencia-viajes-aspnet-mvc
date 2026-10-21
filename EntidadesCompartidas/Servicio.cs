using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Servicio
    {
 
        private int _numeroOmnibus;
        private DateTime _fechaHoraPartida;
        private DateTime _fechaHoraLlegada;
        private decimal _precioPasajeIndividual;


        public int NumeroOmnibus
        {
            get { return _numeroOmnibus; }
            set { _numeroOmnibus = value; }
        }

        public DateTime FechaHoraPartida
        {
            get { return _fechaHoraPartida; }
            set { _fechaHoraPartida = value; }
        }

        public DateTime FechaHoraLlegada
        {
            get { return _fechaHoraLlegada; }
            set { _fechaHoraLlegada = value; }
        }

        public decimal PrecioPasajeIndividual
        {
            get { return _precioPasajeIndividual; }
            set { _precioPasajeIndividual = value; }
        }

   
        public Servicio() { }

        public Servicio(int pNumeroOmnibus, DateTime pFechaHoraPartida,
                        DateTime pFechaHoraLlegada, decimal pPrecioPasajeIndividual)
        {
            NumeroOmnibus = pNumeroOmnibus;
            FechaHoraPartida = pFechaHoraPartida;
            FechaHoraLlegada = pFechaHoraLlegada;
            PrecioPasajeIndividual = pPrecioPasajeIndividual;

            //Validar();
        }

      
        public void Validar()
        {
            if (this.NumeroOmnibus < 1 || this.NumeroOmnibus > 100)
                throw new Exception("Número de ómnibus: debe estar entre 1 y 100");

            if (this.FechaHoraLlegada <= this.FechaHoraPartida)
                throw new Exception("La fecha y hora de llegada debe ser posterior a la fecha y hora de partida");

            if (this.PrecioPasajeIndividual <= 0)
                throw new Exception("El precio del pasaje individual debe ser mayor a cero");
        }

        public int CalcularDuracion()
        {
            return (this.FechaHoraLlegada - this.FechaHoraPartida).Days;
        }
    }
}
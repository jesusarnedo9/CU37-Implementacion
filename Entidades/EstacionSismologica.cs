using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;

namespace ImplementacionCU37.Entidades
{
    public class EstacionSismologica
    {
        public int idEstacionSismologica { get; set; }
        public string codigoEstacion { get; set; }
        public string documentoCertificacionAdq { get; set; }
        public DateTime fechaCertificacion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string nombre { get; set; }
        public string nroCertificacionAdq { get; set; }
        public Sismografo sismografo { get; private set; }
        public int idSismografoFK
        {
            get => sismografo?.idSismografo ?? 0;
            set
            {
                if (sismografo == null)
                    sismografo = new Sismografo();

                sismografo.idSismografo = value;
            }
        }

        public EstacionSismologica() { }

        public EstacionSismologica(Sismografo sismografo)
        {
            this.sismografo = sismografo;
        }

        public string getIDSismografo()
        {
            return sismografo.identificadorSismografo;
        }
        public Sismografo getSismografo()
        {
            return this.sismografo;
        }
        public void actualizarEstadoSismografo(Estado nuevoEstado, List<MotivoFueraServicioDTO> motivos, Empleado responsableLogueado)
        {
            if (sismografo != null)
            {
                sismografo.setEstadoActual(nuevoEstado, motivos, responsableLogueado);
            }
        }
    }
}
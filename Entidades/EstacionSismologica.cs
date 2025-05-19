using System;

namespace ImplementacionCU37.Entidades
{
    public class EstacionSismologica
    {
        public string codigoEstacion { get; set; }
        public string documentoCertificacionAdq { get; set; }
        public string fechaSituacionCertificacion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string nombre { get; set; }
        public string nroCertificacionAdquisicion { get; set; }

        // Relaci�n 1 a 1 con Sismografo
        private Sismografo sismografo;

        // Constructor opcional
        public EstacionSismologica(Sismografo sismografo)
        {
            this.sismografo = sismografo;
        }

        public string getIDSismografo()
        {
            return sismografo != null ? sismografo.getID() : "";
        }

    }
}

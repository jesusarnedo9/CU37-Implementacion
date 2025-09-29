using System.Collections.Generic;

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

        private Sismografo sismografo;

        public EstacionSismologica(Sismografo sismografo)
        {
            this.sismografo = sismografo;
        }
        public string getIDSismografo()
        {
            return sismografo.getID();
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

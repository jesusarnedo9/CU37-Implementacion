using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        public void actualizarSismografo(Estado nuevoEstado, List<MotivoFueraServicio> motivos, Empleado responsableLogueado)
        {
            // Lógica para actualizar el sismógrafo asociado a la estación
            if (sismografo != null)
            {
                sismografo.actualizarEstado(nuevoEstado, motivos, responsableLogueado);
            }
        }
    }
}

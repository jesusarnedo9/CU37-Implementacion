using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImplementacionCU37.Entidades 
{
    public class Sismografo
    {
        public int idSismografo { get; set; }
        public DateTime fechaAdquisicion { get; set; }
        public string identificadorSismografo { get; set; }
        public string numeroSerie { get; set; }
        public Estado estadoActual { get; set; }
        public List<CambioEstado> historialEstados { get; set; } = new List<CambioEstado>();
        public CambioEstado cambioEstadoActual { get; private set; }
        public int idEstadoActualFK => estadoActual?.idEstado ?? 0;
        public Sismografo() { }
        public string getID() => identificadorSismografo;

        public void setEstadoActual(Estado nuevoEstado, List<MotivoFueraServicioDTO> motivos, Empleado responsableLogueado)
        {
            if (nuevoEstado == null) throw new ArgumentNullException(nameof(nuevoEstado), "El nuevo estado no puede ser nulo.");
            if (motivos == null || !motivos.Any()) throw new ArgumentException("La lista de motivos no puede estar vacía.", nameof(motivos));
            if (responsableLogueado == null) throw new ArgumentNullException(nameof(responsableLogueado), "El responsable logueado no puede ser nulo.");

            var cambioEstadoActual = obtenerCambioEstadoActual();
            if (cambioEstadoActual != null)
            {
                cambioEstadoActual.finalizar();
            }
            var nuevoCambio = CambioEstado.crear(motivos, responsableLogueado);

            historialEstados.Add(nuevoCambio);
            estadoActual = nuevoEstado;
        }

        public CambioEstado obtenerCambioEstadoActual()
        {
            return historialEstados.FirstOrDefault(ce => ce.esActual());
        }
    }
}

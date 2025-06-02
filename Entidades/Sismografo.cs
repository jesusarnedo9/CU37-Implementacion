using System;
using System.Collections.Generic;
using System.Linq;

namespace ImplementacionCU37.Entidades
{
    public class Sismografo
    {
        //private Sismografo sismografo;

        //Atributos
        public DateTime fechaAdquisicion { get; set; }
        public string identificadorSismografo { get; set; }
        public string nroSerie { get; set; }
        public Estado estadoActual { get; set; } // relación 1 a 1
        public List<CambioEstado> historialEstados { get; set; } = new List<CambioEstado>(); // 1 a muchos

        //Metodos
        public string getID() => identificadorSismografo;

        public void actualizarEstado(Estado nuevoEstado, List<MotivoFueraServicio> motivos, Empleado responsableLogueado)
        {
            if (nuevoEstado == null) throw new ArgumentNullException(nameof(nuevoEstado), "El nuevo estado no puede ser nulo.");
            if (motivos == null || !motivos.Any()) throw new ArgumentException("La lista de motivos no puede estar vacía.", nameof(motivos));
            if (responsableLogueado == null) throw new ArgumentNullException(nameof(responsableLogueado), "El responsable logueado no puede ser nulo.");
            // Cerrar el estado actual si existe
            cerrarEstadoActual();
            // Crear un nuevo cambio de estado
            var nuevoCambio = new CambioEstado(DateTime.Now, motivos);
            nuevoCambio.setRILogueado(responsableLogueado);
            nuevoCambio.setMotivosSeleccionado(motivos.FirstOrDefault());
            // Agregar el cambio al historial
            agregarCambioEstado(nuevoCambio, nuevoEstado);
            setEstadoActual();
        }

        public void setEstadoActual()
        {
            if (historialEstados != null && historialEstados.Any())
            {
                // Asume que el estado actual es el último por fecha de inicio
                var ultimoCambio = historialEstados
                    .OrderByDescending(ce => ce.fechaHoraInicio)
                    .FirstOrDefault();

                estadoActual = ultimoCambio != null && ultimoCambio.esActual() ? estadoActual : null;
            }
        }

        public CambioEstado getCambioEstadoActual()
        {
            return historialEstados.FirstOrDefault(ce => ce.esActual());
        }

        public void agregarCambioEstado(CambioEstado nuevoCambio, Estado nuevoEstado)
        {
            historialEstados.Add(nuevoCambio);
            estadoActual = nuevoEstado;
        }

        public void cerrarEstadoActual()
        {
            var actual = getCambioEstadoActual();
            if (actual != null)
            {
                actual.finalizar();
            }
        }


    }
}

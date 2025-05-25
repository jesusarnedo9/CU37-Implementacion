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

        /*public Estado getEstadoActual()
        {
            setEstadoActual();
            return estadoActual;
        }*/

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

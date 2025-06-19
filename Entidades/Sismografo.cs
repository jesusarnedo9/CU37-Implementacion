using System;
using System.Collections.Generic;
using System.Linq;

namespace ImplementacionCU37.Entidades
{
    public class Sismografo
    {
        public DateTime fechaAdquisicion {get; set;}
        public string identificadorSismografo {get; set;}
        public string nroSerie {get; set;}
        public CambioEstado estado {get; set;}

        public Estado estadoActual;
        public List<CambioEstado> historialEstados {get; set;} = new List<CambioEstado>();

        //Metodos
        public string getID() => identificadorSismografo;
        public void setEstadoActual(Estado nuevoEstado, List<MotivoFueraServicio> motivos, Empleado responsableLogueado)
        {
            if (nuevoEstado == null) throw new ArgumentNullException(nameof(nuevoEstado), "El nuevo estado no puede ser nulo.");
            if (motivos == null || !motivos.Any()) throw new ArgumentException("La lista de motivos no puede estar vacía.", nameof(motivos));
            if (responsableLogueado == null) throw new ArgumentNullException(nameof(responsableLogueado), "El responsable logueado no puede ser nulo.");
            
            var estado = esActual();
            if (estado != null)
            {
                estado.finalizar();
            };
            // Crear un nuevo cambio de estado
            var nuevoCambio = new CambioEstado(DateTime.Now, motivos)
            {
                estado = nuevoEstado
            };
             
            nuevoCambio.setRILogueado(responsableLogueado);
            nuevoCambio.setMotivosSeleccionado(motivos.FirstOrDefault());

            historialEstados.Add(nuevoCambio);

            estadoActual = nuevoEstado;
        }
        public CambioEstado esActual()
        {
            return historialEstados.FirstOrDefault(ce => ce.esActual());
        }

    }
}

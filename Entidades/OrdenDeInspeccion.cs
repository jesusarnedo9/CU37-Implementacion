using System;
using System.Collections.Generic;
using System.Linq;
using ImplementacionCU37.Estados;

namespace ImplementacionCU37.Entidades
{
    public class OrdenDeInspeccion
    {
        public int idOrden { get; set; }
        public int nroOrden { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime? fechaHoraFin { get; set; }
        public DateTime? fechaHoraCierre { get; set; }
        public DateTime? fechaHoraFinalizacion { get => fechaHoraFin; set => fechaHoraFin = value; }
        public string observacionCierre { get; set; }
        public Empleado empleadoAsignado { get; set; }
        public EstacionSismologica estacion { get; set; }
        public IEstado estado { get; private set; }
        public int idEmpleadoAsignadoFK => empleadoAsignado?.idEmpleado ?? 0;
        public int idEstacionFK => estacion?.idEstacionSismologica ?? 0;
        public int idEstadoFK { get; set; }

        public OrdenDeInspeccion() { }

        public OrdenDeInspeccion(int numeroOrden, DateTime fechaHoraInicio, EstacionSismologica estacion, IEstado estado, Empleado empleadoAsignado)
        {
            this.nroOrden = numeroOrden;
            this.fechaHoraInicio = fechaHoraInicio;
            this.estacion = estacion;
            this.estado = estado;
            this.empleadoAsignado = empleadoAsignado;
            this.ActualizarIdEstadoFK(estado);
        }

        public bool esDeEmpleado(Empleado empleado)
        {
            return empleadoAsignado != null && empleado != null && empleadoAsignado.idEmpleado == empleado.idEmpleado;
        }
        public bool estaRealizada()
        {
            return estado != null && estado.estaRealizada();
        }
        public EstacionSismologica getEstacionSismologica()
        {
            return estacion;
        }
        public void cerrarOrden(DateTime fechaHoraCierre)
        {
            if (estado == null)
                throw new InvalidOperationException("La orden no tiene estado asignado.");

            IEstado nuevoEstado = estado.CerrarOrden(this, fechaHoraCierre);
            if (nuevoEstado != null)
            {
                setEstado(nuevoEstado);
            }
        }

        public void setFechaHoraCierre(DateTime fechaHoraCierre)
        {
            this.fechaHoraCierre = fechaHoraCierre;
        }

        public void setEstado(IEstado nuevoEstado)
        {
            this.estado = nuevoEstado;
        }

        private void ActualizarIdEstadoFK(IEstado estado)
        {
            if (estado != null)
                this.idEstadoFK = estado.idEstado;
            else
                this.idEstadoFK = 0;
        }

        public override string ToString()
        {
            var fin = fechaHoraFin?.ToShortDateString() ?? "N/A";
            var sismId = estacion?.getSismografo()?.getID() ?? "N/A";
            var estName = estacion?.nombre ?? "N/A";
            return $"Orden #{nroOrden} - Estación: {estName} - Sismógrafo: {sismId} - Finalización: {fin}";
        }

        public void actualizarEstadoSismografo(Estado nuevoEstado, List<MotivoFueraServicioDTO> motivos, Empleado responsableLogueado)
        {
            estacion.actualizarEstadoSismografo(nuevoEstado, motivos, responsableLogueado);
        }
    }
}
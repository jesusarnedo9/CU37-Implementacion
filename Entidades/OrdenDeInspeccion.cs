using System;

namespace ImplementacionCU37.Entidades
{
    public class OrdenDeInspeccion
    {
        // Atributos
        public int numeroOrden { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime fechaHoraFinalizacion { get; set; }
        public DateTime fechaHoraCierre { get; set; }
        public string observacionCierre { get; set; }

        public Empleado empleadoAsignado { get; set; }
        public EstacionSismologica estacion { get; set; }
        public Estado estado { get; set; }

        //Constructor
        public OrdenDeInspeccion(int numeroOrden, DateTime fechaHoraInicio, EstacionSismologica estacion, Estado estado, Empleado empleadoAsignado)
        {
            this.numeroOrden = numeroOrden;
            this.fechaHoraInicio = fechaHoraInicio;
            this.estacion = estacion;
            this.estado = estado;
            this.empleadoAsignado = empleadoAsignado;
        }

        //Metodos
        public bool estaRealizada()
        {
            return estado != null && estado.nombreEstado.Equals("Realizada");
        }

        public bool esDeEmpleado(Empleado empleado)
        {
                return empleadoAsignado != null && empleadoAsignado.Equals(empleado);
        }
      
        public string getCodigoEstacionSismologica()
        {
            return estacion != null ? estacion.getIDSismografo() : "";
        }
        public EstacionSismologica getEstacionSismologica()
        {
            return estacion;
        }

        public string obtenerInfoOI()
        {
            return $"Orden #{numeroOrden} | Finalizacion: {fechaHoraFinalizacion} | Estaci�n: {estacion.nombre} | ID Sismografo : {estacion.getIDSismografo()} ";
        }

        public void setFechaHoraCierre(DateTime fechaHoraCierre)
        {
            this.fechaHoraCierre = fechaHoraCierre;
        }


        public void setEstado(Estado nuevoEstado)
        {
            this.estado = nuevoEstado;
        }

       public override string ToString()
        {
            return $"Orden #{numeroOrden} | Finalizacion: {fechaHoraFinalizacion} | Estaci�n: {estacion.nombre} | ID Sismografo : {estacion.getIDSismografo()} ";
        }
    }
}

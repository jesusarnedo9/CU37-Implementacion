using System;
using System.Collections.Generic;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Controlador
{
    public class GestorOrdenInspeccion
    {
        // Atributos propios
        private string comentario;
        private bool confirmacionCierre;
        private DateTime fechaActual;
        private TimeSpan horaActual;
        private string observacionCierre;
        private OrdenDeInspeccion ordenSeleccionada;
        private List<string> solicitudMotivo;
        private Empleado responsableLogueado;
        private List<OrdenDeInspeccion> ordenes;

        // Dependencias
        //private InterfazEmail interfazEmail;
        //private PantallaCCRS pantallaCCRS;
        private PantallaCierreOrden pantalla;
        private List<MotivoTipo> motivos;
        private Estado estado;
        private Sesion sesion;

        //Datos de prueba
        private Empleado empleado;
        private Usuario usuario;

        // Constructor para inicializar los datos de prueba
        public GestorOrdenInspeccion()
        {
            empleado = new Empleado("Jesus", "Arnedo", "jesus@mail.com", "12345", 5, Rol.RESPONSABLE_REPARACION);
            usuario = new Usuario("jesus", empleado);
            sesion = new Sesion(usuario);

            ordenes = new List<OrdenDeInspeccion>();

            
        }

        // Métodos
        public void opcionCerrarOrdenInspeccion()
        {
        }
        public void buscarUsuario()
        {
            Usuario usuario = sesion.getUsuario();
            responsableLogueado = usuario.getEmpleado();
        }

        public void buscarEstadoFueraServicio() { }
        public void buscarMotivo() { }

        public void finCU() { }
        public DateTime getFechaActual() => DateTime.Now.Date;
        public TimeSpan getHoraActual() => DateTime.Now.TimeOfDay;
        public void notificarCierre() { }
        public void ordenar() { }
        public void publicarEnPantallaCCRS() { }
        public void registrarCierre() { }
        public void tomarComentario(string comentario) => this.comentario = comentario;
        public void tomarConfirmacionCierre(bool confirmacion) => this.confirmacionCierre = confirmacion;
        public void tomarObservacionCierre(string observacion) => this.observacionCierre = observacion;
        public void tomarOrdenSeleccionada(OrdenDeInspeccion orden) => this.ordenSeleccionada = orden;
        public void tomarSolicitudMotivo(List<string> motivos) => this.solicitudMotivo = motivos;
        public void validarDatosIngresados() { }
    }
}

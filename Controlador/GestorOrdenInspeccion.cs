using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace ImplementacionCU37.Controlador
{
    public class GestorOrdenInspeccion
    {

        // Atributos
        private Sistema sistema;
        private PantallaCierreOrden pantalla;
        private Empleado empleado;
        private List<OrdenDeInspeccion> ordenes;
        private DateTime fechaHoraActual;
        private bool confirmacionCierre;
        private string observacion;
        private OrdenDeInspeccion ordenSeleccionada;
        private Estado estadoCerrada;
        private Estado estadoFueraServicio;
        private List<MotivoFueraServicio> motivosSeleccionados = new List<MotivoFueraServicio>();
        private MotivoTipo motivoActual;

        public GestorOrdenInspeccion(Sistema sistema, PantallaCierreOrden pantalla)
        {
            this.sistema = sistema;
            this.pantalla = pantalla;
        }
        // Main
        public void opcionCerrarOrdenInspeccion()
        {
            buscarEmpleado();
            buscarOrdenesInspecciones();
        }
        // Metodos
        public void buscarEmpleado()
        {
            empleado = sistema.SesionActiva.getEmpleado();
        }
        public void buscarOrdenesInspecciones()
        {
            ordenes = sistema.Ordenes;
            List<OrdenDeInspeccion> ordenesRealizadas = new List<OrdenDeInspeccion>();
            foreach (OrdenDeInspeccion orden in ordenes)
            {
                if (orden.esDeEmpleado(empleado) && orden.estaRealizada())
                {
                    ordenesRealizadas.Add(orden);
                }
            }
            ordenesRealizadas = ordenarOI(ordenesRealizadas);
            pantalla.solicitarSeleccionOrden(ordenesRealizadas);
        }
        public void tomarOrdenSeleccionada(string numeroOrden)
        {
            foreach (OrdenDeInspeccion orden in ordenes)
            {
                if (orden.numeroOrden.ToString() == numeroOrden)
                {
                    ordenSeleccionada = orden;
                    break;
                }
            }
            pantalla.solicitarObservacionCierre();
        }
        public void tomarObservacionCierre(string observacion)
        {
            this.observacion = observacion;
        }
        public void tomarConfirmacionCierre(bool confirmacion)
        {
            this.confirmacionCierre = confirmacion;
            validarDatosIngresados();
        }
        public void validarDatosIngresados()
        {
            if (string.IsNullOrWhiteSpace(observacion))
            {
                MessageBox.Show("Debe ingresar una observación de cierre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (motivosSeleccionados == null || motivosSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un motivo de cierre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            fechaHoraActual = getFechaHoraActual();
            registrarCierreOI();
        }
        public void tomarMotivoYComentario(MotivoTipo motivo, string comentario)
        {
            motivoActual = motivo;
            var motivoFS = new MotivoFueraServicio(motivoActual, comentario);

            motivosSeleccionados.Add(motivoFS);
            pantalla.solicitarConfirmacionCierre();
        }

        public void buscarEstadoCerrada()
        {
            List<Estado> estados = sistema.EstadosDisponibles;
            foreach (Estado estado in estados)
            {
                if (estado.esAmbitoOI() && estado.esCerrada())
                {
                    estadoCerrada = estado;
                    break;
                }
            }
        }
        public void registrarCierreOI()
        {
            buscarEstadoCerrada();
            cerrarOI();//Agregar self en Diagramas

            // Actualizar estado del sismógrafo
            actualizarEstadoSismografo();
            pantalla.mostrarMensaje("Estado actualizado");
            pantalla.mostrarMensaje("Orden cerrada y estado del sismógrafo actualizado.");
            notificarCierre();
            finCU();
        }

        public void cerrarOI() 
        { 
            ordenSeleccionada.setFechaHoraCierre(fechaHoraActual);
            ordenSeleccionada.setEstado(estadoCerrada);
        }
        public DateTime getFechaHoraActual() => DateTime.Now;
        public Estado buscarEstadoFueraServicio()
        {
            foreach (Estado estado in sistema.EstadosDisponibles)
            {
                if (estado.esAmbitoSismografo() && estado.esFueraServicio())
                {
                    return estado;
                }
            }
            return null;
        }
        public List<MotivoTipo> buscarMotivo()//Probar de poner el metodo get descripciones
        {
            return sistema.MotivoTipos;
        }
        public List<OrdenDeInspeccion> ordenarOI(List<OrdenDeInspeccion> lista)
        {
            return lista
                .OrderByDescending(o => o.fechaHoraFinalizacion)
                .ToList();
        }
        public void actualizarEstadoSismografo()
        {
            estadoFueraServicio = buscarEstadoFueraServicio();
            ordenSeleccionada.actualizarEstadoSismografo(estadoFueraServicio, motivosSeleccionados, empleado);
            var estacion = ordenSeleccionada.getEstacionSismologica();
            var sismografo = estacion.getIDSismografo();
            pantalla.mostrarActualizacionEstado(estacion, sismografo ,motivosSeleccionados, empleado, fechaHoraActual);
        }   
        public void notificarCierre() 
        {
            pantalla.mostrarMensaje("Mails enviados");
        }
        public void finCU()
        {
            // Cierra la pantalla de cierre de orden si está abierta
            if (pantalla != null && !pantalla.IsDisposed)
            {
                pantalla.Close();
                pantalla = null;
            }

            // Limpieza de otras referencias
            sistema = null;
            empleado = null;
            ordenes = null;
            ordenSeleccionada = null;
            estadoCerrada = null;
            estadoFueraServicio = null;
            motivosSeleccionados.Clear();
            motivoActual = null;
        }
    }
}

using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
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
        private bool confirmacionCierre;
        private DateTime fechaHoraActual;
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
            pantalla.habilitarPantalla();
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
        }
        public void tomarObservacionCierre(string observacion)
        {
            this.observacion = observacion;
        }
        public void tomarConfirmacionCierre(bool confirmacion)
        {
            this.confirmacionCierre = confirmacion;
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

            // Todo OK
            registrarCierre();
        }


        public void tomarSeleccionMotivo(MotivoTipo motivo)
        {
            motivoActual = motivo;
        }

        public void tomarComentario(string comentario)
        {
            var motivoFS = new MotivoFueraServicio(motivoActual, comentario);
            motivosSeleccionados.Add(motivoFS);
        }


        public void buscarEstadoCerrada()
        {
            List<Estado> estados = sistema.EstadosDisponibles;
            foreach (Estado estado in estados)
            {
                if (estado.esCerrada())
                {
                    estadoCerrada = estado;
                    break;
                }
            }
        }
        public void registrarCierre()
        {
            if (ordenSeleccionada == null)
            {
                pantalla.mostrarMensaje("Debe seleccionar una orden antes de cerrarla.");
                return;
            }
            buscarEstadoCerrada();
            // Setear fecha, hora y estado
            ordenSeleccionada.setFechaHoraCierre(DateTime.Now);
            ordenSeleccionada.setEstado(estadoCerrada);

            // Actualizar estado del sismógrafo

            string mensaje = actualizarEstadoSismografo();
            MessageBox.Show(mensaje, "Estado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            pantalla.mostrarMensaje("Orden cerrada y estado del sismógrafo actualizado.");
            notificarCierre();
            finCU();
        }
        public DateTime getFechaHoraActual() => DateTime.Now;
        public void buscarEstadoFueraServicio()
        {
            foreach (Estado estado in sistema.EstadosDisponibles)
            {
                if (estado.esAmbitoSismografo() && estado.esFueraServicio())
                {
                    estadoFueraServicio = estado;
                    break;
                }
            }
        }

        public List<MotivoTipo> buscarMotivo()
        {
            return sistema.MotivoTipos;
        }
        public List<OrdenDeInspeccion> ordenarOI(List<OrdenDeInspeccion> lista)
        {
            return lista
                .OrderByDescending(o => o.fechaHoraFinalizacion)
                .ToList();
        }

        public string actualizarEstadoSismografo()
        {
            buscarEstadoFueraServicio();
            EstacionSismologica estacion = ordenSeleccionada.getEstacionSismologica();
            Sismografo sismografo = estacion.getSismografo();
            List<MotivoFueraServicio> motivos = motivosSeleccionados;

            sismografo.actualizarEstado(estadoFueraServicio, motivos, empleado);

            string mensaje = $"Estado actualizado con éxito.\n" +
                 $"Estación: {estacion.nombre}\n" +
                 $"Sismógrafo: {sismografo.getID()}\n" +
                 $"Motivos: {string.Join(", ", motivos)}\n" +
                 $"Responsable: {empleado.id}, {empleado.apellido} {empleado.nombre}\n" +
                 $"Fecha/Hora de cierre: {getFechaHoraActual()}";

            return mensaje;
        }   
        public void notificarCierre() 
        {
            pantalla.mostrarMensaje("Mails enviados");
        }
        public void finCU() 
        {
            pantalla.cerrarVentana();
            pantalla.Dispose();
            pantalla = null;
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

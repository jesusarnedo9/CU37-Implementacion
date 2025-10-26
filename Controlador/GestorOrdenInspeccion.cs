using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace ImplementacionCU37.Controlador
{
    public class GestorOrdenInspeccion
    {
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
        private MotivoTipo motivoActual;
        private List<MotivoTipo> motivosTipos;
        private Dictionary<string, MotivoTipo> mapaMotivos;
        private readonly List<MotivoFueraServicioDTO> motivosSeleccionados;


        public GestorOrdenInspeccion(Sistema sistema, PantallaCierreOrden pantalla)
        {
            this.sistema = sistema;
            this.pantalla = pantalla;
            this.motivosSeleccionados = new List<MotivoFueraServicioDTO>();
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
            var ordenesRealizadas = sistema.Ordenes
                .Where(o => o.esDeEmpleado(empleado) && o.estaRealizada())
                .OrderByDescending(o => o.fechaHoraFinalizacion)
                .Select(o => new OrdenInspeccionDTO
                {
                    Id = o.numeroOrden.ToString(),
                    Texto = o.ToString()
                })
                .ToList();

            pantalla.solicitarSeleccionOrden(ordenesRealizadas);
        }

        public void tomarOrdenSeleccionada(string numeroOrden)
        {
            ordenSeleccionada = ordenes.FirstOrDefault(o => o.numeroOrden.ToString() == numeroOrden);
            pantalla.solicitarObservacionCierre();
        }
        public void tomarObservacionCierre(string observacion)
        {
            this.observacion = observacion;
            List<MotivoTipo> motivosDisponibles = buscarMotivo();
            mapaMotivos = new Dictionary<string, MotivoTipo>();

            List<string> descripciones = new List<string>();
            foreach (MotivoTipo motivo in motivosDisponibles)
            {
                mapaMotivos[motivo.getDescripciones()] = motivo;
                descripciones.Add(motivo.getDescripciones());
            }
            pantalla.solicitarSeleccionMotivo(descripciones);
        }
        public List<MotivoTipo> buscarMotivo()
        {
            return sistema.MotivoTipos;
        }

        public void tomarMotivoSeleccionado(string descripcionSeleccionada, int indiceCheckbox)
        {
            if (mapaMotivos.ContainsKey(descripcionSeleccionada))
            {
                pantalla.solicitarComentario(descripcionSeleccionada, indiceCheckbox);
            }
        }
        public void tomarComentario(string descripcion, string comentario)
        {
            if (!string.IsNullOrWhiteSpace(comentario) && mapaMotivos.ContainsKey(descripcion))
            {
                MotivoTipo motivo = mapaMotivos[descripcion];
                motivosSeleccionados.Add(new MotivoFueraServicioDTO
                {
                    Motivo = motivo,
                    Comentario = comentario
                });
            }
        }

        public void motivosConfirmados()
        {
            pantalla.solicitarConfirmacionCierre();
        }
        public void tomarConfirmacionCierre(bool confirmacion)
        {
            this.confirmacionCierre = confirmacion;
            validarDatosIngresados();
        }
        public void validarDatosIngresados()
        {
            observacion = pantalla.txtObservacionCierre.Text;
            if (string.IsNullOrWhiteSpace(observacion))
            {
                MessageBox.Show("Debe ingresar una observación de cierre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pantalla.txtObservacionCierre.Enabled = true;
                pantalla.txtObservacionCierre.Focus();

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
        public void registrarCierreOI()
        {
            fechaHoraActual = getFechaHoraActual();
            cerrarOI();
            //Actualizar estado del sismógrafo
            actualizarEstadoSismografo();
            pantalla.mostrarMensaje("Orden cerrada y estado del sismógrafo actualizado.");
            notificarCierre();
            finCU();
        }

        public void cerrarOI()
        {
            ordenSeleccionada.cerrarOrden(fechaHoraActual);
        }

        public void actualizarEstadoSismografo()
        {
            estadoFueraServicio = buscarEstadoFueraServicio();
            ordenSeleccionada.actualizarEstadoSismografo(estadoFueraServicio, motivosSeleccionados, empleado);
            //Muestro la actualización en la pantalla
            var estacion = ordenSeleccionada.getEstacionSismologica();
            var sismografo = estacion.getIDSismografo();
            pantalla.mostrarActualizacionEstado(estacion, sismografo, motivosSeleccionados, empleado, fechaHoraActual);
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
        public Dictionary<string, OrdenDeInspeccion> ordenarOI(Dictionary<string, OrdenDeInspeccion> ordenesOrdenadas)
        {
            try
            {
                return ordenesOrdenadas
                    .OrderByDescending(o => o.Value.fechaHoraFinalizacion)
                    .ToDictionary(o => o.Key, o => o.Value);
            }
            catch (Exception)
            {
                pantalla.mostrarMensaje("Error ordenando ordenes");
                return new Dictionary<string, OrdenDeInspeccion>();
            }
        }

        public void notificarCierre()
        {
            var responsables = sistema.Empleados.Where(e => e.esResponsableReparacion()).ToList();
            if (responsables.Count == 0)
            {
                pantalla.mostrarMensaje("No hay responsables de reparación para notificar.");
                return;
            }
            foreach (var responsable in responsables)
            {
                string email = responsable.obtenerEmail();
            }
            pantalla.mostrarMensaje("Mails enviados");
        }
        public void finCU()
        {
            if (pantalla != null && !pantalla.IsDisposed)
            {
                pantalla.cerrarVentana();
                pantalla = null;
            }
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

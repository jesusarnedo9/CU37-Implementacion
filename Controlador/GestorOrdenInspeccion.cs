using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;


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
            // Cachear la colección para evitar accesos múltiples al getter
            ordenes = sistema.Ordenes;
            var allOrdenes = ordenes;
            var ordenesRealizadas = allOrdenes
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
            // Intentar parsear a int para búsquedas más rápidas y seguras
            if (int.TryParse(numeroOrden, out int num))
            {
                ordenSeleccionada = ordenes.FirstOrDefault(o => o.numeroOrden == num);
            }
            else
            {
                // Fallback a comparación por cadena si no se puede parsear
                ordenSeleccionada = ordenes.FirstOrDefault(o => o.numeroOrden.ToString() == numeroOrden);
            }
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
                // Si hay descripciones duplicadas, la última sobrescribe; considerar usar id si existe
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
            if (mapaMotivos != null && mapaMotivos.ContainsKey(descripcionSeleccionada))
            {
                pantalla.solicitarComentario(descripcionSeleccionada, indiceCheckbox);
            }
        }
        public void tomarComentario(string descripcion, string comentario)
        {
            if (!string.IsNullOrWhiteSpace(comentario) && mapaMotivos != null && mapaMotivos.ContainsKey(descripcion))
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
            // Usar la observación ya proporcionada por la UI; evitar acceder a controles desde el gestor
            if (string.IsNullOrWhiteSpace(this.observacion))
            {
                // Delegar la visualización al nivel UI y restaurar la pantalla para que el usuario pueda corregir
                if (pantalla != null)
                {
                    pantalla.mostrarMensaje("Debe ingresar una observación de cierre.");
                    // Limpiar selección interna de motivos para mantener consistencia
                    motivosSeleccionados.Clear();
                    // Pedir a la UI que desbloquee la observación y desmarque motivos
                    pantalla.restaurarPantallaParaObservacion();
                }
                return;
            }
            if (motivosSeleccionados == null || motivosSeleccionados.Count == 0)
            {
                if (pantalla != null)
                    pantalla.mostrarMensaje("Debe seleccionar al menos un motivo de cierre.");
                return;
            }
            // Capturar fecha/hora una sola vez para todo el proceso
            fechaHoraActual = getFechaHoraActual();
            registrarCierreOI();
        }
        public void registrarCierreOI()
        {
            // fechaHoraActual ya fue capturada en validarDatosIngresados
            cerrarOI();
            //Actualizar estado del sismógrafo
            actualizarEstadoSismografo();
            if (pantalla != null)
                pantalla.mostrarMensaje("Orden cerrada y estado del sismógrafo actualizado.");
            notificarCierre();
            finCU();
        }

        public void cerrarOI()
        {
            if (ordenSeleccionada != null)
            {
                ordenSeleccionada.cerrarOrden(fechaHoraActual);
            }
        }

        public void actualizarEstadoSismografo()
        {
            estadoFueraServicio = buscarEstadoFueraServicio();
            if (ordenSeleccionada != null)
            {
                ordenSeleccionada.actualizarEstadoSismografo(estadoFueraServicio, motivosSeleccionados, empleado);
                //Muestro la actualización en la pantalla
                var estacion = ordenSeleccionada.getEstacionSismologica();
                var sismografo = estacion.getIDSismografo();
                pantalla.mostrarActualizacionEstado(estacion, sismografo, motivosSeleccionados, empleado, fechaHoraActual);
            }
        }

        public DateTime getFechaHoraActual() => DateTime.Now;
        public Estado buscarEstadoFueraServicio()
        {
            // Usar LINQ para mayor claridad
            return sistema.EstadosDisponibles?.FirstOrDefault(estado => estado.esAmbitoSismografo() && estado.esFueraServicio());
        }
        public Dictionary<string, OrdenDeInspeccion> ordenarOI(Dictionary<string, OrdenDeInspeccion> ordenesOrdenadas)
        {
            if (ordenesOrdenadas == null || ordenesOrdenadas.Count ==0)
                return new Dictionary<string, OrdenDeInspeccion>();

            return ordenesOrdenadas
                .OrderByDescending(o => o.Value.fechaHoraFinalizacion)
                .ToDictionary(o => o.Key, o => o.Value);
        }

        public void notificarCierre()
        {
            var responsables = sistema?.Empleados?.Where(e => e.esResponsableReparacion()).ToList() ?? new List<Empleado>();
            if (responsables.Count ==0)
            {
                if (pantalla != null)
                    pantalla.mostrarMensaje("No hay responsables de reparación para notificar.");
                return;
            }

            // Informar al usuario que el envío comienza y realizar el envío en segundo plano
            if (pantalla != null)
                pantalla.mostrarMensaje("Mails enviados.");

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

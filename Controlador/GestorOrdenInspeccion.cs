using ImplementacionCU37.Dao;
using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


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
        private readonly string _connectionString;

        public GestorOrdenInspeccion(Sistema sistema, PantallaCierreOrden pantalla)
        {
            this.sistema = sistema;
            this.pantalla = pantalla;
            this.motivosSeleccionados = new List<MotivoFueraServicioDTO>();
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
                throw new Exception("No se encontró la cadena de conexión 'DefaultConnection' en app.config.");
        }

        // Main
        public void opcionCerrarOrdenInspeccion()
        {
            Console.WriteLine(ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString ?? "No encontrada");
            sistema.RecargarSesionPorDefecto();
            buscarEmpleado();
            buscarOrdenesInspecciones();
        }

        // Metodos
        public void buscarEmpleado()
        {
            if (sistema == null || sistema.SesionActiva == null)
            {
                if (pantalla != null) pantalla.mostrarMensaje("No hay sesión activa. Verifique la configuración de usuario.");
                empleado = null;
                return;
            }
            empleado = sistema.SesionActiva.getEmpleado();
        }

        public void buscarOrdenesInspecciones()
        {
            //evita accesos múltiples al getter
            ordenes = sistema?.Ordenes ?? new List<OrdenDeInspeccion>();
            IEnumerable<OrdenDeInspeccion> consulta = ordenes.Where(o => o.estaRealizada());

            if (empleado != null)
            {
                consulta = consulta.Where(o => o.esDeEmpleado(empleado));
            }
            else
            {
                Console.WriteLine("Advertencia: no hay empleado en sesión; mostrando todas las órdenes realizadas.");
            }

            var ordenesRealizadas = consulta
            .OrderByDescending(o => o.fechaHoraFin)
            .Select(o => new OrdenInspeccionDTO
            {
                Id = o.nroOrden.ToString(),
                Texto = o.ToString()
            })
            .ToList();

            pantalla.solicitarSeleccionOrden(ordenesRealizadas);
        }

        public void tomarOrdenSeleccionada(string numeroOrden)
        {
            if (int.TryParse(numeroOrden, out int num))
            {
                ordenSeleccionada = ordenes.FirstOrDefault(o => o.nroOrden == num);
            }
            else
            {
                ordenSeleccionada = ordenes.FirstOrDefault(o => o.nroOrden.ToString() == numeroOrden);
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
        public void quitarMotivoSeleccionado(string descripcionSeleccionada, int indiceCheckbox)
        {
            if (mapaMotivos == null || !mapaMotivos.ContainsKey(descripcionSeleccionada)) return;

            var dto = motivosSeleccionados.FirstOrDefault(m => m.Motivo != null && m.Motivo.getDescripciones() == descripcionSeleccionada && m.Comentario != null);
            if (dto != null)
            {
                motivosSeleccionados.Remove(dto);
                Debug.WriteLine($"Motivo removido: {descripcionSeleccionada}");
            }
            else
            {
                var tipo = mapaMotivos[descripcionSeleccionada];
                var dto2 = motivosSeleccionados.FirstOrDefault(m => m.Motivo != null && m.Motivo.idMotivoTipo == tipo.idMotivoTipo);
                if (dto2 != null) motivosSeleccionados.Remove(dto2);
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
            if (string.IsNullOrWhiteSpace(this.observacion))
            {
                if (pantalla != null)
                {
                    pantalla.mostrarMensaje("Debe ingresar una observación de cierre.");
                    motivosSeleccionados.Clear();
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
            fechaHoraActual = getFechaHoraActual();
            Debug.WriteLine($"Persistir cierre: orden={ordenSeleccionada?.idOrden}/{ordenSeleccionada?.nroOrden} idEstadoFK={ordenSeleccionada?.idEstadoFK} empId={empleado?.idEmpleado}");
            registrarCierreOI();
        }
        public void registrarCierreOI()
        {
            cerrarOI();

            CambioEstadoDao cambioDao = null;
            MotivoFueraServicioDao motivoDao = null;
            OrdenDeInspeccionDao ordenDao = null;
            SismografoDao sismDao = null;

            if (!string.IsNullOrWhiteSpace(_connectionString))
            {
                try
                {
                    cambioDao = new CambioEstadoDao(_connectionString);
                    motivoDao = new MotivoFueraServicioDao(_connectionString);
                    ordenDao = new OrdenDeInspeccionDao(_connectionString);
                    sismDao = new SismografoDao(_connectionString);


                    var estacion = ordenSeleccionada.getEstacionSismologica();
                    var sism = estacion.getSismografo();

                    var cambioACerrar = new CambioEstado(fechaHoraActual)
                    {
                        responsableLogueado = empleado,
                        idEmpleadoFK = empleado.idEmpleado,
                    };
                    cambioACerrar.finalizar();
                    int cambioId = cambioDao.Insert(cambioACerrar, sism.idSismografo);

                    foreach (var dto in motivosSeleccionados)
                    {
                        var motivoEntity = new MotivoFueraServicio(dto.Motivo, dto.Comentario);
                        motivoDao.Insert(motivoEntity, cambioId);
                    }

                    // Actualizo en db(fecha cierre, observacion)
                    var entidadCerrada = sistema.EstadosDisponibles?.FirstOrDefault(e => e.esAmbitoOI() && e.esCerrada());
                    if (entidadCerrada != null)
                    {
                        ordenSeleccionada.idEstadoFK = entidadCerrada.idEstado;
                    }
                    else
                    {
                        Debug.WriteLine("Advertencia: no se encontró el Estado 'CERRADA' en sistema.EstadosDisponibles. Se persistirá el ID actual.");
                    }

                    ordenSeleccionada.observacionCierre = this.observacion;
                    ordenSeleccionada.setFechaHoraCierre(fechaHoraActual);
                    ordenDao.Update(ordenSeleccionada);

                    estadoFueraServicio = buscarEstadoFueraServicio();
                    sism.setEstadoActual(estadoFueraServicio, motivosSeleccionados, empleado);
                    sismDao.Insert(sism);
                }
                catch (Exception ex)
                {
                    if (pantalla != null)
                        pantalla.mostrarMensaje("Error al persistir cierre en la base: " + ex.Message);
                }
            }
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
            return sistema.EstadosDisponibles?.FirstOrDefault(estado => estado.esAmbitoSismografo() && estado.esFueraServicio());
        }
        public Dictionary<string, OrdenDeInspeccion> ordenarOI(Dictionary<string, OrdenDeInspeccion> ordenesOrdenadas)
        {
            if (ordenesOrdenadas == null || ordenesOrdenadas.Count == 0)
                return new Dictionary<string, OrdenDeInspeccion>();

            return ordenesOrdenadas
            .OrderByDescending(o => o.Value.fechaHoraFin)
            .ToDictionary(o => o.Key, o => o.Value);
        }

        public void notificarCierre()
        {
            var responsables = sistema?.Empleados?.Where(e => e.esResponsableReparacion()).ToList() ?? new List<Empleado>();
            if (responsables.Count == 0)
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

        public void RecargarOrdenesDesdeBD()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                if (pantalla != null) pantalla.mostrarMensaje("No hay cadena de conexión configurada.");
                return;
            }

            try
            {
                var ordenDao = new OrdenDeInspeccionDao(_connectionString);
                var nuevas = ordenDao.GetAll();
                sistema.Ordenes = nuevas;

                // Refrescar la UI
                buscarOrdenesInspecciones();
            }
            catch (Exception ex)
            {
                if (pantalla != null) pantalla.mostrarMensaje("Error al recargar órdenes desde BD: " + ex.Message);
            }
        }
    }
}

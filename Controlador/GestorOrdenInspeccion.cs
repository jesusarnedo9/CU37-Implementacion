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
        
        // Atributos propios
        private string comentario;
        private bool confirmacionCierre;
        //private DateTime fechaActual;
        //private TimeSpan horaActual;
        private string observacion;
        private OrdenDeInspeccion ordenSeleccionada;
        //private List<string> solicitudMotivo;
        private Empleado responsableLogueado;
        private List<OrdenDeInspeccion> ordenes;
        private List<string> motivosSeleccionados;
        //private InterfazEmail interfazEmail;
        //private PantallaCCRS pantallaCCRS;
        private PantallaCierreOrden pantalla;
        private List<Estado> estadosDisponibles;
        private Estado estado;
        private Sesion sesion;

        //Datos de prueba
        private Empleado empleado;
        private Usuario usuario;

        private List<MotivoTipo> motivoTipo = new List<MotivoTipo>
        {
            new MotivoTipo("Falla eléctrica"),
            new MotivoTipo("Mantenimiento programado"),
            new MotivoTipo("Condiciones climáticas"),
            new MotivoTipo("Robo o vandalismo")
        };


        public GestorOrdenInspeccion(PantallaCierreOrden pantalla)
        {
            this.pantalla = pantalla;

            // Crear estados ordenes
            var estadoRealizada = new Estado { nombreEstado = Estado.ESTADO_REALIZADA_OI, ambito = Estado.AMBITO_OI };
            var estadoCerrada = new Estado { nombreEstado = Estado.ESTADO_CERRADA_OI, ambito = Estado.AMBITO_OI };
            // Crear estados sismógrafos
            var estadoFueraServicio = new Estado { nombreEstado = Estado.ESTADO_FUERA_SERVICIO_S, ambito = Estado.AMBITO_SISMOGRAFO };
            var estadoRealizado = new Estado { nombreEstado = Estado.ESTADO_REALIZADO_S, ambito = Estado.AMBITO_SISMOGRAFO };

            // Crear lista de estados
            estadosDisponibles = new List<Estado> { estadoRealizada, estadoCerrada, estadoFueraServicio, estadoRealizado };

            // Crear empleados
            var empleadoJesus = new Empleado("Jesus", "Arnedo", "jesus@mail.com", "12345", 5, Rol.RESPONSABLE_REPARACION);
            var empleadoNazareno = new Empleado("Nazareno", "Sotomayor", "nanosotomayor@gmail.com", "56789", 2, Rol.ADMINISTRADOR_REPARACION);
            var empleadoPedro = new Empleado("Pedro", "Colapinto", "colapa@gmail.com", "434343", 4, Rol.RESPONSABLE_REPARACION);
            var empleadoJuancito = new Empleado("Juancito", "Lopez", "juanete@gmail.com", "5645559", 8, Rol.ADMINISTRADOR_REPARACION);

            // Crear usuarios y sesión (usamos a Jesús como logueado)
            usuario = new Usuario("jesus", empleadoJesus);
            sesion = new Sesion(usuario);
            responsableLogueado = usuario.getEmpleado();

            // Crear sismógrafos
            var s1 = new Sismografo { identificadorSismografo = "SISMO-001", nroSerie = "SN001", fechaAdquisicion = DateTime.Now, estadoActual = estadoRealizado };
            var s2 = new Sismografo { identificadorSismografo = "SISMO-002", nroSerie = "SN002", fechaAdquisicion = DateTime.Now, estadoActual = estadoFueraServicio };
            var s3 = new Sismografo { identificadorSismografo = "SISMO-003", nroSerie = "SN003", fechaAdquisicion = DateTime.Now, estadoActual = estadoRealizado };
            var s4 = new Sismografo { identificadorSismografo = "SISMO-004", nroSerie = "SN003", fechaAdquisicion = DateTime.Now, estadoActual = estadoFueraServicio };

            // Crear estaciones
            var e1 = new EstacionSismologica(s1)
            {
                codigoEstacion = "EST001",
                nombre = "Estación Córdoba",
                latitud = -31.4167,
                longitud = -64.1833,
                documentoCertificacionAdq = "DOC001",
                nroCertificacionAdquisicion = "CERT001",
                fechaSituacionCertificacion = "2023-06-01"
            };

            var e2 = new EstacionSismologica(s2)
            {
                codigoEstacion = "EST002",
                nombre = "Estación Mendoza",
                latitud = -32.8908,
                longitud = -68.8272,
                documentoCertificacionAdq = "DOC002",
                nroCertificacionAdquisicion = "CERT002",
                fechaSituacionCertificacion = "2023-07-01"
            };

            var e3 = new EstacionSismologica(s3)
            {
                codigoEstacion = "EST003",
                nombre = "Estación Salta",
                latitud = -24.7821,
                longitud = -65.4232,
                documentoCertificacionAdq = "DOC003",
                nroCertificacionAdquisicion = "CERT003",
                fechaSituacionCertificacion = "2023-08-01"
            };

            var e4 = new EstacionSismologica(s4)
            {
                codigoEstacion = "EST004",
                nombre = "Estación Ushuaia",
                latitud = -54.8019,
                longitud = -68.3030,
                documentoCertificacionAdq = "DOC004",
                nroCertificacionAdquisicion = "CERT004",
                fechaSituacionCertificacion = "2023-09-01"
            };

            // Crear órdenes
            var orden1 = new OrdenDeInspeccion(1, DateTime.Now.AddDays(-4), e1, estadoRealizada, empleadoJesus);
            orden1.fechaHoraFinalizacion = DateTime.Now.AddDays(-15);
            var orden2 = new OrdenDeInspeccion(2, DateTime.Now.AddDays(-3), e2, estadoCerrada, empleadoNazareno);
            orden2.fechaHoraFinalizacion = DateTime.Now.AddDays(-5);
            var orden3 = new OrdenDeInspeccion(3, DateTime.Now.AddDays(-8), e3, estadoRealizada, empleadoJesus);
            orden3.fechaHoraFinalizacion = DateTime.Now.AddDays(-7);
            var orden4 = new OrdenDeInspeccion(4, DateTime.Now.AddDays(-1), e4, estadoRealizada, empleadoJesus);
            orden4.fechaHoraFinalizacion = DateTime.Now.AddDays(-12);

            // Cargar lista
            ordenes = new List<OrdenDeInspeccion> { orden1, orden2, orden3, orden4 };   
        }

        // Métodos
        public void opcionCerrarOrdenInspeccion()
        {
            // 1. Obtener el empleado desde la sesión
            buscarUsuario();

            // 2. Filtrar órdenes realizadas del empleado
            List<OrdenDeInspeccion> ordenesRealizadas = new List<OrdenDeInspeccion>();
            foreach (OrdenDeInspeccion orden in ordenes)
            {
                if (orden.esDeEmpleado(empleado) && orden.estaRealizada())
                {
                    ordenesRealizadas.Add(orden);
                }
            }

            ordenesRealizadas = ordenarOI(ordenesRealizadas);

            // 3. Enviar a la pantalla para mostrar
            pantalla.solicitarSeleccionOrden(ordenesRealizadas);
        }


        public void buscarUsuario()
        {
            Usuario usuario = sesion.getUsuario();
            empleado = usuario.getEmpleado();
            responsableLogueado = usuario.getRILogueado() ? empleado : null;
        }

        public Estado buscarEstadoFueraServicio()
        {
            foreach (Estado estado in estadosDisponibles)
            {
                if (estado.esAmbitoSismografo() && estado.esFueraServicio())
                {
                    return estado;

                }
            }
            return null;
        }

        public void actualizarEstadoSismografo()
        {
            Estado estadoFS = buscarEstadoFueraServicio();
            if (estadoFS == null)
            {
                MessageBox.Show("No se encontró el estado 'Fuera de Servicio'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener estación desde la orden seleccionada
            EstacionSismologica estacion = ordenSeleccionada.getEstacionSismologica();

            // Obtener sismógrafo de la estación
            Sismografo sismografo = estacion.getSismografo();

            // Cerrar estado actual
            sismografo.cerrarEstadoActual();

            // Crear nuevo cambio de estado

            // Convertir strings a objetos MotivoFueraServicio
            List<MotivoFueraServicio> motivos = motivosSeleccionados
                .Select(m => new MotivoFueraServicio(m))
                .ToList();
            CambioEstado nuevoCambio = new CambioEstado(DateTime.Now, motivos);
            nuevoCambio.setRILogueado(responsableLogueado);
            nuevoCambio.setFechaHoraCierre(DateTime.Now);
            nuevoCambio.setMotivosSeleccionado(motivos.FirstOrDefault());

            CambioEstado cambioActual = sismografo.historialEstados.FirstOrDefault(ce => ce.esActual());
            if (cambioActual != null)
                cambioActual.finalizar();

            sismografo.agregarCambioEstado(nuevoCambio, estadoFS);

            // Obtener el último cambio de estado agregado al historial
            CambioEstado ultimoCambio = sismografo.historialEstados.LastOrDefault();

            if (ultimoCambio != null)
            {
                string nombreEstacion = estacion.nombre;
                string idSismografo = sismografo.getID(); 
                string motivo = ultimoCambio.getMotivo() != null ? ultimoCambio.getMotivo().ToString() : "Sin motivo";
                string fechaFin = ultimoCambio.fechaHoraFin.HasValue ? ultimoCambio.fechaHoraFin.Value.ToString("g") : "Sin definir";


                string mensaje = $"Estado actualizado con éxito.\n" +
                     $"Estación: {nombreEstacion}\n" +
                     $"Sismógrafo: {idSismografo}\n" +
                     $"Motivos: {string.Join(", ", ultimoCambio.motivos)}\n" + 
                     $"Responsable: {empleado.id}, {empleado.apellido} {empleado.nombre}\n" +
                     $"Fecha/Hora de cierre: {ultimoCambio.fechaHoraFin?.ToString("g")}";
                MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(" No se pudo obtener el cambio de estado recientemente agregado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public List<MotivoTipo> buscarMotivo()
        {
            return motivoTipo;
        }

        //public void finCU() { }
        //public DateTime getFechaHoraActual() => DateTime.Now;
        //public void notificarCierre() { }
        public List<OrdenDeInspeccion> ordenarOI(List<OrdenDeInspeccion> lista)
        {
            return lista
                .OrderByDescending(o => o.fechaHoraFinalizacion)
                .ToList();
        }
        //public void publicarEnPantallaCCRS() { }
        public void registrarCierre() 
        {
            if (ordenSeleccionada == null)
            {
                pantalla.mostrarMensaje("Debe seleccionar una orden antes de cerrarla.");
                return;
            }

            // Obtener estado cerrado
            Estado estadoCerrada = new Estado
            {
                nombreEstado = Estado.ESTADO_CERRADA_OI,
                ambito = Estado.AMBITO_OI
            };

            // Setear fecha, hora y estado
            ordenSeleccionada.setFechaHoraCierre(DateTime.Now);
            ordenSeleccionada.setEstado(estadoCerrada);

            pantalla.mostrarMensaje("La orden fue cerrada exitosamente.");

            // Actualizar estado del sismógrafo
            actualizarEstadoSismografo();

            pantalla.mostrarMensaje("Orden cerrada y estado del sismógrafo actualizado.");

            // Enviar email
            pantalla.mostrarMensaje("Mails enviados");
        }
        public void tomarComentario(string comentario) => this.comentario = comentario;
        public void tomarConfirmacionCierre(bool confirmacion) => this.confirmacionCierre = confirmacion;
        public void tomarObservacionCierre(string observacion) 
        {
            this.observacion = observacion;
            pantalla.solicitarSeleccionMotivo(buscarMotivo());
        }

        public void tomarOrdenSeleccionada(OrdenDeInspeccion orden)
        {
            this.ordenSeleccionada = orden;
        }

        public void tomarSeleccionMotivo(List<string> solicitudMotivos) => this.motivosSeleccionados = solicitudMotivos;

        public void validarDatosIngresados() 
        {
            // Verifica si hay observación
            bool hayObservacion = string.IsNullOrWhiteSpace(observacion);

            // Verifica si hay al menos un motivo seleccionado
            bool hayMotivos = motivosSeleccionados != null && motivosSeleccionados.Count > 0;

            if (hayObservacion)
            {
                MessageBox.Show("Debe ingresar una observación de cierre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!hayMotivos)
            {
                MessageBox.Show("Debe seleccionar al menos un motivo de cierre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si todo está bien
            registrarCierre();
        }
    }
}

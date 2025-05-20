using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

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
            // Crear estados
            var estadoRealizada = new Estado { nombreEstado = Estado.ESTADO_REALIZADA, ambito = Estado.AMBITO_OI };
            var estadoCerrada = new Estado { nombreEstado = Estado.ESTADO_CERRADA, ambito = Estado.AMBITO_OI };
            var estadoFueraServicio = new Estado { nombreEstado = Estado.ESTADO_FUERA_SERVICIO, ambito = Estado.AMBITO_OI };
            var estadoRealizado = new Estado { nombreEstado = Estado.ESTADO_REALIZADO, ambito = Estado.AMBITO_OI };

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
            var s1 = new Sismografo { identificadorSismografo = "SISMO-001", nroSerie = "SN001", fechaAdquisicion = DateTime.Now, estadoActual = estadoRealizada };
            var s2 = new Sismografo { identificadorSismografo = "SISMO-002", nroSerie = "SN002", fechaAdquisicion = DateTime.Now, estadoActual = estadoCerrada };
            var s3 = new Sismografo { identificadorSismografo = "SISMO-003", nroSerie = "SN003", fechaAdquisicion = DateTime.Now, estadoActual = estadoFueraServicio };
            var s4 = new Sismografo { identificadorSismografo = "SISMO-004", nroSerie = "SN004", fechaAdquisicion = DateTime.Now, estadoActual = estadoRealizado };

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
            var orden2 = new OrdenDeInspeccion(2, DateTime.Now.AddDays(-3), e2, estadoCerrada, empleadoNazareno);
            var orden3 = new OrdenDeInspeccion(3, DateTime.Now.AddDays(-2), e3, estadoFueraServicio, empleadoPedro);
            var orden4 = new OrdenDeInspeccion(4, DateTime.Now.AddDays(-1), e4, estadoRealizado, empleadoJuancito);

            // Cargar lista
            ordenes = new List<OrdenDeInspeccion> { orden1, orden2, orden3, orden4 };
        }



        // Métodos
        public void opcionCerrarOrdenInspeccion()
        {
            // 1. Obtener el empleado desde la sesión
            //Usuario usuario = Sesion.actual.getUsuario();
            //Empleado empleado = usuario.getEmpleado();

            // 2. Filtrar órdenes realizadas del empleado
            //List<OrdenDeInspeccion> ordenesRealizadas = new List<OrdenDeInspeccion>();
            //foreach (OrdenDeInspeccion orden in ordenes)
            //{
            //    if (orden.esDeEmpleado(empleado) && orden.estaRealizada())
            //    {
            //        ordenesRealizadas.Add(orden);
            //    }
            //}

            // 3. Enviar a la pantalla para mostrar
            //pantalla.solicitarSeleccionOrden(ordenesRealizadas);
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
        public void ordenarOI() { }
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

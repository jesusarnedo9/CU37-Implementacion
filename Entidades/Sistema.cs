using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementacionCU37.Entidades
{
    public class Sistema
    {
        public List<MotivoTipo> MotivoTipos { get; set; }
        public List<Estado> EstadosDisponibles { get; set; }
        public List<Empleado> Empleados { get; set; }
        public Sesion SesionActiva { get; set; }
        public Empleado ResponsableLogueado { get; set; }
        public List<OrdenDeInspeccion> Ordenes { get; set; }

        public Sistema()
        {
            // Inicializar listas
            MotivoTipos = new List<MotivoTipo>();
            EstadosDisponibles = new List<Estado>();
            Empleados = new List<Empleado>();
            Ordenes = new List<OrdenDeInspeccion>();
            // Cargar datos iniciales
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            // Datos harcodeados
            MotivoTipos.Add(new MotivoTipo("Falla eléctrica"));
            MotivoTipos.Add(new MotivoTipo("Mantenimiento programado"));
            MotivoTipos.Add(new MotivoTipo("Condiciones climáticas"));
            MotivoTipos.Add(new MotivoTipo("Robo o vandalismo"));

            // Crear estados
            var estadoRealizada = new Estado { nombreEstado = Estado.ESTADO_REALIZADA_OI, ambito = Estado.AMBITO_OI };
            var estadoCerrada = new Estado { nombreEstado = Estado.ESTADO_CERRADA_OI, ambito = Estado.AMBITO_OI };
            var estadoFueraServicio = new Estado { nombreEstado = Estado.ESTADO_FUERA_SERVICIO_S, ambito = Estado.AMBITO_SISMOGRAFO };
            var estadoRealizado = new Estado { nombreEstado = Estado.ESTADO_REALIZADO_S, ambito = Estado.AMBITO_SISMOGRAFO };

            EstadosDisponibles.Add(estadoRealizada);
            EstadosDisponibles.Add(estadoCerrada);
            EstadosDisponibles.Add(estadoFueraServicio);
            EstadosDisponibles.Add(estadoRealizado);

            // Crear empleados
            var jesus = new Empleado("Jesus", "Arnedo", "jesus@mail.com", "12345", 5, Rol.RESPONSABLE_REPARACION);
            var nano = new Empleado("Nazareno", "Sotomayor", "nanosotomayor@gmail.com", "56789", 2, Rol.ADMINISTRADOR_REPARACION);
            var pedro = new Empleado("Pedro", "Colapinto", "colapa@gmail.com", "434343", 4, Rol.RESPONSABLE_REPARACION);
            var juan = new Empleado("Juancito", "Lopez", "juanete@gmail.com", "5645559", 8, Rol.ADMINISTRADOR_REPARACION);

            Empleados.Add(jesus);
            Empleados.Add(nano);
            Empleados.Add(pedro);
            Empleados.Add(juan);

            // Usuario logueado
            var usuario = new Usuario("jesus", jesus);
            SesionActiva = new Sesion(usuario);

            // Sismografos
            var s1 = new Sismografo { identificadorSismografo = "SISMO-001", nroSerie = "SN001", fechaAdquisicion = DateTime.Now, estadoActual = estadoRealizado };
            var s2 = new Sismografo { identificadorSismografo = "SISMO-002", nroSerie = "SN002", fechaAdquisicion = DateTime.Now, estadoActual = estadoFueraServicio };
            var s3 = new Sismografo { identificadorSismografo = "SISMO-003", nroSerie = "SN003", fechaAdquisicion = DateTime.Now, estadoActual = estadoRealizado };
            var s4 = new Sismografo { identificadorSismografo = "SISMO-004", nroSerie = "SN003", fechaAdquisicion = DateTime.Now, estadoActual = estadoFueraServicio };

            // Estaciones
            var e1 = new EstacionSismologica(s1) { codigoEstacion = "EST001", nombre = "Estación Córdoba", latitud = -31.4167, longitud = -64.1833, documentoCertificacionAdq = "DOC001", nroCertificacionAdquisicion = "CERT001", fechaSituacionCertificacion = "2023-06-01" };
            var e2 = new EstacionSismologica(s2) { codigoEstacion = "EST002", nombre = "Estación Mendoza", latitud = -32.8908, longitud = -68.8272, documentoCertificacionAdq = "DOC002", nroCertificacionAdquisicion = "CERT002", fechaSituacionCertificacion = "2023-07-01" };
            var e3 = new EstacionSismologica(s3) { codigoEstacion = "EST003", nombre = "Estación Salta", latitud = -24.7821, longitud = -65.4232, documentoCertificacionAdq = "DOC003", nroCertificacionAdquisicion = "CERT003", fechaSituacionCertificacion = "2023-08-01" };
            var e4 = new EstacionSismologica(s4) { codigoEstacion = "EST004", nombre = "Estación Ushuaia", latitud = -54.8019, longitud = -68.3030, documentoCertificacionAdq = "DOC004", nroCertificacionAdquisicion = "CERT004", fechaSituacionCertificacion = "2023-09-01" };

            // Crear órdenes de inspección
            var orden1 = new OrdenDeInspeccion(1, DateTime.Now.AddDays(-4), e1, estadoRealizada, jesus);
            orden1.fechaHoraFinalizacion = DateTime.Now.AddDays(-15);
            var orden2 = new OrdenDeInspeccion(2, DateTime.Now.AddDays(-3), e2, estadoCerrada, nano);
            orden2.fechaHoraFinalizacion = DateTime.Now.AddDays(-5);
            var orden3 = new OrdenDeInspeccion(3, DateTime.Now.AddDays(-8), e3, estadoRealizada, jesus);
            orden3.fechaHoraFinalizacion = DateTime.Now.AddDays(-7);
            var orden4 = new OrdenDeInspeccion(4, DateTime.Now.AddDays(-1), e4, estadoRealizada, jesus);
            orden4.fechaHoraFinalizacion = DateTime.Now.AddDays(-12);
            var orden5 = new OrdenDeInspeccion(5, DateTime.Now.AddDays(-2), e3, estadoCerrada, jesus);
            orden5.fechaHoraFinalizacion = DateTime.Now.AddDays(-14);

            Ordenes.Add(orden1);
            Ordenes.Add(orden2);
            Ordenes.Add(orden3);
            Ordenes.Add(orden4);
            Ordenes.Add(orden5);
        }
    }
}
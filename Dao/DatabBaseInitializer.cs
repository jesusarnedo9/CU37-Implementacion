using ImplementacionCU37.Dao;
using ImplementacionCU37.Entidades;
using ImplementacionCU37.Estados;
using System;
using System.Diagnostics;
using System.Linq;

namespace ImplementacionCU37
{
    public static class DatabaseInitializer
    {
        public static void Seed(string connectionString)
        {
            string cs = connectionString;

            var estadoDao = new EstadoDao(cs);
            var rolDao = new RolDao(cs);
            var empleadoDao = new EmpleadoDao(cs);
            var sismografoDao = new SismografoDao(cs);
            var estacionDao = new EstacionSismologicaDao(cs);
            var usuarioDao = new UsuariosDao(cs);
            var motivoTipoDao = new MotivoTipoDao(cs);
            var ordenDao = new OrdenDeInspeccionDao(cs);
            Debug.WriteLine("== SEED INICIADO ==");


            // PASO 1 & 2: Roles, Estados y Motivos (Tablas Base)

            // --- Roles ---
            if (rolDao.GetAll().Count == 0)
            {
                rolDao.Insert(new Entidades.Rol(Entidades.Rol.RESPONSABLE_REPARACION, "Encargado de gestionar reparaciones"));
                rolDao.Insert(new Entidades.Rol(Entidades.Rol.ADMINISTRADOR_REPARACION, "Gestiona el sistema"));
            }
            var roles = rolDao.GetAll();
            var rolResp = roles.FirstOrDefault(r => r.nombre == Entidades.Rol.RESPONSABLE_REPARACION);
            var rolAdm = roles.FirstOrDefault(r => r.nombre == Entidades.Rol.ADMINISTRADOR_REPARACION);

            // --- Estados ---
            if (estadoDao.GetAll().Count == 0)
            {
                estadoDao.Insert(new Estado { nombreEstado = "ACTIVO", ambito = "SISMOGRAFO" });
                estadoDao.Insert(new Estado { nombreEstado = "FUERA_SERVICIO", ambito = "SISMOGRAFO" });
                estadoDao.Insert(new Estado { nombreEstado = "PENDIENTE", ambito = "ORDEN" });
                estadoDao.Insert(new Estado { nombreEstado = "REALIZADA", ambito = "ORDEN" });
                estadoDao.Insert(new Estado { nombreEstado = "CERRADA", ambito = "ORDEN" });
                estadoDao = new EstadoDao(cs);
            }
            var estados = estadoDao.GetAll();
            var estadoActivo = estados.FirstOrDefault(e => e.nombreEstado == "ACTIVO" && e.ambito == "SISMOGRAFO");
            var estadoFueraServicio = estados.FirstOrDefault(e => e.nombreEstado == "FUERA_SERVICIO" && e.ambito == "SISMOGRAFO");
            var estadoRealizadaDB = estados.FirstOrDefault(e => e.nombreEstado == "REALIZADA" && e.ambito == "ORDEN");
            var estadoCerradaDB = estados.FirstOrDefault(e => e.nombreEstado == "CERRADA" && e.ambito == "ORDEN");

            // --- Motivos Tipo ---
            if (motivoTipoDao.GetAll().Count == 0)
            {
                motivoTipoDao.Insert(new MotivoTipo("Falla eléctrica"));
                motivoTipoDao.Insert(new MotivoTipo("Mantenimiento programado"));
                motivoTipoDao.Insert(new MotivoTipo("Condiciones climáticas"));
                motivoTipoDao.Insert(new MotivoTipo("Robo o vandalismo"));
            }

            // PASO 3: Empleados y Usuario (FKs a Rol)

            if (empleadoDao.GetAll().Count == 0 && rolResp != null && rolAdm != null)
            {
                empleadoDao.Insert(new Empleado("Jesus", "Arnedo", "jesus@mail.com", "12345", 5, rolResp));
                empleadoDao.Insert(new Empleado("Nazareno", "Sotomayor", "nanosotomayor@gmail.com", "56789", 2, rolAdm));
                empleadoDao.Insert(new Empleado("Pedro", "Colapinto", "colapa@gmail.com", "434343", 4, rolResp));
                empleadoDao.Insert(new Empleado("Juancito", "Lopez", "juanete@gmail.com", "5645559", 8, rolAdm));
            }
            var empleados = empleadoDao.GetAll();
            var jesus = empleados.FirstOrDefault(e => e.nombre == "Jesus");
            var nano = empleados.FirstOrDefault(e => e.nombre == "Nazareno");

            // --- Usuario de Sesión ---
            if (usuarioDao.GetAll().Count == 0 && jesus != null)
            {
                usuarioDao.Insert(new Entidades.Usuario("jesus", jesus) { contrasena = "default_pass" });
            }

            // PASO 4: Sismógrafos (CORREGIDO: Insertar los 4 Sismógrafos)
            if (sismografoDao.GetAll().Count == 0)
            {
                // Validar que los estados existen
                if (estadoActivo == null || estadoFueraServicio == null)
                {
                    Debug.WriteLine("Error: no se encontraron los estados necesarios para cargar los sismógrafos.");
                    return;
                }

                // Función auxiliar para insertar y devolver el objeto con su ID asignado
                Func<string, string, Estado, Sismografo> insertarSismografo = (id, serie, estado) =>
                {
                    var sism = new Sismografo
                    {
                        fechaAdquisicion = DateTime.Now,
                        identificadorSismografo = id,
                        numeroSerie = serie,
                        estadoActual = estado
                    };

                    int nuevoId = sismografoDao.Insert(sism);
                    sism.idSismografo = nuevoId;

                    Debug.WriteLine($"Sismógrafo insertado: {id} con ID {nuevoId} y estado {estado.nombreEstado}");
                    return sism;
                };


                // Inserta los 4 sismógrafos
                insertarSismografo("SISM-001".ToUpper(), "SN001", estadoActivo);
                insertarSismografo("SISM-002".ToUpper(), "SN002", estadoFueraServicio);
                insertarSismografo("SISM-003".ToUpper(), "SN003", estadoActivo);
                insertarSismografo("SISM-004".ToUpper(), "SN004", estadoFueraServicio);


                Debug.WriteLine($"Total de sismógrafos insertados: 4");
            }

            var sismografos = sismografoDao.GetAll();
            foreach (var s in sismografos)
                Debug.WriteLine($"Sismógrafo en memoria: {s.idSismografo} - {s.identificadorSismografo}");


            // PASO 5: Estaciones Sismológicas (FKs a Sismógrafo)
            Console.WriteLine($"sismografos.Count = {sismografos.Count}");
            Console.WriteLine($"estacionDao.GetAll().Count = {estacionDao.GetAll().Count}");


            if (estacionDao.GetAll().Count == 0 && sismografos.Count >= 4)
            {
                // Mapeo directo de Sismógrafos cargados:
                var s1 = sismografos.FirstOrDefault(s => s.identificadorSismografo == "SISM-001");
                var s2 = sismografos.FirstOrDefault(s => s.identificadorSismografo == "SISM-002");
                var s3 = sismografos.FirstOrDefault(s => s.identificadorSismografo == "SISM-003");
                var s4 = sismografos.FirstOrDefault(s => s.identificadorSismografo == "SISM-004");

                // Validación de seguridad
                if (s1 == null || s2 == null || s3 == null || s4 == null)
                {
                    Console.WriteLine("Error: no se encontraron los sismógrafos esperados.");
                    return;
                }

                // Crear las estaciones con sus relaciones FK correctas
                var estacion1 = new EstacionSismologica(s1)
                {
                    codigoEstacion = "EST001",
                    nombre = "Estación Córdoba",
                    latitud = -31.4167,
                    longitud = -64.1833,
                    documentoCertificacionAdq = "DOC001",
                    nroCertificacionAdq = "CERT001",
                    fechaCertificacion = new DateTime(2023,06,01),
                    idSismografoFK = s1.idSismografo
                };

                var estacion2 = new EstacionSismologica(s2)
                {
                    codigoEstacion = "EST002",
                    nombre = "Estación Mendoza",
                    latitud = -32.8908,
                    longitud = -68.8272,
                    documentoCertificacionAdq = "DOC002",
                    nroCertificacionAdq = "CERT002",
                    fechaCertificacion = new DateTime(2023,07,01),
                    idSismografoFK = s2.idSismografo
                };

                var estacion3 = new EstacionSismologica(s3)
                {
                    codigoEstacion = "EST003",
                    nombre = "Estación Salta",
                    latitud = -24.7821,
                    longitud = -65.4232,
                    documentoCertificacionAdq = "DOC003",
                    nroCertificacionAdq = "CERT003",
                    fechaCertificacion = new DateTime(2023,08,01),
                    idSismografoFK = s3.idSismografo
                };

                var estacion4 = new EstacionSismologica(s4)
                {
                    codigoEstacion = "EST004",
                    nombre = "Estación Ushuaia",
                    latitud = -54.8019,
                    longitud = -68.3030,
                    documentoCertificacionAdq = "DOC004",
                    nroCertificacionAdq = "CERT004",
                    fechaCertificacion = new DateTime(2023,09,01),
                    idSismografoFK = s4.idSismografo
                };

                // Insertar en base
                estacionDao.Insert(estacion1);
                estacionDao.Insert(estacion2);
                estacionDao.Insert(estacion3);
                estacionDao.Insert(estacion4);

                Console.WriteLine("Estaciones sismológicas insertadas correctamente.");
            }

            var estaciones = estacionDao.GetAll(); // Recargar estaciones con sus IDs

            // BLOQUE DE RECARGA NECESARIO (ANTES DEL PASO 6)
            // Recargamos todas las entidades necesarias para las FKs de las Órdenes
            ordenDao = new OrdenDeInspeccionDao(cs);
            estaciones = estacionDao.GetAll(); // Recarga las estaciones con sus IDs de BD
            empleados = empleadoDao.GetAll();  // Recarga los empleados con sus IDs de BD
            var estadosOI = estadoDao.GetAll().Where(e => e.ambito == "ORDEN").ToList();

            // Obtener las referencias de objetos CON IDS DE LA BD:
            jesus = empleados.FirstOrDefault(e => e.nombre == "Jesus");
            nano = empleados.FirstOrDefault(e => e.nombre == "Nazareno");
            estadoRealizadaDB = estadosOI.FirstOrDefault(e => e.nombreEstado == "REALIZADA");
            estadoCerradaDB = estadosOI.FirstOrDefault(e => e.nombreEstado == "CERRADA");

            // PASO 6: Órdenes de Inspección (FKs a Empleado, Estado, Estación)
            Console.WriteLine($"ordenDao.GetAll().Count = {ordenDao.GetAll().Count}");
            Console.WriteLine($"jesus != null -> {jesus != null}");
            Console.WriteLine($"nano != null -> {nano != null}");
            Console.WriteLine($"estaciones.Count = {estaciones.Count}");


            if (ordenDao.GetAll().Count == 0 && jesus != null && nano != null && estaciones.Count >= 4)
            {
                // Mapeo de Entidades (utilizando los códigos EST00X):
                var e1 = estaciones.FirstOrDefault(e => e.codigoEstacion == "EST001");
                var e2 = estaciones.FirstOrDefault(e => e.codigoEstacion == "EST002");
                var e3 = estaciones.FirstOrDefault(e => e.codigoEstacion == "EST003");
                var e4 = estaciones.FirstOrDefault(e => e.codigoEstacion == "EST004");

                var fechaBase = DateTime.Now;

                // Las 5 órdenes de inspección (usando los objetos cargados)
                var orden1 = new OrdenDeInspeccion(1, fechaBase.AddDays(-20), e1, EstadoFactory.CrearEstadoDesde(estadoRealizadaDB), jesus)
                { fechaHoraFin = fechaBase.AddDays(-15) };
                ordenDao.Insert(orden1);
                Debug.WriteLine($"Insertando orden con ID_ESTADO={orden1.idEstadoFK}");



                var orden2 = new OrdenDeInspeccion(2, fechaBase.AddDays(-15), e2, EstadoFactory.CrearEstadoDesde(estadoCerradaDB), nano)
                { fechaHoraFin = fechaBase.AddDays(-5), fechaHoraCierre = fechaBase.AddDays(-5).AddHours(1), observacionCierre = "Cierre estándar" };
                ordenDao.Insert(orden2);
                Debug.WriteLine($"Insertando orden con ID_ESTADO={orden2.idEstadoFK}");

                var orden3 = new OrdenDeInspeccion(3, fechaBase.AddDays(-30), e3, EstadoFactory.CrearEstadoDesde(estadoRealizadaDB), jesus)
                { fechaHoraFin = fechaBase.AddDays(-7) };
                ordenDao.Insert(orden3);
                Debug.WriteLine($"Insertando orden con ID_ESTADO={orden3.idEstadoFK}");

                var orden4 = new OrdenDeInspeccion(4, fechaBase.AddDays(-10), e4, EstadoFactory.CrearEstadoDesde(estadoRealizadaDB), jesus)
                { fechaHoraFin = fechaBase.AddDays(-12) };
                ordenDao.Insert(orden4);
                Debug.WriteLine($"Insertando orden con ID_ESTADO={orden4.idEstadoFK}");

                var orden5 = new OrdenDeInspeccion(5, fechaBase.AddDays(-25), e3, EstadoFactory.CrearEstadoDesde(estadoCerradaDB), jesus)
                { fechaHoraFin = fechaBase.AddDays(-14), fechaHoraCierre = fechaBase.AddDays(-14).AddHours(1), observacionCierre = "Cierre por acceso" };
                ordenDao.Insert(orden5);
                Debug.WriteLine($"Insertando orden con ID_ESTADO={orden5.idEstadoFK}");

                Console.WriteLine("Datos iniciales de Órdenes de Inspección cargados correctamente.");
            }
        }
    }
}
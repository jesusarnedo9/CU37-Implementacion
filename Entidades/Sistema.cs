using ImplementacionCU37.Dao;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Linq;

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

        private readonly string _connectionString;

        public Sistema()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
                throw new Exception("Error: No se encontró la cadena de conexión 'DefaultConnection' en App.config. Verifique la configuración.");

            var motivoTipoDao = new MotivoTipoDao(_connectionString);
            var estadoDao = new EstadoDao(_connectionString);
            var empleadoDao = new EmpleadoDao(_connectionString);
            var ordenDao = new OrdenDeInspeccionDao(_connectionString);
            var usuarioDao = new UsuariosDao(_connectionString);

            MotivoTipos = motivoTipoDao.GetAll();
            EstadosDisponibles = estadoDao.GetAll();
            Empleados = empleadoDao.GetAll();
            Ordenes = ordenDao.GetAll();

            var usuarioJesus = usuarioDao.GetById("jesus");

            if (usuarioJesus != null)
            {
                SesionActiva = new Sesion(usuarioJesus);
                ResponsableLogueado = SesionActiva.getEmpleado();
            }
            else
            {
                Console.WriteLine("Advertencia: No se pudo cargar el usuario de inicio de sesión 'jesus'.");
            }
        }

        public void RecargarSesionPorDefecto()
        {
            try
            {
                var usuarioDao = new UsuariosDao(_connectionString);
                var usuarioJesus = usuarioDao.GetById("jesus");

                if (usuarioJesus != null)
                {
                    SesionActiva = new Sesion(usuarioJesus);
                    ResponsableLogueado = SesionActiva.getEmpleado();
                }
                else
                {
                    Console.WriteLine("Advertencia: No se pudo recargar el usuario de inicio de sesión 'jesus' tras el seed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recargar sesión por defecto: " + ex.Message);
            }
        }
    }
}
namespace ImplementacionCU37.Entidades
{
    public class Usuario
    {
        public string nombreUsuario { get; set; }
        public Empleado empleado { get; set; }

        public Usuario(string nombreUsuario, Empleado empleado)
        {
            this.nombreUsuario = nombreUsuario;
            this.empleado = empleado;
        }

        public Empleado getEmpleado()
        {
            return empleado;
        }

        public bool esResponsableDeReparacion()
        {
            return empleado != null && empleado.esResponsableReparacion();
        }
    }
}

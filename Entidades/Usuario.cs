namespace ImplementacionCU37.Entidades
{
    public class Usuario
    {
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public Empleado empleado { get; set; }
        public int idEmpleadoFK => empleado?.idEmpleado ?? 0;

        public Usuario() { }

        public Usuario(string nombreUsuario, Empleado empleado)
        {
            this.nombreUsuario = nombreUsuario;
            this.empleado = empleado;
        }

        public Usuario(string nombreUsuario, Empleado empleado, string contrasena)
            : this(nombreUsuario, empleado)
        {
            this.contrasena = contrasena;
        }
        public Empleado getRIlogueado()
        {
            if (empleado != null && empleado.esResponsableReparacion())
            {
                return empleado;
            }
            return null;
        }
    }
}
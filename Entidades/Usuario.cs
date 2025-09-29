namespace ImplementacionCU37.Entidades
{
    public class Usuario
    {
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public Empleado empleado { get; set; }

        public Usuario(string nombreUsuario, Empleado empleado)
        {
            this.nombreUsuario = nombreUsuario;
            this.empleado = empleado;
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

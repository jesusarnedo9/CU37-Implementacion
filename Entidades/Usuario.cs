namespace ImplementacionCU37.Entidades
{
    public class Usuario
    {
        //Atributos
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public Empleado empleado { get; set; }

        //Constructor
        public Usuario(string nombreUsuario, Empleado empleado)
        {
            this.nombreUsuario = nombreUsuario;
            this.empleado = empleado;
        }

        //Metodos
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

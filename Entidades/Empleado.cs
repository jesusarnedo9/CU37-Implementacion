namespace ImplementacionCU37.Entidades
{
    public class Empleado
    {
        public Rol rol { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public int idEmpleado { get; set; }
        public Empleado() { }

        public Empleado(string nombre, string apellido, string mail, string telefono, int id, Rol rol)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = mail;
            this.telefono = telefono;
            this.idEmpleado = id;
            this.rol = rol;
        }

        public bool esResponsableReparacion()
        {
            
            return rol != null && rol.esResponsableDeReparacion();
        }
        public string obtenerEmail()
        {
            return email;
        }
    }
}
namespace ImplementacionCU37.Entidades
{
    public class Rol
    {
        public string descripcionRol { get; set; }
        public string nombre { get; set; }

        // Roles predefinidos
        public static readonly Rol RESPONSABLE_REPARACION = new Rol("Responsable de Reparación", "Encargado de gestionar reparaciones");
        public static readonly Rol ADMINISTRADOR_REPARACION = new Rol("Administrador", "Gestiona el sistema");


        //Constructor
        public Rol(string nombre, string descripcionRol)
        {
            this.nombre = nombre;
            this.descripcionRol = descripcionRol;
        }

        //Metodos
        public bool getNombre()
        {
            return this.nombre == RESPONSABLE_REPARACION.nombre;
        }
    }
}

namespace ImplementacionCU37.Entidades
{
    public class Rol
    {
        public int idRol { get; set; }
        public string descripcionRol { get; set; }
        public string nombre { get; set; }
        public Rol() { }

        public Rol(string nombre, string descripcionRol)
        {
            this.nombre = nombre;
            this.descripcionRol = descripcionRol;
        }

        public const string ADMINISTRADOR_REPARACION = "Administrador de Reparación";
        public const string RESPONSABLE_REPARACION = "Responsable de Reparación";

        public bool esResponsableDeReparacion() => this.nombre == RESPONSABLE_REPARACION;

        public override string ToString()
        {
            return nombre;
        }
    }
}
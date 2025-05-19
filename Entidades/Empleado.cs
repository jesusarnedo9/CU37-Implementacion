using System;

namespace ImplementacionCU37.Entidades
{
    public class Empleado
    {
        public string apellido { get; set; }
        public string mail { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public int id { get; set; }

        public Rol rol;

        public Empleado(string nombre, string apellido, string mail, string telefono, int id, Rol rol)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.mail = mail;
            this.telefono = telefono;
            this.id = id;   
            this.rol = rol;
        }



        public bool esResponsableReparacion()
        {
            return rol != null && rol.esResponsable();
        }

        public string obtenerEmail()
        {
            return mail;
        }
    }
}
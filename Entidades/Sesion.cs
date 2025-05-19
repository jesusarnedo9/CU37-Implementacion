using System;

namespace ImplementacionCU37.Entidades
{
    public class Sesion
    {
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }

        // Relaciones
        public Usuario usuario { get; set; }
        public Estado estado { get; set; } // Relación agregada con Estado

        // Métodos
        public Empleado getUsuario()
        {
            return usuario.getEmpleado();
        }

        public string getRILogueado()
        {
            // Corrige el error CS1513 y CS0120
            if (usuario.empleado.esResponsableReparacion())
            {
                return usuario.empleado.nombre; // Se accede correctamente a la propiedad 'nombre' de la instancia de 'Empleado'
            }
            return null; // Devuelve null si no es responsable de reparación
        }

        public Empleado getResponsable()
        {
            if (usuario.empleado.rol.esResponsable())
                return usuario.empleado;

            return null;
        }
    }
}

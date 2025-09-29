using System;

namespace ImplementacionCU37.Entidades
{
    public class Sesion
    {
        public DateTime fechaHoraInicio { get; set; }
        private Usuario usuarioLogueado;

        public Sesion(Usuario usuario)
        {
            usuarioLogueado = usuario;
        }

        public Empleado getEmpleado()
        {
            return usuarioLogueado.getRIlogueado();
        }
    }
}


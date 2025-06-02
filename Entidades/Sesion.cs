using System;

namespace ImplementacionCU37.Entidades
{
    public class Sesion
    {
        //Atributos
        public DateTime fechaHoraInicio { get; set; }
        private Usuario usuarioLogueado;

        public Sesion(Usuario usuario)
        {
            usuarioLogueado = usuario;
        }

        //Metodos
        public Empleado getEmpleado()
        {
            return usuarioLogueado.getRIlogueado();
        }
    }
}


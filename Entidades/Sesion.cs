using System;

namespace ImplementacionCU37.Entidades
{
    public class Sesion
    {
        //Atributos
        //public DateTime fechaHoraInicio { get; set; }
        private Usuario usuarioLogueado;

        //Constructor
        public Sesion(Usuario usuario)
        {
            usuarioLogueado = usuario;
        }

        //Metodos
        public Usuario getUsuario()
        {
            return usuarioLogueado;
        }
    }
}

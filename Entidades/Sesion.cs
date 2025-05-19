using System;

namespace ImplementacionCU37.Entidades
{
    public class Sesion
    {
        private Usuario usuarioLogueado;

        public Sesion(Usuario usuario)
        {
            usuarioLogueado = usuario;
        }

        public Usuario getUsuario()
        {
            return usuarioLogueado;
        }
    }
}

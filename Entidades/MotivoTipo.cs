using System.Collections.Generic;

namespace ImplementacionCU37.Entidades
{
    public class MotivoTipo
    {
        public string descripcion;
        public MotivoTipo(string descripcion)
        {
            this.descripcion = descripcion;
        }
        public string getDescripciones()
        {
            return descripcion;
        }
        public override string ToString()
        {
            return this.descripcion;
        }

    }

}

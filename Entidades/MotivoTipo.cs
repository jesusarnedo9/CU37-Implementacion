using System.Collections.Generic;

namespace ImplementacionCU37.Entidades
{
    public class MotivoTipo
    {
        public string descripcion { get; set; }
        public MotivoTipo(string descripcion)
        {
            this.descripcion = descripcion;
        }

        public static List<string> getDescripciones(List<MotivoTipo> motivos)
        {
            List<string> descripciones = new List<string>();
            foreach (MotivoTipo motivo in motivos)
            {
                descripciones.Add(motivo.descripcion);
            }
            return descripciones;
        }

        public override string ToString()
        {
            return this.descripcion;
        }

    }

}

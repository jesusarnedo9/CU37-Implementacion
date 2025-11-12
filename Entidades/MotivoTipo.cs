namespace ImplementacionCU37.Entidades
{
    public class MotivoTipo
    {
        public int idMotivoTipo { get; set; }
        public string descripcion;
        public MotivoTipo(string descripcion)
        {
            this.descripcion = descripcion;
        }

        public MotivoTipo() { }

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
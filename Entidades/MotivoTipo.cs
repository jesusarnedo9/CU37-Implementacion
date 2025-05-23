namespace ImplementacionCU37.Entidades
{
    public class MotivoTipo
    {
        public string descripcion { get; set; }

        public MotivoTipo() { }

        public MotivoTipo(string descripcion)
        {
            this.descripcion = descripcion;
        }

        public override string ToString()
        {
            return this.descripcion; // o como se llame tu propiedad con la descripción
        }

    }

}

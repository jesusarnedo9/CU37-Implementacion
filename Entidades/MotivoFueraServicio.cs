namespace ImplementacionCU37.Entidades
{
    public class MotivoFueraServicio
    {
        // Atributos
        public string comentario { get; set; }

        private MotivoTipo tipo;

        public MotivoFueraServicio(string comentario)
        {
            this.comentario = comentario;
        }

        public void setMotivoTipo(MotivoTipo tipo)
        {
            this.tipo = tipo;
        }

        public MotivoTipo getMotivoTipo()
        {
            return tipo;
        }
        public override string ToString()
        {
            return this.comentario; 
        }
    }


}

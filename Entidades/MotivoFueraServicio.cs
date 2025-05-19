namespace ImplementacionCU37.Entidades
{
    public class MotivoFueraServicio
    {
        public string comentario { get; set; }

        // Relación 1 a 1 con MotivoTipo
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
    }
}

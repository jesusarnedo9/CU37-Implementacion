namespace ImplementacionCU37.Entidades
{
   public class MotivoFueraServicio
    {
        public string comentario { get; set; }
        public MotivoTipo tipo;

        public MotivoFueraServicio(MotivoTipo tipo, string comentario)
        {
            this.tipo = tipo;
            this.comentario = comentario;
        }

        public MotivoFueraServicio()
        {
        }

        public MotivoTipo getMotivoTipo()
        {
            return tipo;
        }

        public override string ToString()
        {
            return $"{tipo.descripcion}: {comentario}";
        }
    }
}

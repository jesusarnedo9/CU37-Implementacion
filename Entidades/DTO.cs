namespace ImplementacionCU37.Entidades
{
    public class OrdenInspeccionDTO
    {
        public string Id { get; set; }
        public string Texto { get; set; }
    }
    public class MotivoFueraServicioDTO
    {
        public MotivoTipo Motivo { get; set; }
        public string Comentario { get; set; }
    }
}

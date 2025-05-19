namespace ImplementacionCU37.Entidades
{
    public class Estado
    {
        // Atributos
        public string ambito { get; set; }
        public string nombreEstado { get; set; }

        // Constantes internas
        public const string ESTADO_REALIZADA = "Realizada";
        public const string ESTADO_CERRADA = "Cerrada";
        public const string ESTADO_FUERA_SERVICIO = "Fuera de Servicio";
        public const string ESTADO_REALIZADO = "Realizado";
        public const string AMBITO_OI = "OrdenInspeccion";
        public const string AMBITO_SISMOGRAFO = "Sismografo";

        // Métodos
        public bool esAmbitoOI()
        {
            return ambito == AMBITO_OI;
        }

        public bool esAmbitoSismografo()
        {
            return ambito == AMBITO_SISMOGRAFO;
        }

        public bool esRealizado()
        {
            return nombreEstado == ESTADO_REALIZADO;
        }

        public bool estaRealizada()
        {
            return nombreEstado == ESTADO_REALIZADA;
        }

        public bool esFueraServicio()
        {
            return nombreEstado == ESTADO_FUERA_SERVICIO;
        }

        public bool esCerrada()
        {
            return nombreEstado == ESTADO_CERRADA;
        }
    }
}

namespace ImplementacionCU37.Entidades
{
    public class Estado
    {
        // Atributos
        public string ambito { get; set; }
        public string nombreEstado { get; set; }

        // Constantes internas
        public const string ESTADO_REALIZADA_OI = "Realizada";
        public const string ESTADO_CERRADA_OI = "Cerrada";
        public const string ESTADO_FUERA_SERVICIO_S = "Fuera de Servicio";
        public const string ESTADO_REALIZADO_S = "Realizado";
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
            return nombreEstado == ESTADO_REALIZADO_S;
        }

        public bool estaRealizada()
        {
            return nombreEstado == ESTADO_REALIZADA_OI;
        }

        public bool esFueraServicio()
        {
            return nombreEstado == ESTADO_FUERA_SERVICIO_S;
        }

        public bool esCerrada()
        {
            return nombreEstado == ESTADO_CERRADA_OI;
        }
    }
}

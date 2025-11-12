using System;

namespace ImplementacionCU37.Entidades
{
    public class Estado
    {
        public int idEstado { get; set; }
        public string ambito { get; set; }
        public string nombreEstado { get; set; }

        public const string ESTADO_REALIZADA_OI = "REALIZADA";
        public const string ESTADO_CERRADA_OI = "CERRADA";
        public const string ESTADO_FUERA_SERVICIO_S = "FUERA_SERVICIO";
        public const string ESTADO_REALIZADO_S = "ACTIVO";

        public const string AMBITO_OI = "ORDEN";
        public const string AMBITO_SISMOGRAFO = "SISMOGRAFO";


        public bool esAmbitoOI()
        {
            return ambito == AMBITO_OI;
        }
        public bool esAmbitoSismografo()
        {
            return ambito == AMBITO_SISMOGRAFO;
        }

        public bool estaRealizada()
        {
            return nombreEstado.Equals(ESTADO_REALIZADA_OI, StringComparison.OrdinalIgnoreCase);
        }
        public bool esFueraServicio()
        {
            return nombreEstado.Equals(ESTADO_FUERA_SERVICIO_S, StringComparison.OrdinalIgnoreCase);
        }
        public bool esCerrada()
        {
            return nombreEstado.Equals(ESTADO_CERRADA_OI, StringComparison.OrdinalIgnoreCase);
        }
    }
}

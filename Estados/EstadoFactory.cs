using System;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public static class EstadoFactory
    {
        public static IEstado CrearEstadoDesde(Estado entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            if (!entidad.esAmbitoOI())
                throw new InvalidOperationException($"El estado '{entidad.nombreEstado}' no pertenece a OrdenDeInspeccion.");

            IEstado s;
            switch (entidad.nombreEstado?.ToUpperInvariant())
            {
                case "REALIZADA":
                    s = new Realizada();
                    break;
                case "CERRADA":
                    s = new Cerrada();
                    break;
                default:
                    throw new InvalidOperationException($"Estado '{entidad.nombreEstado}' no implementado en EstadoFactory.");
            }
            s.idEstado = entidad.idEstado; //asigo a bd
            return s;

        }
    }
}

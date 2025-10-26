using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementacionCU37.Estados
{
    using ImplementacionCU37.Estados;
    using ImplementacionCU37.Entidades;

    public static class EstadoFactory
    {
        public static IEstado CrearEstadoDesde(Estado estadoViejo)
        {
            if (estadoViejo == null)
                throw new ArgumentNullException(nameof(estadoViejo));

            if (estadoViejo.esAmbitoOI())
            {
                if (estadoViejo.estaRealizada())
                    return new Realizada();
                if (estadoViejo.esCerrada())
                    return new Cerrada();
            }

            // Si el estado pertenece a otro ámbito, no se convierte
            throw new InvalidOperationException(
                $"El estado '{estadoViejo.nombreEstado}' no pertenece a OrdenDeInspeccion o no está implementado.");
        }
    }

}

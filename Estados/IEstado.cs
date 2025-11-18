using System;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public interface IEstado
    {
        int idEstado { get; set; }
        String nombre { get; }

        // Aplicable sólo al ámbito Orden (State pattern se usa para Orden)
        bool esAmbitoOI();
        bool estaRealizada();
        bool esCerrada();
        void CerrarOrden(OrdenDeInspeccion orden, DateTime fechaHoraActual);
    }
}
using System;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public interface IEstado
    {
        int idEstado { get; set; }
        String nombre { get; }
        bool esAmbitoOI();
        bool esAmbitoSismografo();
        bool estaRealizada();
        bool esFueraServicio();
        bool esCerrada();

        IEstado CerrarOrden(OrdenDeInspeccion orden, DateTime fechaHoraActual);
    }
}
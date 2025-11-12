using System;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public class Realizada : IEstado
    {
        public int idEstado { get; set; }
        public String nombre => "Realizada";

        public bool esAmbitoOI()
        {
            return true;
        }
        public bool esAmbitoSismografo()
        {
            return false;
        }
        public bool estaRealizada()
        {
            return true;
        }
        public bool esFueraServicio()
        {
            return false;
        }
        public bool esCerrada()
        {
            return false;
        }
        public IEstado CerrarOrden(Entidades.OrdenDeInspeccion orden, DateTime fechaHoraActual)
        {
            if (orden == null) throw new ArgumentNullException(nameof(orden));
            orden.setFechaHoraCierre(fechaHoraActual);
            return new Cerrada();
        }
    }
}
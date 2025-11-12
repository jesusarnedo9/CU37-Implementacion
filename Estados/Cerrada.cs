using System;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public class Cerrada : IEstado
    {
        public int idEstado { get; set; }
        public String nombre => "Cerrada";
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
            return false;
        }
        public bool esFueraServicio()
        {
            return false;
        }
        public bool esCerrada()
        {
            return true;
        }
        public IEstado CerrarOrden(Entidades.OrdenDeInspeccion orden, DateTime fechaHoraActual)
        {
            throw new InvalidOperationException("La orden de inspección ya está cerrada.");
        }
    }
}
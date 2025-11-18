using System;
using ImplementacionCU37.Entidades;


namespace ImplementacionCU37.Estados
{
    public class ParcialmenteRealizada : IEstado
    {
        public int idEstado { get; set; }
        public String nombre => "ParcialmenteRealizada";
        public bool esAmbitoOI()
        {
            return true;
        }
        public bool estaRealizada()
        {
            return false;
        }
        public bool esCerrada()
        {
            return false;
        }
        public void CerrarOrden(OrdenDeInspeccion orden, DateTime fechaHoraActual)
        {
            throw new InvalidOperationException("Operacion invalida.");
        }
    }
}

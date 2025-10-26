using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public class Cerrada : IEstado
    {
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
        public void CerrarOrden(Entidades.OrdenDeInspeccion orden, DateTime fechaHoraActual)
        {
            // La orden ya está cerrada, no se realiza ninguna acción.
            throw new InvalidOperationException("La orden de inspección ya está cerrada.");
        }
    }
}

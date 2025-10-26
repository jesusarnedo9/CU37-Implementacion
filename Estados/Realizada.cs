using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementacionCU37.Entidades;

namespace ImplementacionCU37.Estados
{
    public class Realizada : IEstado
    {
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
        public void CerrarOrden(Entidades.OrdenDeInspeccion orden, DateTime fechaHoraActual)
        {
            // Lógica para cerrar la orden de inspección
            orden.setEstado(new Cerrada());
            orden.setFechaHoraCierre(fechaHoraActual);
            
        }
    }
}

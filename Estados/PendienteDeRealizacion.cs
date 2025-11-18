using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementacionCU37.Entidades;


namespace ImplementacionCU37.Estados
{
    public class PendienteDeRealizacion : IEstado
    {
        public int idEstado { get; set; }
        public String nombre => "PendienteDeRealizacion";
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

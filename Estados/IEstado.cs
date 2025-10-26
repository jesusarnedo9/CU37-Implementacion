using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImplementacionCU37.Entidades
{
    public interface IEstado
    {
       String nombre { get; }
        bool esAmbitoOI();
        bool esAmbitoSismografo();
        bool estaRealizada();
        bool esFueraServicio();
        bool esCerrada();

        void CerrarOrden(OrdenDeInspeccion orden, DateTime fechaHoraActual);
    }
}

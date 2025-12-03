using ImplementacionCU37.Entidades;
using System;
using ImplementacionCU37.Dao;

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
        public bool estaRealizada()
        {
            return true;
        }
        public bool esCerrada()
        {
            return false;
        }
        public void CerrarOrden(OrdenDeInspeccion orden, DateTime fechaHoraActual)
        {
            Estado estadoDB = EstadoDao.GetByNombre("CERRADA");
           
            IEstado cerrada = EstadoFactory.CrearEstadoDesde(estadoDB);

            orden.setFechaHoraCierre(fechaHoraActual);
            orden.setEstado(cerrada);
            orden.idEstadoFK = cerrada.idEstado;//Sincronizo id para la bd
        }
        public IEstado crearEstado()
        {
            return new Cerrada();
        }
    }
}
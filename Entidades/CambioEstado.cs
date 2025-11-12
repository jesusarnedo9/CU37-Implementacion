using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;

namespace ImplementacionCU37.Entidades
{
    public class CambioEstado
    {

        public int idCambioEstado { get; set; } 
        public int idEmpleadoFK { get; set; }  
        public int idSismografoFK { get; set; } 
        public DateTime fechaHoraInicio { get; private set; }
        public DateTime? fechaHoraFin { get; private set; }
        public Empleado responsableLogueado { get; set; }

        private Estado estado;
        private MotivoFueraServicio motivo;
        public List<MotivoFueraServicio> motivos;

        public CambioEstado()
        {
            this.motivos = new List<MotivoFueraServicio>();
        }

        public CambioEstado(DateTime inicio)
        {
            this.fechaHoraInicio = inicio;
            this.motivos = new List<MotivoFueraServicio>();
        }

        public MotivoFueraServicio getMotivo()
        {
            return motivo;
        }

        public bool esActual()
        {
            return fechaHoraFin == null;
        }

        public void finalizar()
        {
            fechaHoraFin = DateTime.Now;
        }

        public IReadOnlyList<MotivoFueraServicio> Motivos => motivos.AsReadOnly();

        public void AgregarMotivo(MotivoFueraServicio motivo)
        {
            motivos.Add(motivo);
        }
        public void setRILogueado(Empleado responsable)
        {
            this.responsableLogueado = responsable;
            this.idEmpleadoFK = responsable.idEmpleado;
        }

        public static CambioEstado crear(List<MotivoFueraServicioDTO> motivos, Empleado responsableLogueado)
        {
            var nuevoCambio = new CambioEstado(DateTime.Now);

            nuevoCambio.setFechaHoraCierre(DateTime.Now);
            nuevoCambio.setRILogueado(responsableLogueado);

            foreach (var dto in motivos)
            {
                var motivo = new MotivoFueraServicio(dto.Motivo, dto.Comentario);
                nuevoCambio.AgregarMotivo(motivo);
            }
            return nuevoCambio;
        }
        public DateTime getFechaHoraInicio() => fechaHoraInicio;

        public void setFechaHoraCierre(DateTime cierre) => fechaHoraFin = cierre;
    }
}
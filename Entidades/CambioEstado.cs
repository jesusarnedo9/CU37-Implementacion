using ImplementacionCU37.Entidades;
using System;

public class CambioEstado
{
    public DateTime fechaHoraInicio { get; set; }
    public DateTime? fechaHoraFin { get; set; }

    private MotivoFueraServicio motivo;
    private Empleado responsableLogueado;


    public CambioEstado(DateTime inicio, MotivoFueraServicio motivo = null)
    {
        this.fechaHoraInicio = inicio;
        this.motivo = motivo;
    }

    public bool esActual() 
    {
        return fechaHoraFin == null;
    }

    public void finalizar()
    {
        fechaHoraFin = DateTime.Now;
    }

    public void setMotivosSeleccionado(MotivoFueraServicio motivoSeleccionado)
    {
        this.motivo = motivoSeleccionado;
    }

    public void setFechaHoraCierre(DateTime cierre)
    {
        this.fechaHoraFin = cierre;
    }

    public void setRILogueado(Empleado responsable)
    {
        this.responsableLogueado = responsable;
    }
}

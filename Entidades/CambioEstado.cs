using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;

public class CambioEstado
{
    public DateTime fechaHoraInicio { get; set; }
    public DateTime? fechaHoraFin { get; set; }

    public MotivoFueraServicio motivo;
    public List<MotivoFueraServicio> motivos;
    private Empleado responsableLogueado;


    public CambioEstado(DateTime inicio, List<MotivoFueraServicio> motivos)
    {
        this.fechaHoraInicio = inicio;
        this.motivos = motivos;
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

    public void setMotivosSeleccionado(MotivoFueraServicio motivoSeleccionado)
    {
        this.motivo = motivoSeleccionado;
    }

    public void setFechaHoraCierre(DateTime cierre)
    {
        this.fechaHoraFin = cierre;
    }
    public DateTime? getFechaHoraFin()
    {
        return fechaHoraFin;
    }
    public void setRILogueado(Empleado responsable)
    {
        this.responsableLogueado = responsable;
    }
}

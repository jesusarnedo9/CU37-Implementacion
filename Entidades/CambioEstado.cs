using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

public class CambioEstado
{
    private DateTime fechaHoraInicio;
    private DateTime? fechaHoraFin;
    public Estado estado { get; set; }
    public MotivoFueraServicio motivo;
    public List<MotivoFueraServicio> motivos;
    private Empleado responsableLogueado;

    public CambioEstado(DateTime inicio, List<MotivoFueraServicio> motivos)
    {
        inicio = getFechaHoraInicio();
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
    public void setRILogueado(Empleado responsable)
    {
        this.responsableLogueado = responsable;
    }
    public DateTime getFechaHoraInicio() => fechaHoraInicio;
    public void setFechaHoraInicio(DateTime inicio) => fechaHoraInicio = inicio;
    public DateTime? getFechaHoraFin() => fechaHoraFin;
    public void setFechaHoraCierre(DateTime cierre) => fechaHoraFin = cierre;

    public static CambioEstado crear(List<MotivoFueraServicio> motivos, Empleado responsableLogueado)
    {
        var nuevoCambio = new CambioEstado(DateTime.Now, motivos);
        nuevoCambio.motivo = motivos.FirstOrDefault();
        nuevoCambio.responsableLogueado = responsableLogueado;
        if (motivos.Any())
        {
            var motivoNuevo = new MotivoFueraServicio(motivos.First().tipo, "Motivo creado automáticamente");
        }
        return nuevoCambio;
    }
}


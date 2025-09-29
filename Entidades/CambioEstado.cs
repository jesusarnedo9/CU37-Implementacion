using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;

public class CambioEstado
{
    private DateTime fechaHoraInicio;
    private DateTime? fechaHoraFin;
    private Estado estado;
    private MotivoFueraServicio motivo;
    public List<MotivoFueraServicio> motivos;
    private Empleado responsableLogueado;


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
    }
    public DateTime getFechaHoraInicio() => fechaHoraInicio;

    public void setFechaHoraCierre(DateTime cierre) => fechaHoraFin = cierre;

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
}
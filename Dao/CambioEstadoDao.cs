using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ImplementacionCU37.Dao
{
    public class CambioEstadoDao
    {
        private readonly string _connectionString;
        private readonly EmpleadoDao _empleadoDao;

        public CambioEstadoDao(string connectionString)
        {
            _connectionString = connectionString;
            _empleadoDao = new EmpleadoDao(connectionString);
        }

        public List<CambioEstado> GetBySismografoId(int idSismografo)
        {
            var cambios = new List<CambioEstado>();

            var sql = "SELECT ID_CAMBIO_ESTADO, FECHA_HORA_INICIO, FECHA_HORA_FIN, ID_EMPLEADO, ID_SISMOGRAFO " +
                      "FROM CAMBIO_ESTADO WHERE ID_SISMOGRAFO = @idSismografo ORDER BY FECHA_HORA_INICIO DESC";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@idSismografo", idSismografo);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cambio = new CambioEstado(reader.GetDateTime(1)) // Usa el constructor con fecha de inicio
                        {
                            idCambioEstado = reader.GetInt32(0),
                            idEmpleadoFK = reader.GetInt32(3),
                            idSismografoFK = reader.GetInt32(4)
                        };

                        // Resuelve la FechaHoraFin
                        if (!reader.IsDBNull(2))
                        {
                            cambio.setFechaHoraCierre(reader.GetDateTime(2));
                        }

                        // Si quisieras el objeto Empleado completo:
                        // cambio.setRILogueado(_empleadoDao.GetById(cambio.idEmpleadoFK));

                        cambios.Add(cambio);
                    }
                }
            }
            return cambios;
        }

        public int Insert(CambioEstado cambio, int idSismografo)
        {
            var sql = "INSERT INTO CAMBIO_ESTADO (FECHA_HORA_INICIO, FECHA_HORA_FIN, ID_EMPLEADO, ID_SISMOGRAFO) " +
                      "OUTPUT INSERTED.ID_CAMBIO_ESTADO " +
                      "VALUES (@inicio, @fin, @empId, @sismId);";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                // Usa las propiedades corregidas: fechaHoraInicio es ahora pública.
                cmd.Parameters.AddWithValue("@inicio", cambio.fechaHoraInicio);

                // Si fechaHoraFin es nulo, pasamos DBNull
                object finParam = cambio.fechaHoraFin.HasValue ? (object)cambio.fechaHoraFin.Value : DBNull.Value;
                cmd.Parameters.AddWithValue("@fin", finParam);

                // Protección: responsableLogueado puede ser nulo, usamos idEmpleado si está set
                int empId = cambio.responsableLogueado != null ? cambio.responsableLogueado.idEmpleado : cambio.idEmpleadoFK;
                if (empId <= 0)
                    throw new InvalidOperationException("No se puede insertar CambioEstado: idEmpleado inválido.");

                cmd.Parameters.AddWithValue("@empId", empId);

                cmd.Parameters.AddWithValue("@sismId", idSismografo);

                conn.Open();
                var result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    throw new InvalidOperationException("No se pudo obtener el ID generado al insertar CambioEstado (resultado nulo).");
                }

                try
                {
                    return Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("No se pudo convertir el ID generado del CambioEstado a int.", ex);
                }
            }
        }
    }
}
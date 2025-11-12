using ImplementacionCU37.Entidades;
using System;
using System.Data.SqlClient;

namespace ImplementacionCU37.Dao
{
    internal class MotivoFueraServicioDao
    {
        private readonly string _connectionString;
        public MotivoFueraServicioDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(MotivoFueraServicio motivo, int cambioEstadoId)
        {
            if (motivo == null) return;

            var tipo = motivo.getMotivoTipo();
            if (tipo == null || tipo.idMotivoTipo <= 0)
                throw new InvalidOperationException("No se puede insertar MotivoFueraServicio: MotivoTipo inválido (idMotivoTipo no presente).");

            var sql = "INSERT INTO MOTIVO_FUERA_SERVICIO (COMENTARIO, ID_MOTIVO_TIPO, ID_CAMBIO_ESTADO) VALUES (@comentario, @idMotivoTipo, @idCambio)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@comentario", motivo.comentario ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@idMotivoTipo", tipo.idMotivoTipo);
                cmd.Parameters.AddWithValue("@idCambio", cambioEstadoId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
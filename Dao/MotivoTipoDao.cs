using ImplementacionCU37.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ImplementacionCU37.Dao
{
    public class MotivoTipoDao
    {
        private readonly string _connectionString;
        public MotivoTipoDao(string connectionString)
        {
            _connectionString = connectionString;     
        }
        public List<MotivoTipo> GetAll()
        {
            var motivos = new List<MotivoTipo>();
            var sql = "SELECT ID_MOTIVO_TIPO, DESCRIPCION FROM MOTIVO_TIPO";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var motivo = new MotivoTipo(reader["DESCRIPCION"].ToString())
                        {
                            idMotivoTipo = (int)reader["ID_MOTIVO_TIPO"]
                        };
                        motivos.Add(motivo);
                    }
                }
            }
            return motivos;
        }

        public void Insert(MotivoTipo motivo)
        {
            var sql = "INSERT INTO MOTIVO_TIPO (DESCRIPCION) VALUES (@descripcion)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@descripcion", motivo.descripcion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
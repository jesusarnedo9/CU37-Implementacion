using ImplementacionCU37.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ImplementacionCU37.Dao
{
    public class EstadoDao
    {
        private readonly string _connectionString;
        public EstadoDao(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Estado> GetAll()
        {
            var estados = new List<Estado>();
            var sql = "SELECT ID_ESTADO, AMBITO, NOMBRE_ESTADO FROM ESTADO";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var estado = new Estado
                        {
                            idEstado = (int)reader["ID_ESTADO"],
                            ambito = reader["AMBITO"].ToString(),
                            nombreEstado = reader["NOMBRE_ESTADO"].ToString()
                        };
                        estados.Add(estado);
                    }
                }
            }
            return estados;
        }

        public void Insert(Estado estado)
        {
            var sql = "INSERT INTO ESTADO (AMBITO, NOMBRE_ESTADO) VALUES (@ambito, @nombreEstado)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ambito", estado.ambito);
                cmd.Parameters.AddWithValue("@nombreEstado", estado.nombreEstado);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Estado GetById(int idEstado)
        {
            var sql = "SELECT ID_ESTADO, AMBITO, NOMBRE_ESTADO FROM ESTADO WHERE ID_ESTADO = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idEstado);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Estado
                        {
                            idEstado = (int)reader["ID_ESTADO"],
                            ambito = reader["AMBITO"].ToString(),
                            nombreEstado = reader["NOMBRE_ESTADO"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
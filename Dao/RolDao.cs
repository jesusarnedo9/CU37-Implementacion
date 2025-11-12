using ImplementacionCU37.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ImplementacionCU37.Dao
{
    public class RolDao
    {
        private readonly string _connectionString;
        public RolDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Rol> GetAll()
        {
            var listaRoles = new List<Rol>();
            var sql = "SELECT ID_ROL, NOMBRE, DESCRIPCION_ROL FROM ROL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rol = new Rol(
                            nombre: reader["NOMBRE"].ToString(),
                            descripcionRol: reader["DESCRIPCION_ROL"].ToString()
                        )
                        {
                            idRol = (int)reader["ID_ROL"]
                        };
                        listaRoles.Add(rol);
                    }
                }
            }
            return listaRoles;
        }
        public Rol GetById(int idRol)
        {
            var sql = "SELECT ID_ROL, NOMBRE, DESCRIPCION_ROL FROM ROL WHERE ID_ROL = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idRol);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Rol(
                            nombre: reader["NOMBRE"].ToString(),
                            descripcionRol: reader["DESCRIPCION_ROL"].ToString()
                        )
                        {
                            idRol = (int)reader["ID_ROL"]
                        };
                    }
                }
            }
            return null;
        }
        public void Insert(Rol rol)
        {
            var sql = "INSERT INTO ROL (NOMBRE, DESCRIPCION_ROL) VALUES (@nombre, @descripcion)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", rol.nombre);
                cmd.Parameters.AddWithValue("@descripcion", rol.descripcionRol);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ImplementacionCU37.Dao
{
    public class UsuariosDao
    {
        private readonly string _connectionString;
        private readonly EmpleadoDao _empleadoDao;
        public UsuariosDao(string connectionString)
        {
            _connectionString = connectionString;
            _empleadoDao = new EmpleadoDao(connectionString);
        }

        public List<Usuario> GetAll()
        {
            var listaUsuarios = new List<Usuario>();
            var sql = "SELECT NOMBRE_USUARIO FROM USUARIO";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaUsuarios.Add(new Usuario(reader["NOMBRE_USUARIO"].ToString(), null));
                    }
                }
            }
            return listaUsuarios;
        }
        public Usuario GetById(string nombreUsuario)
        {
            Usuario usuario = null;
            var sql = "SELECT NOMBRE_USUARIO, CONTRASENA, ID_EMPLEADO FROM USUARIO WHERE NOMBRE_USUARIO = @nombre";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idEmpleadoFK = (int)reader["ID_EMPLEADO"];
                        Empleado empleadoAsignado = _empleadoDao.GetById(idEmpleadoFK);

                        usuario = new Usuario(
                            nombreUsuario: reader["NOMBRE_USUARIO"].ToString(),
                            empleado: empleadoAsignado
                        );

                        usuario.contrasena = reader["CONTRASENA"]?.ToString();
                    }
                }
            }
            return usuario;
        }
        public void Insert(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            if (usuario.idEmpleadoFK <= 0)
                throw new InvalidOperationException("No se puede insertar Usuario: empleado inválido o no creado (idEmpleadoFK <= 0).");

            var sql = @"
                INSERT INTO USUARIO (NOMBRE_USUARIO, CONTRASENA, ID_EMPLEADO) 
                VALUES (@nombreUsuario, @contrasena, @idEmpleado)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", usuario.contrasena ?? "");
                cmd.Parameters.AddWithValue("@idEmpleado", usuario.idEmpleadoFK);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
using ImplementacionCU37.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ImplementacionCU37.Dao
{
    public class EmpleadoDao
    {
        private readonly string _connectionString;
        private readonly RolDao _rolDao;

        public EmpleadoDao(string connectionString)
        {
            _connectionString = connectionString;
            _rolDao = new RolDao(connectionString);
        }

        public List<Empleado> GetAll()
        {
            var listaEmpleados = new List<Empleado>();
            // 1. Instanciamos el DAO de la dependencia para resolver la FK.
            var rolDao = new RolDao(_connectionString);

            // Usamos los nombres de columna de tu DDL/DER: ID_EMPLEADO, ID_ROL, etc.
            var sql = "SELECT ID_EMPLEADO, NOMBRE, APELLIDO, TELEFONO, EMAIL, ID_ROL FROM EMPLEADO";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // 2. Leemos la Clave Foránea (ID_ROL)
                        int idRolFK = (int)reader["ID_ROL"];

                        // 3. Resolvemos la relación: Obtenemos el objeto Rol completo.
                        Rol rolAsignado = rolDao.GetById(idRolFK);

                        // 4. Creamos y mapeamos el objeto Empleado
                        var empleado = new Empleado
                        {
                            // Mapeo de la PK
                            idEmpleado = (int)reader["ID_EMPLEADO"],

                            nombre = reader["NOMBRE"].ToString(),
                            apellido = reader["APELLIDO"].ToString(),
                            telefono = reader["TELEFONO"].ToString(),
                            email = reader["EMAIL"].ToString(),

                            // 5. Asignamos el objeto Rol resuelto
                            rol = rolAsignado
                        };
                        listaEmpleados.Add(empleado);
                    }
                }
            }
            return listaEmpleados;
        }

        // *** MÉTODO AGREGADO: Obtener Empleado por ID (Necesario para FKs) ***
        public Empleado GetById(int idEmpleado)
        {
            Empleado empleado = null;

            // Consulta de un solo registro
            var sql = "SELECT ID_EMPLEADO, NOMBRE, APELLIDO, TELEFONO, EMAIL, ID_ROL FROM EMPLEADO WHERE ID_EMPLEADO = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idEmpleado);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idRolFK = (int)reader["ID_ROL"];
                        Rol rolAsignado = _rolDao.GetById(idRolFK);
                        empleado = new Empleado
                        {
                            idEmpleado = (int)reader["ID_EMPLEADO"], // Mapeo de la PK
                            nombre = reader["NOMBRE"].ToString(),
                            apellido = reader["APELLIDO"].ToString(),
                            telefono = reader["TELEFONO"].ToString(),
                            email = reader["EMAIL"].ToString(),
                            rol = rolAsignado // Asignamos el objeto Rol
                        };
                    }
                }
            }
            return empleado;
        }

        // Método de Inserción (Usado por DatabaseInitializer.Seed())
        public void Insert(Empleado empleado)
        {
            var rolDao = new RolDao(_connectionString);
            var rolEncontrado = rolDao.GetAll().FirstOrDefault(r => r.nombre == empleado.rol.nombre);

            if (rolEncontrado == null) return;

            var sql = @"
                INSERT INTO EMPLEADO (NOMBRE, APELLIDO, TELEFONO, EMAIL, ID_ROL) 
                VALUES (@nombre, @apellido, @telefono, @email, @idRol)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", empleado.nombre);
                cmd.Parameters.AddWithValue("@apellido", empleado.apellido);
                cmd.Parameters.AddWithValue("@telefono", empleado.telefono);
                cmd.Parameters.AddWithValue("@email", empleado.email);
                cmd.Parameters.AddWithValue("@idRol", rolEncontrado.idRol);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
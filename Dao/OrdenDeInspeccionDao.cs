using ImplementacionCU37.Entidades;
using ImplementacionCU37.Estados;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace ImplementacionCU37.Dao
{
    public class OrdenDeInspeccionDao
    {
        private readonly string _connectionString;
        private readonly EmpleadoDao _empleadoDao;
        private readonly EstadoDao _estadoDao;
        private readonly EstacionSismologicaDao _estacionDao;

        public OrdenDeInspeccionDao(string connectionString)
        {
            _connectionString = connectionString;
            _empleadoDao = new EmpleadoDao(connectionString);
            _estadoDao = new EstadoDao(connectionString);
            _estacionDao = new EstacionSismologicaDao(connectionString);
        }

        public List<OrdenDeInspeccion> GetAll()
        {
            var ordenes = new List<OrdenDeInspeccion>();

            var sql = "SELECT ID_ORDEN, NRO_ORDEN, FECHA_HORA_INICIO, FECHA_HORA_FIN, FECHA_HORA_CIERRE, " +
                      "OBSERVACION_CIERRE, ID_ESTADO, ID_EMPLEADO, ID_ESTACION " +
                      "FROM ORDEN_DE_INSPECCION";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Log raw DB value for diagnosis
                        var rawFin = reader["FECHA_HORA_FIN"];
                        Debug.WriteLine($"DB ROW -> ID_ORDEN={reader["ID_ORDEN"]}, NRO_ORDEN={reader["NRO_ORDEN"]}, FECHA_HORA_FIN={rawFin}");

                        int idEstadoFK = reader["ID_ESTADO"] != DBNull.Value ? (int)reader["ID_ESTADO"] :0;
                        int idEmpleadoFK = reader["ID_EMPLEADO"] != DBNull.Value ? (int)reader["ID_EMPLEADO"] :0;
                        int idEstacionFK = reader["ID_ESTACION"] != DBNull.Value ? (int)reader["ID_ESTACION"] :0;


                        Empleado empleado = idEmpleadoFK >0 ? _empleadoDao.GetById(idEmpleadoFK) : null;
                        Estado estadoReal = idEstadoFK >0 ? _estadoDao.GetById(idEstadoFK) : null;
                        EstacionSismologica estacion = idEstacionFK >0 ? _estacionDao.GetById(idEstacionFK) : null;

                        IEstado estadoObjeto = null;
                        if (estadoReal != null)
                        {
                            try
                            {
                                estadoObjeto = EstadoFactory.CrearEstadoDesde(estadoReal);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Advertencia: no se pudo crear IEstado desde Estado id={idEstadoFK}: {ex.Message}");
                                estadoObjeto = null;
                            }
                        }

                        var orden = new OrdenDeInspeccion(
                            numeroOrden: (int)reader["NRO_ORDEN"],
                            fechaHoraInicio: (DateTime)reader["FECHA_HORA_INICIO"],
                            estacion: estacion,
                            estado: estadoObjeto,
                            empleadoAsignado: empleado
                        )
                        {
                            idOrden = (int)reader["ID_ORDEN"],
                            idEstadoFK = idEstadoFK,
                            fechaHoraFin = reader["FECHA_HORA_FIN"] != DBNull.Value ? (DateTime?)reader["FECHA_HORA_FIN"] : null,
                            fechaHoraCierre = reader["FECHA_HORA_CIERRE"] != DBNull.Value ? (DateTime?)reader["FECHA_HORA_CIERRE"] : null,
                            observacionCierre = reader["OBSERVACION_CIERRE"] != DBNull.Value ? reader["OBSERVACION_CIERRE"].ToString() : null,
                        };
                        ordenes.Add(orden);
                    }
                }
            }
            return ordenes;
        }

        public void Insert(OrdenDeInspeccion orden)
        {
            var sql = @"
                INSERT INTO ORDEN_DE_INSPECCION (NRO_ORDEN, FECHA_HORA_INICIO, FECHA_HORA_FIN, 
                FECHA_HORA_CIERRE, OBSERVACION_CIERRE, ID_ESTADO, ID_EMPLEADO, ID_ESTACION) 
                VALUES (@nroOrden, @fIni, @fFin, @fCierre, @obsCierre, @idEstado, @idEmpleado, @idEstacion)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                Debug.WriteLine("Estado ID = " + orden.idEstadoFK);

                // Parámetros obligatorios
                cmd.Parameters.AddWithValue("@nroOrden", orden.nroOrden);
                cmd.Parameters.AddWithValue("@fIni", orden.fechaHoraInicio);
                cmd.Parameters.AddWithValue("@idEstado", orden.idEstadoFK);
                cmd.Parameters.AddWithValue("@idEmpleado", orden.idEmpleadoAsignadoFK);
                cmd.Parameters.AddWithValue("@idEstacion", orden.idEstacionFK);

                // Parámetros opcionales (manejar NULL)
                var paramFin = orden.fechaHoraFin.HasValue ? (object)orden.fechaHoraFin.Value : DBNull.Value;
                cmd.Parameters.AddWithValue("@fFin", paramFin);
                cmd.Parameters.AddWithValue("@fCierre", orden.fechaHoraCierre.HasValue ? (object)orden.fechaHoraCierre.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@obsCierre", string.IsNullOrEmpty(orden.observacionCierre) ? (object)DBNull.Value : orden.observacionCierre);

                conn.Open();
                Debug.WriteLine($"Inserting Orden -> NRO_ORDEN={orden.nroOrden}, FECHA_HORA_INICIO={orden.fechaHoraInicio}, FECHA_HORA_FIN={paramFin}");

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(OrdenDeInspeccion orden)
        {
            var sql = @"
        UPDATE ORDEN_DE_INSPECCION 
        SET FECHA_HORA_CIERRE = @fCierre,
            OBSERVACION_CIERRE = @obsCierre,
            ID_ESTADO = @idEstado
        WHERE ID_ORDEN = @idOrden";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                // Parámetros de actualización
                cmd.Parameters.AddWithValue("@fCierre", orden.fechaHoraCierre.HasValue ? (object)orden.fechaHoraCierre.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@obsCierre", string.IsNullOrEmpty(orden.observacionCierre) ? (object)DBNull.Value : orden.observacionCierre);
                cmd.Parameters.AddWithValue("@idEstado", orden.idEstadoFK); // Usamos el ID del nuevo estado ("CERRADA")

                // Parámetro de WHERE (Clave Primaria)
                cmd.Parameters.AddWithValue("@idOrden", orden.idOrden);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
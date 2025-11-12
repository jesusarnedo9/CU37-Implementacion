using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ImplementacionCU37.Dao
{
    public class EstacionSismologicaDao
    {
        private readonly string _connectionString;
        private readonly SismografoDao _sismografoDao;

        public EstacionSismologicaDao(string connectionString)
        {
            _connectionString = connectionString;
            _sismografoDao = new SismografoDao(connectionString);
        }

        public List<EstacionSismologica> GetAll()
        {
            var estaciones = new List<EstacionSismologica>();

            var sql = "SELECT ID_ESTACION_SISMOLOGICA, CODIGO_ESTACION, DOCUMENTO_CERTIFICACION_ADQ, " +
                      "FECHA_CERTIFICACION, LATITUD, LONGITUD, NOMBRE, NRO_CERTIFICACION_ADQ, ID_SISMOGRAFO " +
                      "FROM ESTACION_SISMOLOGICA";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idSismografoFK = (int)reader["ID_SISMOGRAFO"];

                        Sismografo sismografoAsignado = _sismografoDao.GetById(idSismografoFK);

                        var estacion = new EstacionSismologica(sismografoAsignado)
                        {
                            idEstacionSismologica = Convert.ToInt32(reader["ID_ESTACION_SISMOLOGICA"]),
                            codigoEstacion = reader["CODIGO_ESTACION"].ToString(),
                            documentoCertificacionAdq = reader["DOCUMENTO_CERTIFICACION_ADQ"].ToString(),
                            fechaCertificacion = Convert.ToDateTime(reader["FECHA_CERTIFICACION"]),
                            latitud = Convert.ToDouble(reader["LATITUD"]),
                            longitud = Convert.ToDouble(reader["LONGITUD"]),
                            nombre = reader["NOMBRE"].ToString(),
                            nroCertificacionAdq = reader["NRO_CERTIFICACION_ADQ"].ToString()
                        };

                        Debug.WriteLine($"Fila -> ID_ESTACION_SISMOLOGICA={reader["ID_ESTACION_SISMOLOGICA"]}, ID_SISMOGRAFO={reader["ID_SISMOGRAFO"]}");

                        estaciones.Add(estacion);
                    }
                }
            }
            return estaciones;
        }

        public void Insert(EstacionSismologica estacion)
        {
            var sql = @"
        INSERT INTO ESTACION_SISMOLOGICA (
            CODIGO_ESTACION, 
            DOCUMENTO_CERTIFICACION_ADQ, 
            FECHA_CERTIFICACION, 
            LATITUD, 
            LONGITUD, 
            NOMBRE, 
            NRO_CERTIFICACION_ADQ, 
            ID_SISMOGRAFO
        ) 
        VALUES (
            @codigo, 
            @docCert, 
            @fechaCert, 
            @lat, 
            @lon, 
            @nombre, 
            @nroCert, 
            @idSismografo
        )";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@codigo", estacion.codigoEstacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@docCert", estacion.documentoCertificacionAdq ?? (object)DBNull.Value);

                // Evita desbordamiento de DateTime
                var fechaValida = estacion.fechaCertificacion < new DateTime(1753, 1, 1)
                    ? (object)DBNull.Value
                    : estacion.fechaCertificacion;
                cmd.Parameters.AddWithValue("@fechaCert", fechaValida);

                cmd.Parameters.AddWithValue("@lat", estacion.latitud);
                cmd.Parameters.AddWithValue("@lon", estacion.longitud);
                cmd.Parameters.AddWithValue("@nombre", estacion.nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nroCert", estacion.nroCertificacionAdq ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@idSismografo", estacion.idSismografoFK);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // *** MÉTODO AGREGADO: Obtener Estacion por ID (Necesario para FKs) ***
        public EstacionSismologica GetById(int idEstacion)
        {
            EstacionSismologica estacion = null;

            var sql = "SELECT ID_ESTACION_SISMOLOGICA, CODIGO_ESTACION, DOCUMENTO_CERTIFICACION_ADQ, " +
                      "FECHA_CERTIFICACION, LATITUD, LONGITUD, NOMBRE, NRO_CERTIFICACION_ADQ, ID_SISMOGRAFO " +
                      "FROM ESTACION_SISMOLOGICA WHERE ID_ESTACION_SISMOLOGICA = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idEstacion);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idSismografoFK = (int)reader["ID_SISMOGRAFO"];

                        Sismografo sismografoAsignado = _sismografoDao.GetById(idSismografoFK);

                        estacion = new EstacionSismologica(sismografoAsignado)
                        {
                            idEstacionSismologica = (int)reader["ID_ESTACION_SISMOLOGICA"],
                            codigoEstacion = reader["CODIGO_ESTACION"].ToString(),
                            documentoCertificacionAdq = reader["DOCUMENTO_CERTIFICACION_ADQ"].ToString(),
                            fechaCertificacion = Convert.ToDateTime(reader["FECHA_CERTIFICACION"]),
                            latitud = (double)reader["LATITUD"],
                            longitud = (double)reader["LONGITUD"],
                            nombre = reader["NOMBRE"].ToString(),
                            nroCertificacionAdq = reader["NRO_CERTIFICACION_ADQ"].ToString()
                        };
                    }
                }
            }
            return estacion;
        }
    }
}
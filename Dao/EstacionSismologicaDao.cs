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
                        int idSismografoFK = reader["ID_SISMOGRAFO"] != DBNull.Value ? Convert.ToInt32(reader["ID_SISMOGRAFO"]) :0;

                        Sismografo sismografoAsignado = idSismografoFK >0 ? _sismografoDao.GetById(idSismografoFK) : null;

                        var estacion = new EstacionSismologica(sismografoAsignado)
                        {
                            idEstacionSismologica = reader["ID_ESTACION_SISMOLOGICA"] != DBNull.Value ? Convert.ToInt32(reader["ID_ESTACION_SISMOLOGICA"]) :0,
                            codigoEstacion = reader["CODIGO_ESTACION"] != DBNull.Value ? reader["CODIGO_ESTACION"].ToString() : null,
                            documentoCertificacionAdq = reader["DOCUMENTO_CERTIFICACION_ADQ"] != DBNull.Value ? reader["DOCUMENTO_CERTIFICACION_ADQ"].ToString() : null,
                            fechaCertificacion = reader["FECHA_CERTIFICACION"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA_CERTIFICACION"]) : default(DateTime),
                            latitud = reader["LATITUD"] != DBNull.Value ? Convert.ToDouble(reader["LATITUD"]) :0.0,
                            longitud = reader["LONGITUD"] != DBNull.Value ? Convert.ToDouble(reader["LONGITUD"]) :0.0,
                            nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : null,
                            nroCertificacionAdq = reader["NRO_CERTIFICACION_ADQ"] != DBNull.Value ? reader["NRO_CERTIFICACION_ADQ"].ToString() : null
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
                var fechaValida = estacion.fechaCertificacion < new DateTime(1753,1,1)
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
                        int idSismografoFK = reader["ID_SISMOGRAFO"] != DBNull.Value ? Convert.ToInt32(reader["ID_SISMOGRAFO"]) :0;

                        Sismografo sismografoAsignado = idSismografoFK >0 ? _sismografoDao.GetById(idSismografoFK) : null;

                        estacion = new EstacionSismologica(sismografoAsignado)
                        {
                            idEstacionSismologica = reader["ID_ESTACION_SISMOLOGICA"] != DBNull.Value ? Convert.ToInt32(reader["ID_ESTACION_SISMOLOGICA"]) :0,
                            codigoEstacion = reader["CODIGO_ESTACION"] != DBNull.Value ? reader["CODIGO_ESTACION"].ToString() : null,
                            documentoCertificacionAdq = reader["DOCUMENTO_CERTIFICACION_ADQ"] != DBNull.Value ? reader["DOCUMENTO_CERTIFICACION_ADQ"].ToString() : null,
                            fechaCertificacion = reader["FECHA_CERTIFICACION"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA_CERTIFICACION"]) : default(DateTime),
                            latitud = reader["LATITUD"] != DBNull.Value ? Convert.ToDouble(reader["LATITUD"]) :0.0,
                            longitud = reader["LONGITUD"] != DBNull.Value ? Convert.ToDouble(reader["LONGITUD"]) :0.0,
                            nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : null,
                            nroCertificacionAdq = reader["NRO_CERTIFICACION_ADQ"] != DBNull.Value ? reader["NRO_CERTIFICACION_ADQ"].ToString() : null
                        };
                    }
                }
            }
            return estacion;
        }
    }
}
using ImplementacionCU37.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ImplementacionCU37.Dao
{
    public class SismografoDao
    {
        private readonly string _connectionString;
        private readonly EstadoDao _estadoDao;
        private readonly CambioEstadoDao _cambioEstadoDao;

        public SismografoDao(string connectionString)
        {
            _connectionString = connectionString;
            _estadoDao = new EstadoDao(connectionString);
            _cambioEstadoDao = new CambioEstadoDao(connectionString);
        }

        public List<Sismografo> GetAll()
        {
            var sismografos = new List<Sismografo>();
            var sql = "SELECT ID_SISMOGRAFO, FECHA_ADQUISICION,IDENTIFICADOR_SISMOGRAFO, NUMERO_SERIE, ID_ESTADO FROM SISMOGRAFO";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idEstadoFK = (int)reader["ID_ESTADO"];
                        int idSismografoPK = (int)reader["ID_SISMOGRAFO"];

                        Estado estadoActual = _estadoDao.GetById(idEstadoFK);

                        List<CambioEstado> historial = _cambioEstadoDao.GetBySismografoId(idSismografoPK);

                        var s = new Sismografo
                        {
                            idSismografo = Convert.ToInt32(reader["ID_SISMOGRAFO"]),
                            fechaAdquisicion = Convert.ToDateTime(reader["FECHA_ADQUISICION"]),
                            identificadorSismografo = reader["IDENTIFICADOR_SISMOGRAFO"].ToString(),
                            numeroSerie = reader["NUMERO_SERIE"].ToString()
                        };
                        sismografos.Add(s);
                        Debug.WriteLine($"DAO -> {s.idSismografo} - {s.identificadorSismografo} - {s.numeroSerie}");

                    }
                }
            }
            return sismografos;
        }

        public int Insert(Sismografo sismografo)
        {
            var sql = @"
                INSERT INTO SISMOGRAFO (IDENTIFICADOR_SISMOGRAFO, FECHA_ADQUISICION, NUMERO_SERIE, ID_ESTADO)
                OUTPUT INSERTED.ID_SISMOGRAFO
                VALUES (@identificador, @fechaAdq, @nroSerie, @idEstado);";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@identificador", sismografo.identificadorSismografo);
                cmd.Parameters.AddWithValue("@fechaAdq", sismografo.fechaAdquisicion);
                cmd.Parameters.AddWithValue("@nroSerie", sismografo.numeroSerie);
                cmd.Parameters.AddWithValue("@idEstado", sismografo.idEstadoActualFK);

                conn.Open();
                var idObjeto = cmd.ExecuteScalar();
                return (idObjeto != null && idObjeto != DBNull.Value) ? Convert.ToInt32(idObjeto) : 0;
        }
        }


        public Sismografo GetById(int idSismografo)
        {
            var sql = "SELECT ID_SISMOGRAFO, FECHA_ADQUISICION, IDENTIFICADOR_SISMOGRAFO,NUMERO_SERIE, ID_ESTADO FROM SISMOGRAFO WHERE ID_SISMOGRAFO = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idSismografo);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idEstadoFK = (int)reader["ID_ESTADO"];

                        Estado estadoActual = _estadoDao.GetById(idEstadoFK);

                        List<CambioEstado> historial = _cambioEstadoDao.GetBySismografoId(idSismografo);

                        return new Sismografo
                        {
                            idSismografo = idSismografo,
                            fechaAdquisicion = reader["FECHA_ADQUISICION"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA_ADQUISICION"]) : default(DateTime),
                            identificadorSismografo = reader["IDENTIFICADOR_SISMOGRAFO"] != DBNull.Value ? reader["IDENTIFICADOR_SISMOGRAFO"].ToString() : null,
                            numeroSerie = reader["NUMERO_SERIE"] != DBNull.Value ? reader["NUMERO_SERIE"].ToString() : null,
                            estadoActual = estadoActual,
                            historialEstados = historial
                        };
                    }
                }
            }
            return null;
        }
    }
}
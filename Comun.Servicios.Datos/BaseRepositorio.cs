using Comun.Servicios.Transversales;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Comun.Servicios.Datos
{
    /// <summary>
    /// Clase padre empleada para el acceso a datos:
    /// </summary>
    public abstract class BaseRepositorio : IDisposable
    {
        #region Variables de clase

        bool disposed = false;
        protected SqlConnection conexion;
        protected SqlDataReader reader;
        private string cadenaConexion = ConfigurationManager.ConnectionStrings[Constantes.conexion].ConnectionString;

        protected string CadenaConexion
        {
            get { return cadenaConexion; }
        }

        #endregion Variables de clase

        /// <summary>
        /// Permite la ejecución de un procedimiento que no tenga ningún retorno 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected int EjecutarNoQuery(string procedimiento, SqlParameter[] parametros)
        {
            using (conexion = new SqlConnection(CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se adicionan los parámetros al command
                    if (parametros != null)
                        cmd.Parameters.AddRange(parametros);
                    // Se abre la conexión
                    conexion.Open();
                    cmd.CommandTimeout = 0;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Permite la ejecución de un procedimiento cuyo retorno sea una columna
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected object EjecutarEscalar(string procedimiento, SqlParameter[] parametros)
        {
            using (conexion = new SqlConnection(CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se adicionan los parámetros al command
                    if (parametros != null)
                        cmd.Parameters.AddRange(parametros);
                    // Se abre la conexión
                    conexion.Open();
                    cmd.CommandTimeout = 0;
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Permite el mapeo desde un reader a la entidad de transporte
        /// </summary>
        protected Entidades.Post[] MapearPost(IDataReader reader)
        {
            var retorno = new List<Entidades.Post>();
            while (reader.Read())
            {
                retorno.Add(new Entidades.Post()
                {
                    Id = reader.GetInt32(0),
                    Descripcion = reader.GetString(1),
                    EstadoId = reader.GetInt32(2)
                });
            }
            return retorno.ToArray();
        }

        #region Dispose

        /// <summary>
        /// Hace llamado de la implementación Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Descarga los objetos completamente
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // EN caso de que una conexion se encuentre viva
                if (!Equals(conexion, null))
                    conexion.Dispose();
                // En caso de que el reader aun se encuentre en uso
                if (!Equals(reader, null))
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    reader = null;
                }
            }

        }

        #endregion
    }
}

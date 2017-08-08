using Comun.Servicios;
using Comun.Servicios.Entidades;
using Comun.Servicios.Transversales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Servicios.Datos
{
    public class PostRepositorio : BaseRepositorio
    {

        #region Funciones publicas

        /// <summary>
        /// Permite obtener los post que hacen falta por enviar al servicio de analisis
        /// </summary>
        public Entidades.Post[] ObtenerPostEnviar()
        {
            using (conexion = new SqlConnection(CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(Constantes.spObtenerPostSinEnviar, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se abre la conexión
                    conexion.Open();
                    cmd.CommandTimeout = 0;

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return MapearPost(reader);
                    }
                    else
                        return new Entidades.Post[] { };
                }
            }
        }

        /// <summary>
        /// Permite la actualización de el estado de los post a enviar
        /// </summary>
        public bool ActualizarEstadoPost(int postId, EstadoPost estado)
        {
            var parametros = 
                new SqlParameter[]
                    { new SqlParameter() { ParameterName = "@Opcion", Value = Opcion.Actualizar, DbType = DbType.Int32 },
                        new SqlParameter() { ParameterName = "@PostId", Value = postId, DbType = DbType.Int32 },
                        new SqlParameter() { ParameterName = "@EstadoId", Value = estado, DbType = DbType.Int32 } };
            // Ejecutar la actualización del post:
            int resultado = EjecutarNoQuery(Constantes.spAdministrarPosts, parametros);
            if (resultado > 0)
                return true;
            else
                return false;
        }   

        /// <summary>
        /// Permite la creaión de posts
        /// </summary>
        public int CrearPost(Post post)
        {
            var parametros =
                new SqlParameter[]
                    { new SqlParameter() { ParameterName = "@Opcion", Value = Opcion.Crear, DbType = DbType.Int32 },
                      new SqlParameter() { ParameterName = "@Descripcion", Value = post.Descripcion, DbType = DbType.String },
                      new SqlParameter() { ParameterName = "@EstadoId", Value = EstadoPost.Creado, DbType = DbType.Int32 } };
            // Ejecutar la actualización del post:
            int resultado = EjecutarNoQuery(Constantes.spAdministrarPosts, parametros);
            return resultado;
        }

        #endregion

        #region Funciones privadas

        #endregion
    }
}

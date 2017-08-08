using Comun.Servicios.Datos;
using Comun.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizador.Servicio.Negocio
{
    public class AdministradorPosts
    {
        /// <summary>
        /// Se crea el método para ejecutar un task y evitar bloqueos en IO
        /// </summary>
        public int CrearPost(Post post)
        {
            int retorno;
            using (AnalisisRepositorio repositorio = new AnalisisRepositorio())
            {
                retorno = repositorio.CrearPost(post);
            }
            // Controlar error en la creación del post:
            if (retorno == 0)
                throw new Exception();

            return retorno;
        }

    }
}

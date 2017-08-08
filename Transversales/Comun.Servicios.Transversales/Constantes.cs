using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Servicios.Transversales
{
    public static class Constantes
    {
        public const string conexion = "Conexion";
        // Asociados a los post en el lado del generador de posts:
        public const string spObtenerPostSinEnviar = "Post.ObtenerPostSinEnviar";
        public const string spAdministrarPosts = "Post.AdministrarPosts";
        // Asociados a los post del lado del analizador:
        public const string spAdminAnalisisPosts = "Analisis.AdministrarPosts";

        public const string postUri = "PostUri";
        public const string mediaType = "MediaType";
        public const string baseAdress = "BaseAdress";
        public const string mensajeOk = "Ok";

        public const string esp = " ";
    }
}

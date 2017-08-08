using Comun.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Servicios.Transversales
{
    public static class PostInvoke
    {
        #region Propiedades

        /// <summary>
        /// Uri donde se envián los post mediante Http POST
        /// </summary>
        static string PostUri
        {
            get { return ConfigurationManager.AppSettings[Constantes.postUri].ToString(); }
        }

        /// <summary>
        /// Header de la solicitud de Http POST 
        /// </summary>
        static string MediaType
        {
            get { return ConfigurationManager.AppSettings[Constantes.postUri].ToString(); }
        }

        /// <summary>
        /// Dirección base donde se encuentraa publicado el servicio WebApi para los posts 
        /// </summary>
        static string BaseAddress
        {
            get { return ConfigurationManager.AppSettings[Constantes.baseAdress].ToString(); }
        }

        #endregion Propiedades

        #region Funciones publicas

        /// <summary>
        /// Permite invocar el llamado al servicio WebApi de Posts
        /// </summary>
        public static EstadoPost EnviarPost(Post post)
        {
            Task<EstadoPost> tarea = Task<EstadoPost>.Factory.StartNew(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    ClientInitialize(client);
                    using (var respuesta = CrearPostAsync(client, post))
                    {
                        if (Equals(respuesta.Result, HttpStatusCode.Created))
                            return EstadoPost.Creado;
                        else
                            return EstadoPost.Error;
                    }
                }
            });
            return tarea.Result;
        }

        #endregion Funciones publicas

        #region Metodos internos

        /// <summary>
        /// Permite inicializar el cliente HttpClient para envio de los post
        /// </summary>
        static void ClientInitialize(HttpClient client)
        {
            // New code:
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
        }

        /// <summary>
        /// Permite invocar el servicio WebApi para los post:
        /// </summary>
        static async Task<HttpStatusCode> CrearPostAsync(HttpClient client, Post post)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(PostUri, post);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.StatusCode;
        }

        #endregion Metodos internos
    }
}
